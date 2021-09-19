using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PizzeriaSprint1.Models;

namespace PizzeriaSprint1.Controllers
{
    public class PizzaSearchAPIController : ApiController
    {
        public IHttpActionResult getpizzaname(string search)
        {
             PizzeriaEntities1 sd = new PizzeriaEntities1();
            var result = sd.PizzaTables.Where(x => x.Pizzaname.StartsWith(search) || search == null).ToList();
            return Ok(result);
        }
    }
}
