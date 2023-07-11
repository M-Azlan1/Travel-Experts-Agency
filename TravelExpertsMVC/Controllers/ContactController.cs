using Microsoft.AspNetCore.Mvc;

namespace TravelExpertsMVC.Controllers
{
    public class ContactController : Controller
    {
        //Static view for the contact page (can later be employee information taken from the sql database) (Coded by: Muhammad, with help from Ali) 
        public IActionResult Contact()
        {
            return View();
        }
    }
}
