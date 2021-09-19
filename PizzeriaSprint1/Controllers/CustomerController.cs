using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PizzeriaSprint1.Models;


namespace PizzeriaSprint1.Controllers
{
    public class CustomerController : Controller
    {
        //Registration Action
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }


        //Registration POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified,ActivationCode")] Customer customer)
        {
            bool Status = false;
            string message = "";

            //Model Validation
            if (ModelState.IsValid)
            {

                #region  //Email is already exist
                var isExist = IsEmailExist(customer.CustomerEmail);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(customer);
                }
                #endregion

                #region Generate Activate Code

                customer.ActivationCode = Guid.NewGuid();

                #endregion
                #region Password Hashing
                customer.Password = Crypto.Hash(customer.Password);
                customer.ConfirmPassword = Crypto.Hash(customer.ConfirmPassword); //
                #endregion
                customer.IsEmailVerified = "false";

                #region Save to Database
                using (PizzeriaEntities1 dc = new PizzeriaEntities1())
                {
                    dc.Customers.Add(customer);
                    dc.SaveChanges();

                    //Send Email to customer
                    SendVerificationLinkEmail(customer.CustomerEmail, customer.ActivationCode.ToString());
                    message = "Registration successfully done. Account activation link " +
                        "has been sent to your email id :" + customer.CustomerEmail;
                    Status = true;

                }
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }



            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(customer);
        }

        //Verify Account
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (PizzeriaEntities1 dc = new PizzeriaEntities1())
            {
                dc.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid confirm password does not match issue on save changes
                var v = dc.Customers.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = "true";
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = Status;
            return View();
        }

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (PizzeriaEntities1 dc = new PizzeriaEntities1())
            {
                var v = dc.Customers.Where(a => a.CustomerEmail == login.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            foreach(var rec in dc.Customers.ToList())
                            {
                                if(rec.CustomerEmail ==  login.EmailID)
                                {
                                    Session["cust_id"] = rec.CustomerId;
                                    break;
                                }
                            }
                            return RedirectToAction("Index", "Customer1");
                        }
                    }
                    else
                    {
                        message = "Invalid credential provided";
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Customer");
        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (PizzeriaEntities1 dc = new PizzeriaEntities1())
            {
                var v = dc.Customers.Where(a => a.CustomerEmail == emailID).FirstOrDefault();
                return v != null;

            }
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode)
        {
            var verifyUrl = "/Customer/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("dotnetawesome@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "********"; // Replace with actual password
            string subject = "Your account is successfully created!";

            string body = "<br/><br/> We are excited to tell you that your Dotnet Awesome account is" + "Successfully Created...Please click on the below link to verify your account" +
                "<br/><br/><a href='" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                //smtp.Send(message)
                ;
        }
    }


}