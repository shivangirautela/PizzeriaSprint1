    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzeriaSprint1.Models
{
    /// <summary>
    /// This is a model class containing object of pizza table and its quantity it also 
    /// contains size descriptions for each pizza.
    /// </summary>
    public class item
    {
        public PizzaTable Pizza { get; set; }
        public int Quantity { get; set; }

        public List<PizzaSizeModel> psm = new List<PizzaSizeModel>() {
            new PizzaSizeModel()
            { index=1, Size="Small +40Rs.",Price=40, isChecked=false},
            new PizzaSizeModel()
            { index=2, Size="Medium +70Rs.",Price=70, isChecked=false},
            new PizzaSizeModel()
            { index=3, Size="Large +100Rs.",Price=100, isChecked=false},
            };
        public item() { }

        public item(PizzaTable product, int quantity, List<PizzaSizeModel> psm)
        {
            this.Pizza = product;
            this.Quantity = quantity;
            this.psm = psm;
        }
    }
}