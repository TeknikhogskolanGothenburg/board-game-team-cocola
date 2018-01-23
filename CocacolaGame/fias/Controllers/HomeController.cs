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

        public ActionResult GameChecker(string name = null)
        {
            if (name == null)
            {
                return RedirectToActionPermanent("CreateGame");
            }
            else
            {
                Database Game = new Database();
                Game.InsertKeyInDataBase();

                return RedirectToAction("GameStarter", "Home", new { Gamekey = Game.generatedKey });
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
