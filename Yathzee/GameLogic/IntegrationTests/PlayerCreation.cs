using DAL.EntityFramework;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yathzee.Controllers;
using Yathzee.Models;

namespace GameLogic.IntegrationTests
{
    
    public class PlayerCreation
    {

        public async Task<bool> Create_player_Action_Creates_Player()
        {
            //var emailGetaway = new FakeEmail
            var controller = new AccountController();


            await controller.Register(new RegisterViewModel
            {
                FirstName = "Katheryn",
                LastName = "Winnick",
                Email = "kat.Win@yathzee.be",
                Privacy = "Private",
                Password = "Kat123..",
                ConfirmPassword = "Kat123.."
            });

            var context = new YathzeeContext();
            try
            {
                var playerToRetrieve = context.Players.AsNoTracking().FirstOrDefault(p => p.Email == "kat.Win@yathzee.be");
                playerToRetrieve.Name = "Katheryn Winnick";
                playerToRetrieve.Privacy = Privacy.Private;
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


    }
}

