using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameEngine;

namespace CocacolaGame.Controllers
{
    public class HomeController : Controller
    {
         //private static List<Ludo> 
        public ActionResult Index()
        {
            Ludo ludoGame = new Ludo();
            
            return View(ludoGame.GameStateModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
        

            return View();
        }

        public ActionResult Page1()
        {
            
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            

            return View();
        }

        
 
    }
}