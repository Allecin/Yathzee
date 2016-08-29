using DAL.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace BL
{
    //Manages the communication between views and the repository for gameScores (gamescores are the scores of a player for one game)
    //Manages score logic that happens behind the views
    public class GameScoresManager
    {
        private readonly GameScoreRepository GameScoreRepo;

        public GameScoresManager()
        {
            GameScoreRepo = new GameScoreRepository();
        }

        internal int GetWinnerId(int gameId, int inviterId, int memberId)
        {
            int scoreInviter = GetTotalScoreByGameAndPlayer(gameId, inviterId);
            int scoreMember = GetTotalScoreByGameAndPlayer(gameId, memberId);
            if (scoreInviter > scoreMember)
            {
                return inviterId;
            }
            else
            {
                return memberId;
            }
        }

        public bool GameEnded(int gameId, int playerId)
        {
            var opponentId = new GameManager().GetOtherPlayerId(gameId, playerId);
            var gameScorePlayer = GetGameScore(gameId, playerId);
            var gameScoreOpponent = GetGameScore(gameId, opponentId);
            
            if (fullScore(gameScorePlayer) && fullScore(gameScoreOpponent))
            {
                return true;
            }
            return false;
        }

        private bool fullScore(GameScore g)
        {
            if (g.ScoreAces != 0 && g.ScoreTwos !=0 && g.ScoreThrees != 0 && g.ScoreFours != 0 && g.ScoreFives != 0 && g.ScoreSixes != 0 && g.ScoreSmallStraight != 0 && g.ScoreLargeStraight != 0 && g.ScoreFullHouse != 0 && g.ScoreThreeOfAKind != 0 && g.ScoreFourOfAKind != 0 && g.ScoreYathzee != 0 && g.ScoreChance != 0)
            {
                return true;
            }
            return false;
        }

        private int GetTotalScoreByGameAndPlayer(int gameId, int inviterId)
        {
            return GameScoreRepo.GetTotalScoreByGameAndPlayer(gameId, inviterId);
        }

        public GameScore GetGameScore(int gameId, int playerId)
        {
            return GameScoreRepo.GetScoreByGameAndPlayerId(gameId, playerId);
        }

        public void AddMove(int gameId, int myId, Option option)
        {
            var gameScore = GameScoreRepo.GetScoreByGameAndPlayerId(gameId, myId);
            switch((OptionId)option.OptionsId)
                {
                    case OptionId.U1: gameScore.ScoreAces = option.ScoreValue;
                    break;
                case OptionId.U2: gameScore.ScoreTwos = option.ScoreValue;
                    break;
                case OptionId.U3: gameScore.ScoreThrees = option.ScoreValue;
                    break;
                case OptionId.U4: gameScore.ScoreFours = option.ScoreValue;
                    break;
                case OptionId.U5: gameScore.ScoreFives = option.ScoreValue;
                    break;
                case OptionId.U6: gameScore.ScoreSixes = option.ScoreValue;
                    break;
                case OptionId.L1: gameScore.ScoreThreeOfAKind = option.ScoreValue;
                    break;
                case OptionId.L2: gameScore.ScoreFourOfAKind = option.ScoreValue;
                    break;
                case OptionId.L3: gameScore.ScoreFullHouse = option.ScoreValue;
                    break;
                case OptionId.L4: gameScore.ScoreSmallStraight = option.ScoreValue;
                    break;
                case OptionId.L5: gameScore.ScoreLargeStraight = option.ScoreValue;
                    break;
                case OptionId.L6: gameScore.ScoreYathzee = option.ScoreValue;
                    break;
                case OptionId.L7: gameScore.ScoreChance = option.ScoreValue;
                    break;
            }
            GameScoreRepo.UpdateGameScore(gameScore);
        }

        public List<GameScore> CreateGameScore(int gameId, int inviterId, int memeberId)
        {
            var g = new List<GameScore>();

            g.Add(new GameScore
            {
                PlayerId = inviterId,
                GameId = gameId
            });
            g.Add(new GameScore
            {
                PlayerId = memeberId,
                GameId = gameId
            });

            return CreateGameScore(g);
        }

        private List<GameScore> CreateGameScore(List<GameScore> g)
        {
            return GameScoreRepo.AddGameScore(g);
        }
    }
}
