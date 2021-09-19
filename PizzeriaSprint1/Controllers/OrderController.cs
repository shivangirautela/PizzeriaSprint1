using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaSprint1.Models;

namespace PizzeriaSprint1.Controllers
{
    /// <summary>
    /// This class helps to add new Order for customer
    /// </summary>
    public class OrderController : Controller
    {
        PizzeriaEntities1 entityObj = new PizzeriaEntities1();

        /// <summary>
        /// This method redirects to page where Employee can place order for specific customer.
        /// </summary>
        /// <param name="totalprice"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OrderIndex(string totalprice)
        {
            Session["cartprice"] = Convert.ToInt32(totalprice);
            return View();
        }

        /// <summary>
        /// This method edits the data of the order placed by employee.This can be done only by employee.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            return View(this.entityObj.CustomerOrders.Where(x => x.OrderId == id).FirstOrDefault());
        }
        /// <summary>
        /// This method is redirected when we edit an object in CustomerOrder table the changes
        /// are updated in database.
        /// </summary>
        /// <param name="customerOrder"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(CustomerOrder customerOrder)
        {
            this.entityObj.Entry(customerOrder).State = System.Data.Entity.EntityState.Modified;
            this.entityObj.SaveChanges();
            return RedirectToAction("OrderSummary");
        }


        /// <summary>
        /// This get method is called when we want to View/Edit all orders summary
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderSummary()
        {
            var allOrders = (from record in this.entityObj.CustomerOrders
                             select record);
            return View(allOrders.ToList());
        }
        /// <summary>
        /// This post method takes customer object from Index/Order page and then places order
        /// for that customer. 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OrderSummary(Customer customer)
        {
                var userDetails = entityObj.Customers.Where(x => x.FirstName == customer.FirstName).FirstOrDefault();
                if (userDetails == null)
                {
                    customer.OrderErrorMessage = "Customer does not exists.";
                    return View("Index");
                }
                else
                {
                    List<item> items = (List<item>)Session["cart"];
                    int id = items[0].Pizza.PizzaId;
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].Pizza.PizzaId > id)
                            id = items[i].Pizza.PizzaId;
                    }
                CustomerOrder newOrder = new CustomerOrder()
                    {
                        CustomerId = userDetails.CustomerId,
                        EmployeeId = Convert.ToInt32(Session["username"]),
                        DeliveryDate = DateTime.Now,
                        OrderStatus = "Preparing",
                        TotalPrice = Convert.ToDecimal(Session["cartprice"]),
                        PizzaId = id
                    };
                
                    this.entityObj.CustomerOrders.Add(newOrder);
                    this.entityObj.SaveChanges();
                }
            Session["cart"] = null;
            return View(this.entityObj.CustomerOrders.ToList());
        }
    }
}