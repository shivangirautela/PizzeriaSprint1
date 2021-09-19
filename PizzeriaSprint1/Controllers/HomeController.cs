using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzeriaSprint1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Message = "Admin Login Page.";

            return View();
        }

        public ActionResult EmployeeM()
        {
            ViewBag.Message = "Employee Login Page.";

            return View();
        }
        public ActionResult Customer()
        {
            ViewBag.Message = "Customer Login Page.";

            return View();
        }

    }
}