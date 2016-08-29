using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.GameModel;

namespace BL
{
    //Manages some of the actions during a game. (other part is in javascript)
    public class PlayManager
    {
        public List<Option> Options { get; set; }

        public PlayManager()
        {
            Options = new List<Option>();
        }

        public List<Option> GetAllOptions(List<IDice> dices)
        {
            AddAllNumberOptions(dices);
            AddOfKindsOptions(dices);
            AddStraightOptions(dices);
            AddFullHouseOptions(dices);

            return Options;
        }

        //Adds options from 1 to 6 in the options-list 
        private void AddAllNumberOptions(List<IDice> dices)
        {
            for (int i = 1; i<=6; i++)
            {
                int total = 0;
                foreach (Dice d in dices)
                {
                    if (d.Number == i)
                    {
                        total += i;
                    }
                }
                if (total > 0)
                {
                    Options.Add(new Option((OptionId)i)
                    {
                        ScoreValue = total
                    });
                }
            }
            Options.Add(new Option(OptionId.L7)                 //L7: sum of total
            {
                ScoreValue = SumOfDices(dices)
            });
        }

        private void AddOfKindsOptions(List<IDice> dices)
        {
            var foundThrees = false;
            var foundFours = false;
            var foundYathzee = false;

            foreach (Dice d in dices)
            {
                //Counts the amount of values of a number (d.number)
                var amountOfXs = dices.Count(v => v.Number == d.Number);
                if (amountOfXs >= 3)
                {
                    if (!foundThrees)
                    {
                        foundThrees = true;
                        Options.Add(new Option(OptionId.L1)                             //L1 = three of a kind
                        {
                            ScoreValue = SumOfDices(dices)
                        });
                    }       
                    if (amountOfXs >= 4)
                    {
                        if (!foundFours)
                        {
                            foundFours = true;
                            Options.Add(new Option(OptionId.L2)                             //L2 = four of a kind
                            {
                                ScoreValue = SumOfDices(dices)
                            });
                        }
                        if (amountOfXs >= 5)
                        {
                            if (!foundYathzee)
                            {
                                foundYathzee = true;
                                Options.Add(new Option(OptionId.L6)                             //L6 = yathzee
                                {
                                    ScoreValue = 50
                                });
                            }
                        }                      
                    }
                }
            }
        }

        private void AddStraightOptions(List<IDice> dices)
        {
            var orderedDices = dices.OrderBy(d => d.Number).ToList();

            //large straight: numbers 1 to 5 or 2 to 6
            if (orderedDices[0].Number == 1 && orderedDices[1].Number == 2 && orderedDices[2].Number == 3 && orderedDices[3].Number == 4 && orderedDices[4].Number == 5 || orderedDices[0].Number == 2 && orderedDices[1].Number == 6 && orderedDices[2].Number == 4 && orderedDices[3].Number == 5 && orderedDices[4].Number == 6)
            {
                Options.Add(new Option(OptionId.L5)                             //L5 = large straight
                {
                    ScoreValue = 40
                });
                Options.Add(new Option(OptionId.L4)                             //L4 = small straight
                {
                    ScoreValue = 30
                });
            }
            else
            {
                var orderedUniqueDices = orderedDices.Distinct(new DiceComparer()).ToList();
                //small straight: numbers 1 to 4, 2 to 5 ot 3 to 6
                if ((orderedUniqueDices.Count() == 4) && ( 
                    (orderedUniqueDices[0].Number == 1 && orderedUniqueDices[3].Number == 4) || 
                    (orderedUniqueDices[0].Number == 2 && orderedUniqueDices[3].Number == 5) || 
                    (orderedUniqueDices[0].Number == 3 && orderedUniqueDices[3].Number == 6) ) )
                {
                    Options.Add(new Option(OptionId.L4)                             //L4 = small straight
                    {
                        ScoreValue = 30
                    });
                }
            }
        }

        private void AddFullHouseOptions(List<IDice> dices)
        {
            var orderedDices = dices.OrderBy(d => d.Number).ToList();
            //Number ot times the first number is in the list
            var numberOfFirst = orderedDices.Count(v => v.Number == orderedDices[0].Number);
            //Number ot times the last number is in the list
            var numberOfLast = orderedDices.Count(v => v.Number == orderedDices[orderedDices.Count()-1].Number);

            if ((numberOfFirst == 2 && numberOfLast == 3) || (numberOfFirst == 3 && numberOfLast == 2) || numberOfFirst == 5)
            {
                Options.Add(new Option(OptionId.L3)                             //L3 = full house
                {
                    ScoreValue = 25
                });
            }
        }
        
        private int SumOfDices(List<IDice> dices)
        {
            var sum = 0;
            foreach (Dice d in dices)
            {
                sum += d.Number;
            }
            return sum;
        }

        public List<IDice> RandomDices()
        {
            List<IDice> dices = new List<IDice>();

            for (int i = 0; i < 5; i++)
            {
                Dice d = new Dice();
                dices.Add(d);
            }

            return dices;
        }

        public List<IDice> RollDices(List<IDice> dices)
        {

            foreach (Dice d in dices)
            {
                d.ThrowDice();
            }

            return dices;
        }

        class DiceComparer : IEqualityComparer<IDice>
        {

            public bool Equals(IDice x, IDice y)
            {
                return x.Number == y.Number;
            }

            public int GetHashCode(IDice obj)
            {
                return obj.Number.GetHashCode();
            }
        }
    }
}








/*
 *         public string go()
        {
            string lol = "";
            for (int i =0; i<5;i++)
            {
                lol += (OptionId)i + " ";
            }
            return lol;
        }


        //Adds Option to list if you can play aces with these dices
        private void Aces(List<Dice> dices)
        {
            int total = 0;
            foreach (Dice d in dices)
            {
                if (d.Number == 1)
                {
                    total += 1;
                }
            }
            if (total > 0)
            {
                Options.Add(new Option(OptionId.L1)
                {
                    ScoreValue = total
                });
            }
        }
        private void Twos(List<Dice> dices)
        {
            
            int total = 0;
            foreach (Dice d in dices)
            {
                if (d.Number == 2)
                {
                    total += 2;
                }
            }
            if (total > 0)
            {
                Options.Add(new Option(OptionId.L2)
                {
                    ScoreValue = total
                });
            }
        }
*/
