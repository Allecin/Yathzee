using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Yathzee.Models
{
    //Model for the view page of a plqyers profile.
    public class ProfileViewModel
    {
        [Key]
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [Display(Name = "Privacy settings")]
        public Privacy Privacy { get; set; }

        [Display(Name = "Games played")]
        public int GamesPlayed { get; set; }

        [Display(Name = "Games won")]
        public int GamesWon { get; set; }

        [Display(Name = "Maximal score")]
        public int MaxScore { get; set; }

        [Display(Name = "Average game score")]
        public int AverageScore { get; set; }
    }
}