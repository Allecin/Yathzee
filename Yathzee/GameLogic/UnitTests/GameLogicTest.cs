using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BL;
using ViewModels.GameModel;
using System.Collections.Generic;
using ViewModels;

namespace GameLogic
{
    [TestClass]
    public class GameLogicTest
    {
        [TestMethod]
        public void AllOptions()
        {
            //arrange            
            var dices1 = CreateDiceList(new int[] { 6, 6, 6, 4, 1 });       //1, 4, 6, three, sum
            var dices2 = CreateDiceList(new int[] { 5, 2, 3, 4, 4 });       //2, 3, 4, 5, small, sum
            var dices3 = CreateDiceList(new int[] { 5, 1, 4, 3, 2 });       //1, 2, 3, 4, 5, small, full, sum
            var dices4 = CreateDiceList(new int[] { 3, 5, 5, 3, 5 });       //3, 5, three, sum, full
            var dices5 = CreateDiceList(new int[] { 5, 5, 5, 5, 5 });       //5, sum, three, four, yathzee, full

            //act
            var options1 = new PlayManager().GetAllOptions(dices1);
            var options2 = new PlayManager().GetAllOptions(dices2);
            var options3 = new PlayManager().GetAllOptions(dices3);
            var options4 = new PlayManager().GetAllOptions(dices4);
            var options5 = new PlayManager().GetAllOptions(dices5);

            //assert
            Assert.AreEqual(5, options1.Count);
            Assert.AreEqual(6, options2.Count);
            Assert.AreEqual(8, options3.Count);
            Assert.AreEqual(5, options4.Count);
            Assert.AreEqual(6, options5.Count);
        }

        private List<IDice> CreateDiceList(int[] numbers)
        {
            var dices = new List<IDice>();

            foreach(int i in numbers)
            { 
            dices.Add(new Dice
            {
                Number = i
            });
            }

            return dices;
        }
    }
}
