using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Monopoly.Web.Models;

namespace Monopoly.Web.Controllers
{
    public class BoardController : Controller
    {
        private DirectoryInfo baseDirectory;
        public MonopolyApp MonopolyApp { get; set; }

        public ActionResult Index()
        {
            if (TempData.Peek("monopoly") == null)
            {
                baseDirectory = new DirectoryInfo(HostingEnvironment.ApplicationPhysicalPath).Parent;
                var path = baseDirectory.FullName + "\\Monopoly";
                MonopolyApp = new MonopolyApp(path);
                MonopolyApp.Init();
                TempData["monopoly"] = MonopolyApp;
                TempData.Keep("monopoly");
            }
            else
            {
                MonopolyApp = TempData["monopoly"] as MonopolyApp;
            }
            const string actionMessage = "Roll the dice to start the game";
            return RedirectToAction("Game", new { actionMessage, isThrowDie = true });
        }

        public ActionResult Game(string actionMessage, bool isThrowDie)
        {
            MonopolyApp = TempData["monopoly"] as MonopolyApp;
            if (MonopolyApp == null)
                return RedirectToAction("Index");
            TempData.Keep("monopoly");
            var gameViewModel = new GameViewModel()
            {
                CurrentPlayer = MonopolyApp.GetCurrentPlayer(),
                Locations = MonopolyApp.board.locations.Values.ToList(),
                Players = MonopolyApp.Players,
                DieValue = MonopolyApp.board.CurrentDieValue,
                ActionMessage = actionMessage,
                IsThrowDie = isThrowDie

            };
            return View(gameViewModel);
        }

        public ActionResult ThrowDie()
        {
            MonopolyApp = TempData["monopoly"] as MonopolyApp;
            if (MonopolyApp == null)
            {
                return RedirectToAction("Index");
            }
            TempData.Keep("monopoly");
            var actionMessage = MonopolyApp.Game();
            return RedirectToAction("Game", new { actionMessage, isThrowDie = false });
        }

        public ActionResult EndTurn()
        {
            MonopolyApp = TempData["monopoly"] as MonopolyApp;
            TempData.Keep("monopoly");
            var actionMessage = MonopolyApp.IncreaseTurnCounter();
            return RedirectToAction("Game", new { actionMessage, isThrowDie = true });
        }

        //public ActionResult Init()
        //{
        //    var imagePath= baseDirectory.FullName + "\\Monopoly\\Monopoly.Web\\Content\\image\\Players\\";
        //    ViewBag.PlayerImagePath = imagePath;
        //    return View();
        //}
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}