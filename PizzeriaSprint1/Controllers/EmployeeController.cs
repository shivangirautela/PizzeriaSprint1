
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzeriaSprint1.Models;

namespace Crudoperationsbyadmin_employee.Controllers
{
    public class EmployeeController : Controller
    {
       
        PizzeriaEntities1 dbobj = new PizzeriaEntities1();
        public ActionResult Employee(EmployeeTable obj)
        {
                return View(obj);
        }
        /// <summary>
        /// This method allow admin to add new employee
        /// </summary>
        [HttpPost]
        public ActionResult AddEmployee(EmployeeTable model)
        {
            if (ModelState.IsValid)
            {
                EmployeeTable obj = new EmployeeTable();
                obj.UserName = model.UserName;
                obj.Password = model.Password;
                obj.FName = model.FName;
                obj.LName = model.LName;
                obj.Address = model.Address;
                obj.PhoneNo = model.PhoneNo;
                obj.City = model.City;
                obj.Email = model.Email;

               if(model.EmployeeId==0)
                {
                    dbobj.EmployeeTables.Add(obj);
                    dbobj.SaveChanges();
                }
                else
                {
                    obj.EmployeeId = model.EmployeeId;
                    dbobj.Entry(obj).State = EntityState.Modified;
                   
                    dbobj.SaveChanges();
                }
            }
            ModelState.Clear();

            return View("Employee");
        }
        /// <summary>
        /// This method displays all the information related to employees in the table format
        /// </summary>
        /// <returns></returns>

        public ActionResult EmployeeList()
        {
            var res = dbobj.EmployeeTables.ToList();
            return View(res);
        }
        /// <summary>
        /// This method allow admin to delete the employee from the database
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
      
        public ActionResult Delete(int EmployeeId)
        {
            var res = dbobj.EmployeeTables.Where(x => x.EmployeeId == EmployeeId).First();
            dbobj.EmployeeTables.Remove(res);
            dbobj.SaveChanges();
            var List = dbobj.EmployeeTables.ToList();
            return View("EmployeeList", List);


        }
    
    }
}