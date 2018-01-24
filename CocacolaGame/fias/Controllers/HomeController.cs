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
        public ActionResult JoinGame()
        {
            string str = Request.Form["name"];
            if (Request.Form["name"] == "k")
            {
                return RedirectToAction("CreateGame", "Home");
            }
            else
            {
                 return RedirectToAction("CreateGame", "Home");

            }
        }


        public ActionResult JoinGameView( )
        {
                return View();
           
        }
        
        public ActionResult GameController(string name = null)
        {
            if (name == null || string.IsNullOrWhiteSpace(name))
            {
                return RedirectToAction("CreateGame","Home");
            }
            else if(Database.Exists("Player", "Nickname", name))
            {
                return RedirectToAction("CreateGame", "Home");
            }
            else
            {
                Database.Insert("Player", "Nickname", name); 
                Database.InsertKeyInDataBase();
                Database.InsertToJoin("GamePlayer", "KeyID", "NicknameID", Database.generatedKey, name);

                return RedirectToAction("GameStarter", "Home", new { Gamekey = Database.generatedKey });
            }
        }
        //hejhej

        public ActionResult CreateGame()
        {

            return View();
        }

        public ActionResult GameStarter(string Gamekey)
        {
            if (Database.Exists("Game", "[Key]", Gamekey) == true)
            {
                ViewBag.Key = Gamekey;
                return View();
            }
            else
            {
                return RedirectToAction("CreateGame");
            }

        }


    }

}
