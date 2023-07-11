using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class CustomerManager
    {
        public static List<Customer> GetAll(TravelExpertsContext db)
        {
            List<Customer> customers = db.Customers.OrderBy(c => c.CustFirstName).ToList();
            return customers;
        }

        /// <summary>
        /// User is authenticated based on credentials and a user returned if exists or null if not.
        /// </summary>
        /// <param name="username">Username as string</param>
        /// <param name="password">Password as string</param>
        /// <returns>A user object or null.</returns>
        /// (Coded by Muhammad & Ali)

        public static Customer Authenticate(string username, string password)
        {
            Customer user = null;
            using (TravelExpertsContext db = new TravelExpertsContext())
            {
                user = db.Customers.SingleOrDefault(usr => usr.Username == username
                                                    && usr.Password == password);
            }

            return user; //this will either be null or an object
        }

        // Find a customer using their unique Username. (Coded by Muhammad & Ali)
        public static Customer FindCustomer(string username, TravelExpertsContext db)
        {

            Customer cust = null;

            cust = db.Customers.Include(b => b.Bookings).SingleOrDefault(c => c.Username == username); //get customer with including associated

            return cust;
        }
    }
         
        
}
