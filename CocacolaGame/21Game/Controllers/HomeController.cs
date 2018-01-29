using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameEngine;

namespace _21Game.Controllers
{
    public class HomeController : Controller
    {
        private Game GetGame { get; set; }
        // GET: Home
      
        public ActionResult GameCreator( )
        {

            try
            {
                if (!StringController.IsEmpty(Request.Form["NameInput"]))
                {
                    GetGame = new Game(DataBase.CreateGameKey());
                    GetGame.InsertPlayer(Request.Form["NameInput"], Game._IsAdmin.Yes);
                    HttpCookie[] cookie = new HttpCookie[] { new HttpCookie("GameKey"), new HttpCookie("Player") , new HttpCookie("AuthID") };
                    string authId = Guid.NewGuid().ToString();
                    cookie[0].Value = GetGame.GetGameKey;
                    cookie[1].Value = GetGame._CurrentPlayer.Nickname;
                    cookie[2].Value = authId;
                    Session["AuthID"] = authId ;
                        foreach (HttpCookie c in cookie)
                    {
                        Response.Cookies.Add(c);
                    }
                    /*new { GameKey= GetGame.GetGameKey, Player = GetGame._CurrentPlayer.Nickname }*/

                    return RedirectToAction("GameLobby", "Home");
                }
                else
                {
                    return RedirectToAction("NewGame", "Home");

                }
            }
            catch(Exception e)
            {
                string message = e.ToString();
                return RedirectToAction("NewGame", "Home");
            }
           
  
              
        }

        public ActionResult NewGame()
        {
            
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult JoinGame()
        {
            return View();
        }

        public ActionResult GameLogin()
        {
            try
            {
                if (!StringController.IsEmpty(Request.Form["NameInput"]) && !StringController.IsEmpty(Request.Form["GameKey"]))
                {
                    GetGame = new Game(Request.Form["Gamekey"]);
                    if (GetGame.InsertPlayer(Request.Form["NameInput"], Game._IsAdmin.NO))
                        {
                        HttpCookie[] cookie = new HttpCookie[] { new HttpCookie("GameKey"), new HttpCookie("Player"), new HttpCookie("AuthID") };
                        string authId = Guid.NewGuid().ToString();
                        cookie[0].Value = GetGame.GetGameKey;
                        cookie[1].Value = GetGame._CurrentPlayer.Nickname;
                        cookie[2].Value = authId;
                        Session["AuthID"] = authId;
                        foreach (HttpCookie c in cookie)
                        {
                            Response.Cookies.Add(c);
                        }
                        /*new { GameKey= GetGame.GetGameKey, Player = GetGame._CurrentPlayer.Nickname }*/

                        return RedirectToAction("GameLobby", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception e)
            {
                string message = e.ToString();
                return RedirectToAction("NewGame", "Home");
            }

        }

        public ActionResult GameLobby()
        {
            try
            {
                if (Request.Cookies["AuthID"].Value == Session["AuthID"].ToString())
                {
                    if (Request.Cookies["GameKey"] != null && Request.Cookies["Player"] != null)
                    {
                        HttpCookie cookiekey = HttpContext.Request.Cookies.Get("GameKey");
                        HttpCookie cookieUser = HttpContext.Request.Cookies.Get("Player");
                         GetGame = new Game(cookiekey.Value.ToString());
                        GetGame.GetCurrentPlayer(cookieUser.Value.ToString());
                        GetGame.ReadGameStatus(GetGame.GetGameKey);
                        GetGame.ReadGameStatus(cookiekey.Value.ToString());
                        if (GetGame.GameStatus == 0)
                        {
                            return View(GetGame);
                        }
                        else
                        {
                            return RedirectToAction("RunGame", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }

            //string GameKey, string Player
          
          
            
           
          
           
        }

        public ActionResult RunGame()
        {
            try
            {
                if (Request.Cookies["AuthID"].Value == Session["AuthID"].ToString())
                {
                    if (Request.Cookies["GameKey"] != null && Request.Cookies["Player"] != null)
                    {
                        HttpCookie cookiekey = HttpContext.Request.Cookies.Get("GameKey");
                        HttpCookie cookieUser = HttpContext.Request.Cookies.Get("Player");
                        GetGame = new Game(cookiekey.Value.ToString());
                        GetGame.GetCurrentPlayer(cookieUser.Value.ToString());
                        GetGame.ReadGameStatus(GetGame.GetGameKey);
                        GetGame.ReadGameStatus(GetGame.GetGameKey);
                        GetGame.ChangeGameStatus(true);
                        
                        return View(GetGame);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
            }
            catch
            {
                return RedirectToAction("Index");
            }
           
        }


    }
}