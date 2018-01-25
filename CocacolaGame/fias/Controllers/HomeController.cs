using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameEngine;
using System.Data;

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
          
                if (Request.Form["name"] == null || string.IsNullOrWhiteSpace(Request.Form["name"]))
                {
                    return RedirectToAction("JoinGameView", "Home");
                }
                else if (Request.Form["GameKey"] == null || string.IsNullOrWhiteSpace(Request.Form["GameKey"]))
                {
                    return RedirectToAction("JoinGameView", "Home");
                }
                else if (Database.Exists("GamePlayer", "KeyID", Request.Form["GameKey"], SqlDbType.VarChar))
            
                {

                    try
                    {

                        string[] Columns = new string[2] { "KeyID", "Nickname" };
                        string[] Values = new string[2] { Request.Form["GameKey"], Request.Form["name"] };
                        SqlDbType[] Datatypes = new SqlDbType[2] { SqlDbType.VarChar, SqlDbType.VarChar };
                        Database.InsertToDataBase("GamePlayer", Columns, Values,Datatypes);
                        return RedirectToAction("GamestarterGuest", "Home");
                    }
                    catch
                    {
                        return RedirectToAction("JoinGameView", "Home");
                    }
                }
            
            else
            {
                return RedirectToAction("JoinGameView", "Home");
            }
        
        }


        public ActionResult JoinGameView( )
        {
                return View();
           
        }
        
        public ActionResult GameController()
        {
            if (Request.Form["name"] == null || string.IsNullOrWhiteSpace(Request.Form["name"]))
            {
                return RedirectToAction("CreateGame", "Home");
            }
           
            else
            {
                try
                {
                    string Gamekey = Database.CreateGameKey();
                    string[] Columns = new string[2] { "KeyID", "NicknameID" };
                    string[] Vales = new string[2] { Gamekey, Request.Form["name"] };
                    SqlDbType[] Datatypes = new SqlDbType[2] { SqlDbType.VarChar, SqlDbType.VarChar };
                    Database.InsertToDataBase("GamePlayer", Columns, Vales,Datatypes);

                    return RedirectToAction("GameStarter", "Home", new { Gamekey = Gamekey });
                }
                catch
                {
                    return RedirectToAction("CreateGame", "Home");
                }
            }
        }
        //hejhej

        public ActionResult CreateGame()
        {

            return View();
        }

        public ActionResult GameStarter(string Gamekey)
        {
            if (Database.Exists("GamePlayer", "KeyID", Gamekey,SqlDbType.VarChar) == true)
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
