using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameEngine;

namespace fias.Controllers
{
    public class HomeController : Controller
    {
        // skapar en lista av Spel
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult JoinGameView()
        {
         

            return View();
        }
        
        public ActionResult GameChecker(string name = null)
        {
            if (name == null || string.IsNullOrWhiteSpace(name))
            {
                return RedirectToAction("CreateGame","Home");
            }
            else
            {
              
                Database.InsertKeyInDataBase();

                return RedirectToAction("GameStarter", "Home", new { Gamekey = Database.generatedKey });
            }
        }

        public ActionResult CreateGame()
        {

            return View();
        }

        public ActionResult GameStarter(string Gamekey)
        {
            ViewBag.Key = Gamekey;
            return View();
        }

     
    }

}
