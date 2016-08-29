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
    //The repository for the game scores table. Scores of games can be created, updated, deleted, etc in this class.
    public class GameScoreRepository
    {
        private readonly YathzeeContext context;

        public GameScoreRepository()
        {
            context = new YathzeeContext();
        }

        public List<GameScore> AddGameScore(List<GameScore> gameScores)
        {
            foreach (GameScore g in gameScores)
            {
                AddGameScore(g);
            }
            return gameScores;
        }

        public GameScore AddGameScore(GameScore gameScore)
        {
            context.GameScores.Add(gameScore);
            context.SaveChanges();
            
            return gameScore;
        }

        public void UpdateGameScore(GameScore gameScoreToUpdate)
        {
            context.Entry(gameScoreToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }

        public int GetTotalScoreByGameAndPlayer(int gameId, int inviterId)
        {
            return GetScoreByGameAndPlayerId(gameId, inviterId).ScoreTotal;
        }

        public GameScore GetScoreByGameAndPlayerId(int gameId, int playerId)
        {
            GameScore score = context.GameScores.AsNoTracking().FirstOrDefault(g => g.GameId == gameId && g.PlayerId == playerId);
            return score;
        }        
    }
}
