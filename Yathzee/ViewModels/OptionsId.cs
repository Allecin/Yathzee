using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    //Enum for the different possibilities someone has to achieve in the yathzee game.
    public enum OptionId
    {
        U1 = 1,     //1
        U2 = 2,
        U3 = 3,
        U4 = 4,
        U5 = 5,
        U6 = 6,     //6
        U7 = 7,     //bonus
        L1 = 11,    //3 of a kind
        L2 = 12,    //4 of a kind
        L3 = 13,    //full house
        L4 = 14,    //small straight
        L5 = 15,    //large straight
        L6 = 16,    //Yathzee
        L7 = 17,    //chance total
        L8 = 18     //yathzee bonus
    }
}
