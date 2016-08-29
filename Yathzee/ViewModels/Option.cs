using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    //Model of all the options someone can have with certain dices.
    public class Option
    {
        public Enum OptionsId { get; set; }
        public string Text { get; set; }
        public int ScoreValue { get; set; }
        public int GameId { get; set; }

        public Option(OptionId optionsId)
        {
            this.OptionsId = optionsId;

            switch (optionsId)
            {
                case OptionId.U1:
                    Text = "Only aces";
                    ScoreValue = 1;
                    break;
                case OptionId.U2:
                    Text = "Only twos";
                    ScoreValue = 2;
                    break;
                case OptionId.U3:
                    Text = "Only threes";
                    ScoreValue = 3;
                    break;
                case OptionId.U4:
                    Text = "Only fours";
                    ScoreValue = 4;
                    break;
                case OptionId.U5:
                    Text = "Only fives";
                    ScoreValue = 5;
                    break;
                case OptionId.U6:
                    Text = "Only sixes";
                    ScoreValue = 6;
                    break;
                case OptionId.U7:
                    Text = "+ 63 = 35 Bonus";
                    ScoreValue = 35;
                    break;
                case OptionId.L1:
                    Text = "3 of a kind Total";
                    ScoreValue = 0;
                    break;
                case OptionId.L2:
                    Text = "4 of kind Total";
                    ScoreValue = 0;
                    break;
                case OptionId.L3:
                    Text = "Full house";
                    ScoreValue = 25;
                    break;
                case OptionId.L4:
                    Text = "Small straight";
                    ScoreValue = 30;
                    break;
                case OptionId.L5:
                    Text = "Large Straight";
                    ScoreValue = 40;
                    break;
                case OptionId.L6:
                    Text = "YATHZEE";
                    ScoreValue = 50;
                    break;
                case OptionId.L7:
                    Text = "Chance total";
                    ScoreValue = 0;
                    break;
                case OptionId.L8:
                    Text = "YATHZEE Bonus";
                    ScoreValue = 100;
                    break;
            }
        }
    }
}
