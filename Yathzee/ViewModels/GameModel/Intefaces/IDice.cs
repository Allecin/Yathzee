namespace ViewModels.GameModel
{
    //Dice interface 
    public interface IDice
    {
        /*Properties
         Locked: if the dice is locked or has to be rethrown
         Number: The value of the dice*/
        bool Locked { get; set; }
        int Number { get; set; }

        //Changes the value of the dice
        void ThrowDice();
    }
}