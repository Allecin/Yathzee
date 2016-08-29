using BL;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using ViewModels.GameModel;

namespace Yathzee.Controllers
{
    //Controller for managing a game. Showing the game, playing a turn etc..
    [Authorize]
    public class GameController : Controller
    {
        public ActionResult ShowGame(int gameId, int playerId, bool turnAtPlayer, bool newGame)
        {
            Session["GameId"] = gameId;
            var gameMgr = new GameManager();

            if (newGame)
            {
                gameMgr.ChangeGameStatus(gameId, GameState.Started);
            }

            GameViewModel model = gameMgr.ShowGame(gameId, playerId, turnAtPlayer);
            if (new GameScoresManager().GameEnded(model.GameId, model.MyId ))
            {
                return RedirectToAction("EndGame");
            }

            TempData["gameId"] = gameId;

            return View(model);
        }

        public ActionResult PlayTurn(OptionId optionId, int optionValue)
        {
            var gameIdd = (int)Session["GameId"];
            var option = new Option(optionId);
            option.ScoreValue = optionValue;

            if (Session["PlayerId"] == null)
            {
                RedirectToAction("Index", "Home");
            }

            int playerId = (int)Session["PlayerId"];

            //Change turns
            new GameManager().ChangeGameTurn(gameIdd);

            //Add move
            new GameScoresManager().AddMove(gameIdd, playerId, option);

            //Reload game with new move
            return RedirectToAction("ShowGame", new { gameId = gameIdd, playerId = playerId, turnAtPlayer = false, newGame = false } );
            //ShowGame(gameIdd, playerId, false, false);
            
        }

        public ActionResult EndGame()
        {
            var gameIdd = (int)Session["GameId"];
            
            if (Session["PlayerId"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            int playerId = (int)Session["PlayerId"];

            //change game status
            new GameManager().ChangeGameStatus(gameIdd, GameState.Ended);

            //update profiles
            new PlayerManager().UpdateProfiles(gameIdd, playerId);

            //Go to my games page
            return RedirectToAction("ShowGame", new { gameId = gameIdd, playerId = playerId, turnAtPlayer = false, newGame = false });
        }

        public ActionResult ShowEndedGame(int gameId, int playerId)
        {
            GameViewModel model = new GameManager().ShowEndedGame(gameId, playerId);

            return View(model);
        }

        [HttpPost]
        public ActionResult _UpdatePossibleScores(List<int> valueDices)
        {
            var model = new List<Option>();
            var dices = new List<IDice>();

            foreach (int value in valueDices)
            {
                dices.Add(new Dice
                {
                    Number = value,
                    Locked = false
                });
            }
            model = new PlayManager().GetAllOptions(dices);
            return PartialView("_UpdatePossibleScores", model);
        }

        public ActionResult Indexx()
        {

            #region
            //var play = new PlayManager();

            //var dices = new List<IDice>();
            //dices.Add(new Dice
            //{
            //    Number = 2,
            //    Locked = false
            //});
            //dices.Add(new Dice
            //{
            //    Number = 5,
            //    Locked = false
            //});
            //dices.Add(new Dice
            //{
            //    Number = 5,
            //    Locked = false
            //});
            //dices.Add(new Dice
            //{
            //    Number = 2,
            //    Locked = false
            //});
            //dices.Add(new Dice
            //{
            //    Number = 5,
            //    Locked = false
            //});
            //TempData["options"] = play.GetAllOptions(dices);
            #endregion

            List<IDice> dices = new List<IDice>();
            string nummers = "";

            for (int i = 0; i < 5; i++)
            {
                Dice d = new Dice();
                dices.Add(d);
                nummers += d.Number + " ";
            }

            TempData["nummer"] = "5";

            return View();
        }
    }
}