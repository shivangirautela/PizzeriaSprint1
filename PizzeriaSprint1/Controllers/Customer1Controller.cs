using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaSprint1.Models;

namespace PizzeriaSprint1.Controllers
{
    public class Customer1Controller : Controller
    {

        // GET: Customer1
       
        public ActionResult Index()
        {
            using (PizzeriaEntities1 db=new PizzeriaEntities1())
            {
                return View(db.PizzaTables.ToList());
            }          
        }
        [HttpPost]
        public ActionResult Index(string searching)
        {
            using (PizzeriaEntities1 db = new PizzeriaEntities1())
            {
                return View(db.PizzaTables.Where(Model => Model.Pizzaname.StartsWith(searching) || searching == null).ToList());
            }
        }
    }
}