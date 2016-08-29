using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.GameModel
{
    /*Class of a dice created from an instance IDice and represents a dice that can be rolled.
    */
    public class Dice : IDice
    {
        public int Number { get; set; }
        public bool Locked { get; set; }
        public Random random { private get; set; }

        public Dice()
        {
            random = new Random();
            ThrowDice();
            Locked = false;
        }

        public void ThrowDice()
        {
            System.Threading.Thread.Sleep(443);
            Number = random.Next(1, 6);
        }
    }
}
