using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.GameModel;

namespace ViewModels
{
    //Model for all the information that is needed to show a game.
    public class GameViewModel
    {
        public int GameId { get; set; }
        public bool MyTurn { get; set; }
        public string TurnName { get; set; }
        public int MyId { get; set; }
        public string MyName { get; set; }
        public string OpponentName { get; set; }
        public IList<IDice> Dices { get; set; }
        public IList<Option> Options { get; set; }
        public IList<GameScoreExtra> GameScoresExtra { get; set; }

        public GameViewModel()
        {
            TurnName = "";
            Dices = new List<IDice>();
            Options = new List<Option>();
            GameScoresExtra = new List<GameScoreExtra>();

        }
    }
}
