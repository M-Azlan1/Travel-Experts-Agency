using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    [Authorize]
    public class PackagesController : Controller
    {
        private readonly TravelExpertsContext _context;

        public PackagesController(TravelExpertsContext context)
        {
            _context = context;
        }

        // Get and display all packages available (Coded by: Ali, with help from Muhammad)
        public ActionResult Packages()
        {
            List<Package> packages = PackageManager.GetAllPackages();
            return View(packages);
        }

        //Create funchtionably to add packages to the logged in customer database. (Coded by: Muhammad, with help from Ali)
        [HttpPost]
        public async Task<IActionResult> BookPackage(int packageId, string bookingId)
        {
            if (User.Identity.Name == null)
            {
                // If the user is not logged in, redirect to the login page
                return RedirectToAction("Login", "Account");
            }

            // Find the customer ID of the logged-in user
            int customerId = CustomerManager.FindCustomer(User.Identity.Name, _context).CustomerId;

            // Get the traveler count from the form data
            int travelerCount = int.Parse(Request.Form["travelerCount"]);

            // Create a new booking and set its customer ID, package ID, booking number, and traveler count
            Booking booking = new Booking();
            booking.CustomerId = customerId;
            booking.PackageId = packageId;
            booking.BookingDate = DateTime.Today;
            booking.TravelerCount = travelerCount;

            // Add the booking to the context and save changes to the database
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // Redirect to the MyBooking action method to show the updated booking list
            return RedirectToAction("MyBooking", "Booking");
        }

        // GET: PackagesController/Details/5



    }
}
