using QuizApp.Models;
using QuizApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuizApp.Controllers
{
   
    public class AccountController : Controller
    {

        private QuizDBEntities quizDB;
        // GET: Account

        public AccountController()
        {
            quizDB = new QuizDBEntities();



        }
    

    public ActionResult Index()
        {
            

            return View();
        }
    

    [HttpPost]
    public ActionResult Index(AdminViewModel objadminViewModel)
    {
            if (ModelState.IsValid)
            {
                Admin admin = quizDB.Admins.SingleOrDefault(model => model.UserName==objadminViewModel.UserName);
                if(admin == null)
                {
                    ModelState.AddModelError(string.Empty,"email is not exsist");

                }
                else if(admin.UserPassword != objadminViewModel.UserPassword)
                {
                    ModelState.AddModelError(string.Empty, "password is not valid");
                }
                else
                {

                    FormsAuthentication.SetAuthCookie(objadminViewModel.UserName, false);
                    var authticket = new FormsAuthenticationTicket(1,admin.UserName, DateTime.Now, DateTime.Now.AddMinutes(20),false,"Admin");
                    string encryptTicket = FormsAuthentication.Encrypt(authticket);
                    var authcookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                    HttpContext.Response.Cookies.Add(authcookie);
                    return RedirectToAction("Index","Admin");
                }



            }

        return View();
    }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}





