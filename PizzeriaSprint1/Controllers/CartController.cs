using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaSprint1.Models;

namespace PizzeriaSprint1.Controllers
{

    // GET: Cart
    /// <summary>
    /// This class helps to manage pizza cart operations by employee
    /// </summary>
    public class CartController : Controller
    {
        /// <summary>
        /// This GET method redirects to Pizza Home Page displaying all Pizzas available for buying.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This GET method redirects to Cart Home Page displaying all Pizzas which were added to 
        /// the cart, we can again go back to Pizza home page to add more items.
        /// </summary>
        /// <returns></returns>
        public ActionResult Buy(int id)
        {
            PizzaModel pizzaModel = new PizzaModel();
            if (Session["cart"] == null)
            {
                List<item> cart = new List<item>();
                cart.Add(new item { Pizza = pizzaModel.FindById(id), Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<item> cart = (List<item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new item { Pizza = pizzaModel.FindById(id), Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// This method checks whether a Pizza with given pizzaId exists in Database or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int isExist(int id)
        {
            List<item> cart = (List<item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Pizza.PizzaId.Equals(id))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// This method removes the pizza from the Pizza cart if user does not want to buy it.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Remove(int id)
        {
            List<item> cart = (List<item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
    }
}
