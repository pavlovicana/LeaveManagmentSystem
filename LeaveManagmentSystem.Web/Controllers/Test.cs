using LeaveManagmentSystem.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagmentSystem.Web.Controllers
{
    public class Test : Controller
    {
        public IActionResult Index()
        {
            var data = new TestViewModel
            {
                Name = "Student of MVC Master",
                DateOfBirth = new DateTime(1994,05, 21)
            };
            return View(data);
        }
    }
}
