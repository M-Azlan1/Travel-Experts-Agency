using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelExpertsData;
using System.Security.Claims;
using TravelExpertsMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelExpertsMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly TravelExpertsContext _context;

        public AccountController(TravelExpertsContext context)
        {
            _context = context;
        }
        // GET: AccountController
        public IActionResult Login(String returnUrl = "")
        {
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();

        }

        // Create Login functionality for each user using their Username and Password. (Coded by: Ali, with help from Muhammad)
        [HttpPost]
        public async Task<IActionResult> LoginAsync(Customer user) // data collected on the form
        {
            Customer usr = CustomerManager.Authenticate(user.Username, user.Password);
            if (usr == null) // failed authentication
            {
                ViewData["ErrorMessage"] = "Username or password is incorrect.";
                return View(); // stay on the login page
            }
            // usr != null   - authentication passed


            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usr.Username),
                //new Claim("FullName", usr.FullName),
                //new Claim(ClaimTypes.Role, usr.Role)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme); // use cookies authentication
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal); // generates authentication cookie
            // if no return URL, go to the home page
            if (string.IsNullOrEmpty(TempData["ReturnUrl"].ToString()))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect(TempData["ReturnUrl"].ToString());
            }

        }

        //Create logout functionality for the logged in user. (Coded by: Ali, with help from Muhammad)
        public async Task<IActionResult> LogoutAsync()
        {
            // release authentication cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
           
          

            return RedirectToAction("Index", "Home"); // go to the home page
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        public IActionResult Register(string returnUrl = "")
        {
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }

        //Create functionality for new customers to register using a form and input the values needed for the sql database. (Code by: Muhammad, with help from Ali)

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the username already exists in the database
                var existingUser = await _context.Customers.FirstOrDefaultAsync(c => c.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "This username is already taken. Please try entering a different username.");
                    return View(model);
                }
                // create new customer and add to context
                var customer = new Customer
                {
                    CustFirstName = model.CustFirstName,
                    CustLastName = model.CustLastName,
                    CustAddress = model.CustAddress,
                    CustCity = model.CustCity,
                    CustProv = model.CustProv,
                    CustPostal = model.CustPostal,
                    CustCountry = model.CustCountry,
                    CustHomePhone = model.CustHomePhone,
                    CustBusPhone = model.CustBusPhone,
                    CustEmail = model.CustEmail,
                    Username = model.Username,
                    Password = model.Password,
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }

            // return view with validation messages
            return View(model);
        }

    }
}

