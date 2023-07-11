using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class BookingManager
    {
        // display all bookings ordered by customerId column (Coded by: Muhammad & Ali)
        public static List<Booking> GetAll()
        {
            TravelExpertsContext db = new TravelExpertsContext();
            List<Booking> bookings = db.Bookings.OrderBy(o => o.CustomerId).ToList();
            return bookings;
        }
    }
}
