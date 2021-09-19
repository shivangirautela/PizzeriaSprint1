using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PizzeriaSprint1.Models;

namespace PizzeriaSprint1.Controllers
{
    public class Home1Controller : Controller
    {
        // GET: Home1
        public ActionResult AdminLogin()
        {
            return View();
        }
        public ActionResult ALogin()
        {
            return View();
        }
        /// <summary>
        /// THis method will allow admin to login to the application
        /// when correct username or password is given admin will login to the application .
        /// else it shows errormessage.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnurl"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ALogin(Login model, string returnurl)
        {
            PizzeriaEntities1 db = new PizzeriaEntities1();
            var dataitem = db.Logins.Where(x => x.UserName == model.UserName && x.Password == model.Password).First();
            if (dataitem != null)
            {
                FormsAuthentication.SetAuthCookie(dataitem.UserName, false);
                if (Url.IsLocalUrl(returnurl) && returnurl.Length > 1 && returnurl.StartsWith("/")
                    && !returnurl.StartsWith("//") && !returnurl.StartsWith("/\\"))
                {
                    return Redirect(returnurl);
                }
                else
                {
                    return RedirectToAction("AdminLogin");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }
        [Authorize]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("ALogin", "Home1");
        }
        PizzeriaEntities1 db = new PizzeriaEntities1();


        public ActionResult DataPizza()
        {
            var data = db.PizzaTables.ToList();
            return View(data);
        }
        /// <summary>
        /// This method allow admin to add pizza and the create view will be displayed on screen 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PizzaTable p)
        {
            string fileName = Path.GetFileNameWithoutExtension(p.imageFile.FileName);
            string extension = Path.GetExtension(p.imageFile.FileName);
            HttpPostedFileBase PostedFile = p.imageFile;
            int length = PostedFile.ContentLength;
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (length <= 1000000)
                {
                    fileName = fileName + extension;
                    p.ImageUrl = "~/images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                    p.imageFile.SaveAs(fileName);
                    db.PizzaTables.Add(p);
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["CreateMessage"] = "<script>alert('Data Inserted Successfully')</script>";
                        ModelState.Clear();
                        return RedirectToAction("DataPizza", "Home1");
                    }
                    else
                    {
                        TempData["CreateMessage"] = "<script>alert('Data Not Inserted')</script>";
                    }
                }
                else
                {
                    TempData["SizeMessage"] = "<script>alert('image size must be less than 1MB')</script>";
                }
            }
            return View();
        }
        /// <summary>
        /// This method will allow admin to edit details of  pizza in database 
        /// after editing when we click on save button it redirects to datapizza view.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var PizzaRow = db.PizzaTables.Where(model => model.PizzaId == id).FirstOrDefault();
            Session["Image"] = PizzaRow.ImageUrl;
            return View(PizzaRow);
        }
        [HttpPost]
        public ActionResult Edit(PizzaTable p)
        {


            db.Entry(p).State = EntityState.Modified;
            int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["UpdateMessage"] = "<script>alert('Data updated Successfully')</script>";
                ModelState.Clear();
                return RedirectToAction("DataPizza", "Home1");

            }
            else
            {
                TempData["UpdateMessage"] = "<script>alert('Data Not Updated')</script>";
            }
            return View();
        }
        /// <summary>
        /// THis method will allow admin to delete pizza from database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var PizzaRow = db.PizzaTables.Where(model => model.PizzaId == id).FirstOrDefault();
                if (PizzaRow != null)
                {
                    db.Entry(PizzaRow).State = EntityState.Deleted;
                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        TempData["DeleteMessage"] = "<script>alert('Data Deleted Successfully')</script>";
                    }
                    else
                    {
                        TempData["DeleteMessage"] = "<script>alert('Data not Deleted')</script>";

                    }
                }
            }
            return RedirectToAction("DataPizza", "Home1");
        }
    }
}