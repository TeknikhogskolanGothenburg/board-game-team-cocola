using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameEngine;

namespace WebApplication4.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }


        public string Login(string userName, string passWord)
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
    }
}