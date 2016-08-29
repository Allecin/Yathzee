using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    //Model for a view to require information for starting a new game.
    public class NewGameViewModel
    {
        [Key]
        public int NewGameId { get; set; }

        [Display(Name = "Your friends: ")]
        public IList<PlayerInfo> PlayersInfo { get; set; }

        [Display(Name = "Email address of player you want to challenge: ")]
        public string EmailInviter { get; set; }

        public class PlayerInfo
        {
            public int PlayerInfoId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
        }

        public NewGameViewModel()
        {
            EmailInviter = "";
            PlayersInfo = new List<PlayerInfo>();
        }
    }
}
