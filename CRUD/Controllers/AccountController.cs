using DOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.UserLogin;

namespace CRUD.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["LogedUser"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(Login Model)
        {
            if (ModelState.IsValid)
            {
                UserLoginBLL objUserLoginBLL = new UserLoginBLL();
                Login objLogin = new Login();
                objLogin = objUserLoginBLL.GetUserForLogin(Model);
                if (objLogin != null)
                {
                    Session["LogedUser"] = objLogin;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.incorrect = true;
                }

            }
            return View();
        }
    }
}