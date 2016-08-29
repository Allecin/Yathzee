using DAL.Repositories;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.GameModel;

namespace BL
{
    //Manages the communication between views and gamerepository
    //Manages game logic that happens behind the views
    public class GameManager
    {
        private readonly GameRepository gameRepo;

        private string namePlayer = "";
        private string nameOpponent = "";

        public GameManager()
        {
            gameRepo = new GameRepository();
        }

        public Game CreateGame(int inviterId, int memberId)
        {
            var g = new Game()
            {
                GameState = GameState.Invited,
                InviterId = inviterId,
                MemberId = memberId,
                TurnId = GetRandomStarter(inviterId, memberId)
            };
            return CreateGame(g);
        }       

        public void ChangeGameStatus(int gameId, GameState state)
        {
            var gameToChange = gameRepo.GetGameById(gameId);
            gameToChange.GameState = state;

            gameRepo.UpdateGame(gameToChange);
        }

        public void ChangeGameTurn(int gameId)
        {
            var gameToChange = gameRepo.GetGameById(gameId);
            if (gameToChange.TurnId == gameToChange.InviterId)
            {
                gameToChange.TurnId = gameToChange.MemberId;
            }
            else
            {
                gameToChange.TurnId = gameToChange.InviterId;
            }

            gameRepo.UpdateGame(gameToChange);
        }

        private int GetRandomStarter(int inviterId, int memberId)
        {
            var random = new Random();

            if (random.NextDouble() > 0.5)
            {
                return inviterId;
            } 
            else
            {
                return memberId;
            }
        }

        public Game CreateGame(Game g)
        {
            Game game =  gameRepo.CreateGame(g);

            //After adding the game add gameScores
            var gameScoreMgr = new GameScoresManager();
            gameScoreMgr.CreateGameScore(g.GameId, g.InviterId, g.MemberId);

            return game;
        }

        public MyGamesViewModel GetOrderedGames(int playerId)
        {
            var allGames = GetAllGamesByPlayerId(playerId);

            var allOrderedGames = new MyGamesViewModel();
            var gameScoreMgr = new GameScoresManager();

            foreach (Game g in allGames)
            {
                if (g.GameState == GameState.Ended)
                {
                    GetPlayersNames(g, playerId);
                    
                    var winnerId = gameScoreMgr.GetWinnerId(g.GameId, g.InviterId, g.MemberId);
                    var winnerPlayer = false;
                    if (winnerId == playerId)
                    {
                        winnerPlayer = true;
                    }
                    allOrderedGames.EndedGames.Add(new MyGamesViewModel.GameInfo()
                    {
                        GameId = g.GameId,
                        PlayerId = playerId,
                        PlayerName = namePlayer,
                        OpponentName = nameOpponent,
                        PlayerWinner = winnerPlayer
                    });
                }
                else if (g.GameState == GameState.Started)
                {
                    GetPlayersNames(g, playerId);

                    bool turnPlayer = false;
                    if (g.TurnId == playerId)
                    {
                        turnPlayer = true;
                    };

                    allOrderedGames.OnGoingGames.Add(new MyGamesViewModel.GameInfo()
                    {
                        GameId = g.GameId,
                        PlayerId = playerId,
                        PlayerName = namePlayer,
                        OpponentId = g.MemberId,
                        OpponentName = nameOpponent,
                        PlayerTurn = turnPlayer
                    });
                }
                else if (g.GameState == GameState.Invited)
                {
                    GetPlayersNames(g, playerId);

                    //If player invited
                    if (g.InviterId == playerId)
                    {
                        //allOrderedGames.InviterGames.Add(g);
                        allOrderedGames.InviterGames.Add(new MyGamesViewModel.GameInfo()
                        {
                            GameId = g.GameId,
                            PlayerId = playerId,
                            PlayerName = namePlayer,
                            OpponentName = nameOpponent
                        });
                    }
                    else
                    //If other member invited
                    {                    
                        allOrderedGames.MemberGames.Add(new MyGamesViewModel.GameInfo()
                        {
                            GameId = g.GameId,
                            PlayerId = playerId,
                            PlayerName = namePlayer,
                            OpponentId = g.InviterId,
                            OpponentName = nameOpponent
                        });
                    }
                }
            }

            return allOrderedGames;
        }

        public GameViewModel ShowEndedGame(int gameId, int playerId)
        {
            var modelToShow = new GameViewModel();
            var playerMgr = new PlayerManager();
            var otherPlayerId = GetOtherPlayerId(gameId, playerId);

            modelToShow.GameId = gameId;
            modelToShow.MyName = playerMgr.GetNameById(playerId);
            modelToShow.OpponentName = playerMgr.GetNameById(otherPlayerId);
            modelToShow.GameScoresExtra = GetGameScoresExtra(gameId, playerId, otherPlayerId);

            return modelToShow;
        }

        internal Game GetGameById(int gameId)
        {
            return gameRepo.GetGameById(gameId);
        }

        private void GetPlayersNames(Game g, int playerId)
        {
            var playerMgr = new PlayerManager();

            if (g.InviterId == playerId)
            {
                namePlayer = playerMgr.GetNameById(g.InviterId);
                nameOpponent = playerMgr.GetNameById(g.MemberId);
            }
            else
            {
                nameOpponent = playerMgr.GetNameById(g.InviterId);
                namePlayer = playerMgr.GetNameById(g.MemberId);
            }
        }

        internal IEnumerable<Game> GetAllGamesByPlayerId(int playerId)
        {
            return gameRepo.GetGamesByPlayerId(playerId);
        }

        //Real game: what to show on the game
        public GameViewModel ShowGame(int gameId, int playerId, bool turnAtPlayer)
        {
            var modelToShow = new GameViewModel();
            var playerMgr = new PlayerManager();
            var playMgr = new PlayManager();
            var otherPlayerId = GetOtherPlayerId(gameId, playerId);

            modelToShow.GameId = gameId;
            modelToShow.MyTurn = turnAtPlayer;
            modelToShow.MyId = playerId;
            modelToShow.TurnName = TurnName(playerId, otherPlayerId, turnAtPlayer);
            modelToShow.MyName = playerMgr.GetNameById(playerId);
            modelToShow.OpponentName = playerMgr.GetNameById(otherPlayerId);
            var dices = playMgr.RandomDices();
            modelToShow.Dices = dices;
            //modelToShow.Options = GetPossibleOptions(playMgr.GetAllOptions(dices), gameId, inviterId);
            modelToShow.Options = new List<Option>();
            modelToShow.GameScoresExtra = GetGameScoresExtra(gameId, playerId, otherPlayerId);

            return modelToShow;
        }

        private IList<Option> GetPossibleOptions(List<Option> list, int gameId, int inviterId)
        {
            var gameScores = GetGameScores(gameId, inviterId);
            var remainingOptions = new List<Option>();

            foreach (Option o in list)
            {
                switch ((OptionId)o.OptionsId)
                {
                    case OptionId.U1: if (gameScores.ScoreAces == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.U2:
                        if (gameScores.ScoreTwos == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.U3:
                        if (gameScores.ScoreThrees == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.U4:
                        if (gameScores.ScoreFours == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.U5:
                        if (gameScores.ScoreFives == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.U6:
                        if (gameScores.ScoreSixes == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.L1:
                        if (gameScores.ScoreThreeOfAKind == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.L2:
                        if (gameScores.ScoreFourOfAKind == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.L3:
                        if (gameScores.ScoreFullHouse == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.L4:
                        if (gameScores.ScoreSmallStraight == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.L5:
                        if (gameScores.ScoreLargeStraight == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.L6:
                        if (gameScores.ScoreYathzee == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                    case OptionId.L7:
                        if (gameScores.ScoreChance == 0)
                        {
                            remainingOptions.Add(o);
                        };
                        break;
                }
            }
            return remainingOptions;
        }

        private string TurnName(int inviterId, int memberId, bool turnAtPlayer)
        {
            if (turnAtPlayer)
            {
                return new PlayerManager().GetNameById(inviterId);
            }
            else
            {
                return new PlayerManager().GetNameById(memberId);
            }
        }

        private GameScore GetGameScores(int gameId, int playerId)
        {
            var gameScoresMgr = new GameScoresManager();
            return gameScoresMgr.GetGameScore(gameId, playerId);
        }

        private IList<GameScoreExtra> GetGameScoresExtra(int gameId, int inviterId, int memberId)
        {
            var extras = new List<GameScoreExtra>();
            var gameScoresMgr = new GameScoresManager();

            extras.Add(new GameScoreExtra
            {
                GameScore = GetGameScores(gameId, inviterId)
            });
            extras[0].NumberTotal = GetNumberTotal(extras[0].GameScore);
            extras[0].Bonus = GetBonus(extras[0].LowerTotal);
            extras[0].UpperTotal = extras[0].NumberTotal + extras[0].Bonus;
            extras[0].LowerTotal = GetLowerTotal(extras[0].GameScore);
            extras[0].GameScore.ScoreTotal = extras[0].UpperTotal + extras[0].LowerTotal;

            extras.Add(new GameScoreExtra
            {
                GameScore = GetGameScores(gameId, memberId)
            });
            extras[1].NumberTotal = GetNumberTotal(extras[1].GameScore);
            extras[1].Bonus = GetBonus(extras[1].LowerTotal);
            extras[1].UpperTotal = extras[1].NumberTotal + extras[1].Bonus;
            extras[1].LowerTotal = GetLowerTotal(extras[1].GameScore);
            extras[1].GameScore.ScoreTotal = extras[1].UpperTotal + extras[1].LowerTotal;
            return extras;
        }

        private int GetLowerTotal(GameScore gameScore)
        {
            return gameScore.ScoreThreeOfAKind + gameScore.ScoreFourOfAKind + gameScore.ScoreFullHouse + gameScore.ScoreSmallStraight + gameScore.ScoreLargeStraight + gameScore.ScoreYathzee + gameScore.ScoreChance;
        }

        private int GetBonus(int lowerTotal)
        {
            if (lowerTotal >= 63)
            {
                return 35;
            }
            return 0;
        }

        private int GetNumberTotal(GameScore gameScore)
        {
            return gameScore.ScoreAces + gameScore.ScoreTwos + gameScore.ScoreThrees + gameScore.ScoreFours + gameScore.ScoreFives + gameScore.ScoreSixes;
        }

        public int GetOtherPlayerId(int gameId, int playerId)
        {
            var game = gameRepo.GetGameById(gameId);
            if (game.InviterId == playerId)
            {
                return game.MemberId;
            }
            else
            {
                return game.InviterId;
            }

        }
    }
}
