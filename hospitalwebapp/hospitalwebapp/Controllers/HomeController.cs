using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hospitalwebapp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About us";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Title = "Our contact";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            return View();
        }

        public ActionResult ForgotPassword()
        {
            ViewBag.Title = "Forgot Password";
            return View();
        }
    }
}