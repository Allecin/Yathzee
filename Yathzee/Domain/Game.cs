using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    //This class defines the game table. Created games get defaultvalues that are initialized in the constructor.
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        public GameState GameState { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey("Inviter")]
        public int InviterId { get; set; }
        public virtual Player Inviter { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Player Member { get; set; }

        [ForeignKey("Turn")]
        public int TurnId { get; set; }
        public virtual Player Turn { get; set; }

        public virtual ICollection<GameScore> GameScores { get; set; }

        public Game()
        {
            CreateDate = DateTime.Now;
            GameState = GameState.Invited;
        }
    }
}
