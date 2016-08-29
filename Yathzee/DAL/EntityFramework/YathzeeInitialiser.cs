using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityFramework
{
    //In this class we define what to do when the model changes, start data is also created here
    public class YathzeeInitialiser : DropCreateDatabaseIfModelChanges<YathzeeContext>
    {
        protected override void Seed(YathzeeContext context)
        {
            //base.Seed(context);

            //Players
            context.Players.AddRange(GetPlayers());

            //Games
            context.Games.AddRange(GetGames());

            //GameScores
            context.GameScores.AddRange(GetGameScores());
            context.SaveChanges();
        }

        protected IEnumerable<Player> GetPlayers()
        {
            var playerList = new List<Player>
            {
                new Player()
                {
                    PlayerId = 1,
                    Email = "chip@chip.be",
                    Name = "Chip Glacier",
                    GamesPlayed = 2,
                    GamesWon = 1,
                    MaxScore = 150,
                    AverageScore = 115,
                    Privacy = Privacy.Private
                },
                new Player()
                {
                    PlayerId = 2,
                    Email = "dik@dik.be",
                    Name = "Dik Prince",
                    GamesPlayed = 2,
                    GamesWon = 1,
                    MaxScore = 180,
                    AverageScore = 125,
                    Privacy = Privacy.Global
                },
                new Player()
                {
                    PlayerId = 3,
                    Email = "red@red.be",
                    Name = "Red Devil",
                    GamesPlayed = 3,
                    GamesWon = 2,
                    MaxScore = 180,
                    AverageScore = 100,
                    Privacy = Privacy.Global
                },
                new Player()
                {
                    PlayerId = 4,
                    Email = "zor@zor.be",
                    Name = "Zorille Gailuron",
                    GamesPlayed = 3,
                    GamesWon = 2,
                    MaxScore = 210,
                    AverageScore = 123,
                    Privacy = Privacy.Global
                },
                new Player()
                {
                    PlayerId = 5,
                    Email = "sai@sai.be",
                    Name = "Saimiri Bonheur",
                    GamesPlayed = 2,
                    GamesWon = 0,
                    MaxScore = 0,
                    AverageScore = 95,
                    Privacy = Privacy.Private
                },
                new Player()
                {
                    PlayerId = 6,
                    Email = "bob@bob.be",
                    Name = "Bob Minuit",
                    GamesPlayed = 2,
                    GamesWon = 2,
                    MaxScore = 0,
                    AverageScore = 250,
                    Privacy = Privacy.Private
                }
            };
            return playerList;
        }
        protected IEnumerable<Game> GetGames()
        {
            var gameList = new List<Game>
            {
                new Game()
                {
                    GameId = 1,
                    GameState = GameState.Ended,
                    CreateDate = DateTime.Now,
                    InviterId = 1,
                    MemberId = 2,
                    TurnId = 1
                },
                new Game()
                {
                    GameId = 2,
                    GameState = GameState.Ended,
                    CreateDate = DateTime.Now,
                    InviterId = 2,
                    MemberId = 1,
                    TurnId = 2
                },
                new Game()
                {
                    GameId = 3,
                    GameState = GameState.Started,
                    CreateDate = DateTime.Now,
                    InviterId = 3,
                    MemberId = 4,
                    TurnId = 1
                },
                new Game()
                {
                    GameId = 4,
                    GameState = GameState.Invited,
                    CreateDate = DateTime.Now,
                    InviterId = 5,
                    MemberId = 3,
                    TurnId = 2
                },
                new Game()
                {
                    GameId = 5,
                    GameState = GameState.Ended,
                    CreateDate = DateTime.Now,
                    InviterId = 3,
                    MemberId = 6,
                    TurnId = 1
                },
                new Game()
                {
                    GameId = 6,
                    GameState = GameState.Invited,
                    CreateDate = DateTime.Now,
                    InviterId = 4,
                    MemberId = 5,
                    TurnId = 1
                },
                new Game()
                {
                    GameId = 7,
                    GameState = GameState.Started,
                    CreateDate = DateTime.Now,
                    InviterId = 6,
                    MemberId = 4,
                    TurnId = 2
                }
            };
            return gameList;
        }

        protected IEnumerable<GameScore> GetGameScores()
        {
            var scoresList = new List<GameScore>
            {
                new GameScore()
                {
                    GameScoreId = 1,
                    PlayerId = 1,
                    GameId = 1,
                    ScoreAces = 5,
                    ScoreTwos = 8,
                    ScoreThrees = 9,
                    ScoreFours = 16,
                    ScoreFives = 25,
                    ScoreSixes = 30,
                    ScoreThreeOfAKind = 18,
                    ScoreFourOfAKind = 30,
                    ScoreFullHouse = 25,
                    ScoreSmallStraight = 40,
                    ScoreLargeStraight = 50,
                    ScoreYathzee = 100,
                    ScoreChance = 23
                },
                new GameScore()
                {
                    GameScoreId = 2,
                    PlayerId = 2,
                    GameId = 1,
                    ScoreAces = 4,
                    ScoreTwos = 8,
                    ScoreThrees = 12,
                    ScoreFours = 16,
                    ScoreFives = 20,
                    ScoreSixes = 30,
                    ScoreThreeOfAKind = 20,
                    ScoreFourOfAKind = 0,
                    ScoreFullHouse = 25,
                    ScoreSmallStraight = 40,
                    ScoreLargeStraight = 50,
                    ScoreYathzee = 100,
                    ScoreChance = 25
                },
                new GameScore()
                {
                    GameScoreId = 3,
                    PlayerId = 1,
                    GameId = 2,
                    ScoreAces = 3,
                    ScoreTwos = 8,
                    ScoreThrees = 6,
                    ScoreFours = 12,
                    ScoreFives = 20,
                    ScoreSixes = 30,
                    ScoreThreeOfAKind = 12,
                    ScoreFourOfAKind = 18,
                    ScoreFullHouse = 25,
                    ScoreSmallStraight = 0,
                    ScoreLargeStraight = 50,
                    ScoreYathzee = 100,
                    ScoreChance = 26
                },
                new GameScore()
                {
                    GameScoreId = 4,
                    PlayerId = 2,
                    GameId = 2,
                    ScoreAces = 5,
                    ScoreTwos = 4,
                    ScoreThrees = 6,
                    ScoreFours = 20,
                    ScoreFives = 25,
                    ScoreSixes = 30,
                    ScoreThreeOfAKind = 16,
                    ScoreFourOfAKind = 21,
                    ScoreFullHouse = 0,
                    ScoreSmallStraight = 40,
                    ScoreLargeStraight = 50,
                    ScoreYathzee = 100,
                    ScoreChance = 22
                },
                new GameScore()
                {
                    GameScoreId = 5,
                    PlayerId = 3,
                    GameId = 3,
                    ScoreAces = 5,
                    ScoreTwos = 0,
                    ScoreThrees = 6,
                    ScoreFours = 20,
                    ScoreFives = 0,
                    ScoreSixes = 30,
                    ScoreThreeOfAKind = 0,
                    ScoreFourOfAKind = 0,
                    ScoreFullHouse = 25,
                    ScoreSmallStraight = 40,
                    ScoreLargeStraight = 50,
                    ScoreYathzee = 100,
                    ScoreChance = 0
                },
                new GameScore()
                {
                    GameScoreId = 6,
                    PlayerId = 4,
                    GameId = 3,
                    ScoreAces = 0,
                    ScoreTwos = 8,
                    ScoreThrees = 6,
                    ScoreFours = 0,
                    ScoreFives = 0,
                    ScoreSixes = 18,
                    ScoreThreeOfAKind = 15,
                    ScoreFourOfAKind = 17,
                    ScoreFullHouse = 25,
                    ScoreSmallStraight = 0,
                    ScoreLargeStraight = 40,
                    ScoreYathzee = 50,
                    ScoreChance = 24
                },
                new GameScore()
                {
                    GameScoreId = 7,
                    PlayerId = 3,
                    GameId = 5,
                    ScoreAces = 4,
                    ScoreTwos = 8,
                    ScoreThrees = 6,
                    ScoreFours = 0,
                    ScoreFives = 0,
                    ScoreSixes = 18,
                    ScoreThreeOfAKind = 20,
                    ScoreFourOfAKind = 23,
                    ScoreFullHouse = 25,
                    ScoreSmallStraight = 30,
                    ScoreLargeStraight = 40,
                    ScoreYathzee = 50,
                    ScoreChance = 19
                },
                new GameScore()
                {
                    GameScoreId = 8,
                    PlayerId = 6,
                    GameId = 5,
                    ScoreAces = 5,
                    ScoreTwos = 6,
                    ScoreThrees = 9,
                    ScoreFours = 0,
                    ScoreFives = 15,
                    ScoreSixes = 18,
                    ScoreThreeOfAKind = 15,
                    ScoreFourOfAKind = 22,
                    ScoreFullHouse = 25,
                    ScoreSmallStraight = 30,
                    ScoreLargeStraight = 40,
                    ScoreYathzee = 50,
                    ScoreChance = 19
                },
                new GameScore()
                {
                    GameScoreId = 9,
                    PlayerId = 6,
                    GameId = 7,
                    ScoreAces = 0,
                    ScoreTwos = 6,
                    ScoreThrees = 12,
                    ScoreFours = 0,
                    ScoreFives = 20,
                    ScoreSixes = 0,
                    ScoreThreeOfAKind = 22,
                    ScoreFourOfAKind = 0,
                    ScoreFullHouse = 25,
                    ScoreSmallStraight = 30,
                    ScoreLargeStraight = 0,
                    ScoreYathzee = 0,
                    ScoreChance = 19
                },
                new GameScore()
                {
                    GameScoreId = 10,
                    PlayerId = 4,
                    GameId = 7,
                    ScoreAces = 0,
                    ScoreTwos = 0,
                    ScoreThrees = 9,
                    ScoreFours = 0,
                    ScoreFives = 15,
                    ScoreSixes = 0,
                    ScoreThreeOfAKind = 0,
                    ScoreFourOfAKind = 22,
                    ScoreFullHouse = 25,
                    ScoreSmallStraight = 0,
                    ScoreLargeStraight = 40,
                    ScoreYathzee = 50,
                    ScoreChance = 22
                },
                new GameScore()
                {
                    GameScoreId = 11,
                    PlayerId = 3,
                    GameId = 4,
                    ScoreAces = 0,
                    ScoreTwos = 0,
                    ScoreThrees = 0,
                    ScoreFours = 0,
                    ScoreFives = 0,
                    ScoreSixes = 0,
                    ScoreThreeOfAKind = 0,
                    ScoreFourOfAKind = 0,
                    ScoreFullHouse = 0,
                    ScoreSmallStraight = 0,
                    ScoreLargeStraight = 0,
                    ScoreYathzee = 0,
                    ScoreChance = 0
                },
                new GameScore()
                {
                    GameScoreId = 12,
                    PlayerId = 5,
                    GameId = 4,
                    ScoreAces = 0,
                    ScoreTwos = 0,
                    ScoreThrees = 0,
                    ScoreFours = 0,
                    ScoreFives = 0,
                    ScoreSixes = 0,
                    ScoreThreeOfAKind = 0,
                    ScoreFourOfAKind = 0,
                    ScoreFullHouse = 0,
                    ScoreSmallStraight = 0,
                    ScoreLargeStraight = 0,
                    ScoreYathzee = 0,
                    ScoreChance = 0
                },
                new GameScore()
                {
                    GameScoreId = 12,
                    PlayerId = 4,
                    GameId = 6,
                    ScoreAces = 0,
                    ScoreTwos = 0,
                    ScoreThrees = 0,
                    ScoreFours = 0,
                    ScoreFives = 0,
                    ScoreSixes = 0,
                    ScoreThreeOfAKind = 0,
                    ScoreFourOfAKind = 0,
                    ScoreFullHouse = 0,
                    ScoreSmallStraight = 0,
                    ScoreLargeStraight = 0,
                    ScoreYathzee = 0,
                    ScoreChance = 0
                },
                new GameScore()
                {
                    GameScoreId = 12,
                    PlayerId = 5,
                    GameId = 6,
                    ScoreAces = 0,
                    ScoreTwos = 0,
                    ScoreThrees = 0,
                    ScoreFours = 0,
                    ScoreFives = 0,
                    ScoreSixes = 0,
                    ScoreThreeOfAKind = 0,
                    ScoreFourOfAKind = 0,
                    ScoreFullHouse = 0,
                    ScoreSmallStraight = 0,
                    ScoreLargeStraight = 0,
                    ScoreYathzee = 0,
                    ScoreChance = 0
                }
            };
            return scoresList;
        }
    }
}
