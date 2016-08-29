using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    //Model for all the games of a player.
    public class MyGamesViewModel
    {
        public IList<GameInfo> InviterGames { get; set; }
        public IList<GameInfo> MemberGames { get; set; }
        public IList<GameInfo> OnGoingGames { get; set; }
        public IList<GameInfo> EndedGames { get; set; }

        public class GameInfo
        {
            public int GameId { get; set; }
            public int PlayerId { get; set; }
            public string PlayerName { get; set; }
            public int OpponentId { get; set; }
            public string OpponentName { get; set; }
            public bool PlayerWinner { get; set; }
            public bool PlayerTurn { get; set; }

        }

        public MyGamesViewModel()
        {
            InviterGames = new List<GameInfo>();
            MemberGames  = new List<GameInfo>();
            OnGoingGames = new List<GameInfo>();
            EndedGames = new List<GameInfo>();
        }
    }


}
