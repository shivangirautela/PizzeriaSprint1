using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaSprint1.Models;

namespace PizzeriaSprint1.Controllers
{
    public class Employee1Controller : Controller
    {
        
        // GET: Employee1
        /// <summary>
        /// This method redirects to login page for employee
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeM()
            {
                return View();
            }
            /// <summary>
            /// This Post method helps Employee to Login to the Website
            /// </summary>
            /// <param name="employee"></param>
            /// <returns></returns>
            [HttpPost]
            public ActionResult Authorize(EmployeeTable employee)
            {
                using (PizzeriaEntities1 db = new PizzeriaEntities1())
                {
                    var userDetails = db.EmployeeTables.Where(x => x.UserName == employee.UserName && x.Password == employee.Password).FirstOrDefault();
                    if (userDetails == null)
                    {
                        employee.LogError = "Wrong UserName or Password Entered.";
                        return View("EmployeeM", employee);
                    }
                    else
                    {
                        Session["username"] = userDetails.EmployeeId;
                        Session["emp_name"] = userDetails.FName;
                        return RedirectToAction("Dash", "Dashboard");
                    }
                }
            }
            /// <summary>
            ///  This method helps Employee to Log out of the Website
            /// </summary>
            /// <returns></returns>
            public ActionResult LogOut()
            {
                Session.Abandon();
                return RedirectToAction("EmployeeM", "Home");
            }
           
    }
}