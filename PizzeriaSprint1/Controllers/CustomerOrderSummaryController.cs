using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaSprint1.Models;

namespace PizzeriaSprint1.Controllers
{
    public class CustomerOrderSummaryController : Controller
    {
        PizzeriaEntities1 entityObj = new PizzeriaEntities1();
        /// <summary>
        /// This post method takes customer object from Index/Order page and then places order
        /// for that customer. 
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="totalprice"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(Customer customer,string totalprice)
        {
            List<item> items = (List<item>)Session["cart"];
            int id = items[0].Pizza.PizzaId;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Pizza.PizzaId > id)
                    id = items[i].Pizza.PizzaId;
            }
            int empId;
            if (Convert.ToInt32(Session["username"]) != 0)
                empId = Convert.ToInt32(Session["username"]);
            else
                empId = 0;
            Session["cartprice"] = Convert.ToInt32(totalprice);
            CustomerOrder newOrder = new CustomerOrder()
            {
                CustomerId = Convert.ToInt32(Session["cust_id"]),
                EmployeeId = empId,
                DeliveryDate = DateTime.Now,
                OrderStatus = "Preparing",
                TotalPrice = Convert.ToDecimal(Session["cartprice"]),
                PizzaId = id
            };
            Session["cart"] = null;
            this.entityObj.CustomerOrders.Add(newOrder);
            this.entityObj.SaveChanges();
            return View(newOrder);

        }
    }
}