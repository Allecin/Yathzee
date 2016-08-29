using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    //Viewmodel for the scores of a game per player 
    public class GameScoreExtra
    {
        public GameScore GameScore { get; set; }
        public int NumberTotal { get; set; }
        public int Bonus { get; set; }
        public int UpperTotal { get; set; }
        public int LowerTotal { get; set; }
    }
}

