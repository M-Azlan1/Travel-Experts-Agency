using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelExpertsData;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TravelExpertsMVC.Controllers
{
   
    public class BookingController : Controller
    {
        private readonly TravelExpertsContext _context;

        public BookingController(TravelExpertsContext context)
        {
            _context = context;
        }
        public int CustomerId { get; private set; }

        // GET: BookingController

        // Main static booking page to display all packages and details
        public ActionResult Booking()
        {
            return View();
        }


        // Create a MyBookings page only displays information for a customer that is logged in. (Coded by: Muhammad, with help from Ali)
        [Authorize]
        public async Task<IActionResult> MyBooking()
        {

            if (User.Identity.Name != null)
            {
                decimal totalbookingCost = 0;
                int customerId = CustomerManager.FindCustomer(User.Identity.Name, _context).CustomerId;
                List<Booking> custBooking = _context.Bookings
                    .Include(b => b.BookingDetails) // Include BookingDetails
                    .Include(b => b.Package)
                    .Where(c => c.CustomerId == customerId)
                    .ToList();


                // Calculate the total cost of all the packages present in the customers profile. (Coded by: Muhammad, with help from Ali)
                custBooking.ForEach(c =>
                {
                    if (c.PackageId != null)
                    {
                        totalbookingCost += calCost(c);
                  
                    }
                });

                ViewBag.TotalCost = totalbookingCost.ToString("c");
                return View(custBooking);
            }
            else
            {
                return View("Index", "Home");
            }


        }

        // Total cost formula. (Coded by: Muhammad, with help by Ali)
        private decimal calCost(Booking booking)
        {
            return (booking.Package.PkgBasePrice * Convert.ToDecimal(booking.TravelerCount));

        }


        // Delete each booking from the customers profile. (Coded by: Muhammad, with help from Ali)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.BookingDetails)
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyBooking));
        }
    }
}
