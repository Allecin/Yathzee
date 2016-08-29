using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using ViewModels;
using Yathzee.Models;
using BL;

namespace Yathzee.Controllers
{
    //Controller for the standard pages: newgame, my games, profile etc..
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult NewGame()
        {
            if (Session["PlayerId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string playerId = Session["PlayerId"] + "";
            var playerMgr = new PlayerManager();

            NewGameViewModel model = playerMgr.GetFriendsByPlayerId(playerId);

            return View(model);
        }

        [HttpPost]
        public ActionResult NewGame(NewGameViewModel model)
        {
            var gameMgr = new GameManager();
            var playerMgr = new PlayerManager();
            var memberId = playerMgr.GetPlayerIdByEmail(model.EmailInviter);
            if (memberId < 0)
            {
                TempData["Error"] = "You entered a invalid email address, no game has been created.";
                return NewGame();
            }
            if (Session["PlayerId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int playerId = Int32.Parse(Session["PlayerId"]+"");
            if (gameMgr.CreateGame(playerId, memberId) != null)
            {
                TempData["GameCreated"] = "You have challenged a friend. See the game state in 'My games'.";
            }
            else
            {
                TempData["Error"] = "Something went wrong, no game has been created.";
            };

            return NewGame();
        }

        public ActionResult MyGames()
        {
            if (Session["PlayerId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int playerId = (int)Session["PlayerId"];
            var gameMgr = new GameManager();

            MyGamesViewModel model = gameMgr.GetOrderedGames(playerId);

            return View(model);
        }

        public ActionResult Profile(string otherPlayerId)
        {
            var playerMgr = new PlayerManager();
            var model = new ProfileViewModel();
            if (Int32.Parse(otherPlayerId) == 0)
            {
                if (Session["PlayerId"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                int playerId = (int)Session["PlayerId"];
                model = playerMgr.GetPlayerModel(playerId);
            }
            else
            {
                ModelState.Clear();
                ModelState.Remove("Name");
                ModelState.Remove("Email");
                model = playerMgr.GetPlayerModel(Int32.Parse(otherPlayerId));
            }

            return View(model);
        }

        [HttpPost]
        public string UpdateProfile(string name, bool privacy)
        {
            if (Session["PlayerId"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            int playerId = (int)Session["PlayerId"];

            new PlayerManager().UpdateProfile(playerId, name, privacy);

            if (privacy)
            {
                return name + ",Private";
            }
            else
            {
                return name + ",Global";
            }
        }

        [HttpPost]
        public bool CheckPrivacy(string email)
        {
            bool lol = new PlayerManager().CheckPrivacy(email);
            return lol;
        }

        [HttpPost]
        public int GoToProfile(string email)
        {
            var playerId = new PlayerManager().GetPlayerByEmail(email).PlayerId;

            //return RedirectToAction("Profile", new { otherPlayerId = playerId });

            return playerId;
        }
    }
}