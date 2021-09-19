using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaSprint1.Models;

namespace PizzeriaSprint1.Controllers
{
    /// <summary>
    /// This class contains methods for Employee role management in the application.
    /// </summary>
    /// <returns></returns>
    public class DashboardController : Controller
    {
        PizzeriaEntities1 db = new PizzeriaEntities1();
        /// <summary>
        /// This method redirects to Employee Home Page where he can access his/her Previldeges in the
        /// application.
        /// </summary>
        /// <returns></returns>
        public ActionResult Dash(string searching)
        {
            return View(db.Customers.Where(Model => Model.FirstName.StartsWith(searching) || searching == null).ToList());
        }
        /// <summary>
        /// This action method helps to place an order in pizzeria.It shows all products available.
        /// </summary>
        /// <returns></returns>
        public ActionResult PlaceOrder()
        {
            PizzaModel pizzaModel = new PizzaModel();
            ViewBag.products = pizzaModel.AllPizzas();
            return View(this.db.PizzaTables.ToList());
        }
     
    }
}