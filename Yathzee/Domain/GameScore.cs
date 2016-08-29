using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    //This class defines the gameScore table. Created scores get defaultvalues that are initialized in the constructor.
    public class GameScore
    {
        [Key]
        public int GameScoreId { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public int ScoreAces { get; set; }
        public int ScoreTwos { get; set; }
        public int ScoreThrees { get; set; }
        public int ScoreFours { get; set; }
        public int ScoreFives { get; set; }
        public int ScoreSixes { get; set; }
        public int ScoreThreeOfAKind { get; set; }
        public int ScoreFourOfAKind { get; set; }
        public int ScoreFullHouse { get; set; }
        public int ScoreSmallStraight { get; set; }
        public int ScoreLargeStraight { get; set; }
        public int ScoreYathzee { get; set; }
        public int ScoreChance { get; set; }
        public int ScoreTotal { get; set; }

        public GameScore()
        {
            ScoreAces = 0;
            ScoreTwos = 0;
            ScoreThrees = 0;
            ScoreFours = 0;
            ScoreFives = 0;
            ScoreSixes = 0;
            ScoreThreeOfAKind = 0;
            ScoreFourOfAKind = 0;
            ScoreSmallStraight = 0;
            ScoreLargeStraight = 0;
            ScoreYathzee = 0;
            ScoreChance = 0;
        }
    }
}
