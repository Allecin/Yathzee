using DAL.EntityFramework;
using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    //The repository for the player table. Players can be created, updated, deleted, etc in this class.
    public class PlayerRepository
    {
        private readonly YathzeeContext context;

        public PlayerRepository()
        {
            context = new YathzeeContext();
        }

        public Player ReadPlayer(int PlayerId)
        {
            var player = context.Players.Find(PlayerId);
            return player;
        }

        public Player CreatePlayer(Player playerToCreate)
        {
            context.Players.Add(playerToCreate);
            context.SaveChanges();
            return playerToCreate;
        }

        public void DeletePlayer(Player playerToDelete)
        {
            context.Players.Remove(playerToDelete);
            context.SaveChanges();
        }

        public void UpdatePlayer(Player playerToUpdate)
        {
            context.Entry(playerToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Player GetPlayerByEmail(string email)
        {
            var playerToRetrieve = context.Players.AsNoTracking().FirstOrDefault(p => p.Email == email);
            return playerToRetrieve;
        }

        public Player GetPlayerById(int id)
        {
            var playerToRetrieve = context.Players.AsNoTracking().FirstOrDefault(p => p.PlayerId == id);
            return playerToRetrieve;
        }

        public string GetNameById(int id)
        {
            Player playerToRetrieve = context.Players.AsNoTracking().FirstOrDefault(p => p.PlayerId == id);
            return playerToRetrieve.Name;
        }
    }
}
