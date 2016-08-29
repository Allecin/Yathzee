using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    //This class defines the player table. Created players get defaultvalues that are initialized in the constructor.
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public byte[] Avatar { get; set; }

        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int MaxScore { get; set; }
        public int AverageScore { get; set; }
        public Privacy Privacy { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<GameScore> GameScores { get; set; }

        public Player()
        {
            GamesPlayed = 0;
            GamesWon = 0;
            MaxScore = 0;
            AverageScore = 0;
            Privacy = Privacy.Private;
        }
    }
}
