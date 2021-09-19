using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PizzeriaSprint1.Models;

namespace PizzeriaSprint1.Controllers
{
    public class SearchPizzasController : Controller
    {
        // GET: SearchRecords
        public ActionResult Index(string search)
        {
            IEnumerable<PizzaTable> obj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("http://localhost:62504/api/");

            var consumeapi = hc.GetAsync("PizzaSearchAPI?search=" + search);
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<PizzaTable>>();
                displaydata.Wait();
                obj = displaydata.Result;

            }
            return View(obj);
        }
    }
}