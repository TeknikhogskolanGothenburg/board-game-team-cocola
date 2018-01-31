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
        private Game CurrentGame { get; set; }
        // GET: Home

        public ActionResult GameCreator()
        {

            try
            {
                if (!StringController.IsEmpty(Request.Form["NameInput"]))
                {
                    CurrentGame = new Game(DataBase.CreateGameKey());
                    CurrentGame.InsertPlayer(Request.Form["NameInput"], Game._IsAdmin.Yes);
                    CurrentGame.ReadPlayers(CurrentGame.GetGameKey);
                    HttpCookie[] cookie = new HttpCookie[] { new HttpCookie("GameKey"), new HttpCookie("Player"), new HttpCookie("AuthID") };
                    string authId = Guid.NewGuid().ToString();
                    cookie[0].Value = CurrentGame.GetGameKey;
                    cookie[1].Value = CurrentGame._CurrentPlayer.Nickname;
                    cookie[2].Value = authId;
                    Session["AuthID"] = authId;
                    foreach (HttpCookie c in cookie)
                    {
                        Response.Cookies.Add(c);
                    }
                    /*new { GameKey= CurrentGame.GetGameKey, Player = CurrentGame._CurrentPlayer.Nickname }*/
                   

                    return RedirectToAction("GameLobby", "Home");
                }
                
                else
                {
                    return RedirectToAction("NewGame", "Home");

                }
            }
            catch (Exception e)
            {
                ViewBag.message = e.ToString();
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
                    CurrentGame = new Game(Request.Form["Gamekey"]);
                    if (CurrentGame.InsertPlayer(Request.Form["NameInput"], Game._IsAdmin.NO))
                    {
                        HttpCookie[] cookie = new HttpCookie[] { new HttpCookie("GameKey"), new HttpCookie("Player"), new HttpCookie("AuthID") };
                        string authId = Guid.NewGuid().ToString();
                        cookie[0].Value = CurrentGame.GetGameKey;
                        cookie[1].Value = CurrentGame._CurrentPlayer.Nickname;
                        cookie[2].Value = authId;
                        Session["AuthID"] = authId;
                        foreach (HttpCookie c in cookie)
                        {
                            Response.Cookies.Add(c);
                        }
                        /*new { GameKey= CurrentGame.GetGameKey, Player = CurrentGame._CurrentPlayer.Nickname }*/

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
                        CurrentGame = new Game(cookiekey.Value.ToString());
                        CurrentGame.GetCurrentPlayer(cookieUser.Value.ToString());
                        CurrentGame.ReadGameStatus(CurrentGame.GetGameKey);
                        CurrentGame.ReadGameStatus(cookiekey.Value.ToString());
                        
                        if (CurrentGame.GameStatus == 0)
                        {
                            return View(CurrentGame);
                        }
                        else
                        {
                            CurrentGame.StartNewRound(true);
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
                        CurrentGame = new Game(cookiekey.Value.ToString());
                        CurrentGame.GetCurrentPlayer(cookieUser.Value.ToString());
                        CurrentGame.ChangeGameStatus(true);
                        CurrentGame.ReadGameStatus(CurrentGame.GetGameKey);
                        CurrentGame.ReadGameRound();
                    
                       

                        return View(CurrentGame);
                        
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

        public ActionResult PlayerStand()
        {
            try
            {

                if (Request.Cookies["GameKey"] != null && Request.Cookies["Player"] != null)
                {
                    if (Request.Cookies["AuthID"].Value == Session["AuthID"].ToString())
                    {
                        HttpCookie cookiekey = HttpContext.Request.Cookies.Get("GameKey");
                        HttpCookie cookieUser = HttpContext.Request.Cookies.Get("Player");
                        CurrentGame = new Game(cookiekey.Value.ToString());
                        CurrentGame.GetCurrentPlayer(cookieUser.Value.ToString());
                        CurrentGame.ReadGameStatus(CurrentGame.GetGameKey);
                        if (CurrentGame.GameStatus == 1)
                        {
                         
                            CurrentGame.StayWiththeCurrentNumber(true);
                            return RedirectToAction("RunGame");
                        }
                        return RedirectToAction("RunGame");

                    }
                    return RedirectToAction("RunGame");
                }
                return RedirectToAction("RunGame");
            }
            catch
            {
                 return RedirectToAction("RunGame");
            }
            
            
            }
        //public ActionResult roundend()
        //{
        //    try
        //    {

        //        if (Request.Cookies["GameKey"] != null && Request.Cookies["Player"] != null)
        //        {
        //            if (Request.Cookies["AuthID"].Value == Session["AuthID"].ToString())
        //            {
        //                HttpCookie cookiekey = HttpContext.Request.Cookies.Get("GameKey");
        //                HttpCookie cookieUser = HttpContext.Request.Cookies.Get("Player");
        //                CurrentGame = new Game(cookiekey.Value.ToString());
        //                CurrentGame.GetCurrentPlayer(cookieUser.Value.ToString());
        //                CurrentGame.ReadGameStatus(CurrentGame.GetGameKey);
                        
                        
        //                    CurrentGame.StartNewRound(true);
        //                    return RedirectToAction("RunGame");
                        
        //                return RedirectToAction("Index");

        //            }
        //            return RedirectToAction("Index");
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

            public ActionResult NewRound()
        {
            try
            {

                if (Request.Cookies["GameKey"] != null && Request.Cookies["Player"] != null)
                {
                    if (Request.Cookies["AuthID"].Value == Session["AuthID"].ToString())
                    {
                        HttpCookie cookiekey = HttpContext.Request.Cookies.Get("GameKey");
                        HttpCookie cookieUser = HttpContext.Request.Cookies.Get("Player");
                        CurrentGame = new Game(cookiekey.Value.ToString());
                        CurrentGame.GetCurrentPlayer(cookieUser.Value.ToString());
                        CurrentGame.ReadGameStatus(CurrentGame.GetGameKey);
                        CurrentGame.ReadGameRound();
                        if (CurrentGame.GameRound == 1 && CurrentGame._CurrentPlayer.IsAdmin== 1)
                        {
                            CurrentGame.StartNewRound(true);
                            return RedirectToAction("RunGame");
                        }
                        return RedirectToAction("Index");

                    }
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }


        }
















        public ActionResult GetCard()
                    {
                        try
                        {

                            if (Request.Cookies["GameKey"] != null && Request.Cookies["Player"] != null)
                            {
                                if (Request.Cookies["AuthID"].Value == Session["AuthID"].ToString())
                                {
                                    HttpCookie cookiekey = HttpContext.Request.Cookies.Get("GameKey");
                                    HttpCookie cookieUser = HttpContext.Request.Cookies.Get("Player");
                                    CurrentGame = new Game(cookiekey.Value.ToString());
                                    CurrentGame.GetCurrentPlayer(cookieUser.Value.ToString());
                                    CurrentGame.ReadGameStatus(CurrentGame.GetGameKey);
                                    if (CurrentGame.GameStatus == 1)
                                    {
                                        if (CurrentGame._CurrentPlayer.Attemp > 0)
                                        {
                                            CurrentGame.GetCard();
                                            return RedirectToAction("RunGame");
                                        }
                                        else
                                        {
                                            return RedirectToAction("PlayerStand");
                                        }
                                    }
                                    return RedirectToAction("Index");
                                }
                                return RedirectToAction("Index");

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















