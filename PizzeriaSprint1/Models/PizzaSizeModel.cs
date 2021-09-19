using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzeriaSprint1.Models
{
    /// <summary>
    /// This class is created to have different sizes of pizzas.
    /// </summary>
    public class PizzaSizeModel
    {
        public int index { get; set; }
        public String Size { get; set; }
        public int Price { get; set; }
        public bool isChecked { get; set; }
    }
}