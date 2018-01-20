using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using GameEngine;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
           

            return View();
           
            
        }

        //public ActionResult StartWindow()
        //{
        //    return View();
            
        //}

        public string Login (string userName, string passWord)
        {
            User z = new User();
            var model = z.ReturnList().Where(x => x.UserName == userName && x.Password == passWord).SingleOrDefault();

            //Create Cookies
            HttpCookie UserCookie = new HttpCookie("user", model.Id.ToString());

            //Expire Date
            UserCookie.Expires.AddDays(14);

            //Save data at Cookies
            HttpContext.Response.SetCookie(UserCookie);

            //Get user data from Cookie
            HttpCookie NewCookie = Request.Cookies["user"];

            return NewCookie.Value;

        }

        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}