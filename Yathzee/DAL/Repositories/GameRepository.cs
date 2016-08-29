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
    //The repository for the game table. Games can be created, updated, deleted, etc in this class.
    public class GameRepository
    {
        private readonly YathzeeContext context;

        public GameRepository()
        {
            context = new YathzeeContext();
        }

        public Game CreateGame(Game gameToCreate)
        {
            context.Games.Add(gameToCreate);
            context.SaveChanges();
            return gameToCreate;
        }

        //returns all games 
        public IEnumerable<Game> GetGamesByPlayerId(int playerId)
        {
            IEnumerable<Game> games = context.Games.Where(g => g.InviterId == playerId || g.MemberId == playerId).ToList();
            return games;
        }


        public void DeleteGame(Game gameToDelete)
        {
            context.Games.Remove(gameToDelete);
            context.SaveChanges();
        }

        public void UpdateGame(Game gameToUpdate)
        {
            context.Entry(gameToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Game GetGameById(int id)
        {
            var gameToRetrieve = context.Games.AsNoTracking().FirstOrDefault(p => p.GameId == id);
            return gameToRetrieve;
        }
    }
}
