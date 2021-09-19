using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzeriaSprint1.Models
{
    /// <summary>
    /// This class is created to handle all Pizza 's for placing order
    /// </summary>
    public class PizzaModel
    {
        private List<PizzaTable> products;
        private PizzeriaEntities1 entityObj = new PizzeriaEntities1();
        /// <summary>
        /// This constructor initializes all Pizzas available in Database.
        /// </summary>
        public PizzaModel()
        {
            this.products = new List<PizzaTable>();
            List<PizzaTable> list = (List<PizzaTable>)this.entityObj.PizzaTables.ToList();
            for (int i = 0; i < Convert.ToInt32(list.Count); i++)
            {
                this.products.Add(list[i]);
            }
        }
        /// <summary>
        /// This method returns all pizzas available for placing order
        /// </summary>
        /// <returns></returns>
        public List<PizzaTable> AllPizzas()
        {
            return this.products;
        }
        /// <summary>
        /// This method returns a Pizza object based on it's PizzaId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PizzaTable FindById(int id)
        {
            return this.products.Single(p => p.PizzaId.Equals(id));
        }
    }

}