using DAL.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Yathzee.Models;

namespace BL
{
    //Manages all the access from and to the player repository
    //Has the logic about players that is needed for vieuws
    public class PlayerManager
    {
        private readonly PlayerRepository playerRepo;

        public PlayerManager()
        {
            playerRepo = new PlayerRepository();
        }

        public Player GetPlayer(int playerId)
        {
            return playerRepo.ReadPlayer(playerId);
        }

        public Player AddPlayer(string email, string firstName, string lastName, Privacy privacy)
        {
            var p = new Player()
            {
                Email = email,
                Name = firstName + " " + lastName,
                Privacy = privacy
            };
            return this.AddPlayer(p);
        }

        public Player AddPlayer(Player player)
        {
            this.ValidatePlayer(player);
            return playerRepo.CreatePlayer(player);
        }

        private void ValidatePlayer(Player player)
        {
            var errors = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(player, new ValidationContext(player), errors, validateAllProperties: true);
            foreach (var e in errors)
                Console.WriteLine(e);
            if (!valid)
                throw new ValidationException("Player is not valid!");
        }

        public void ChangePlayer (Player player)
        {
            this.ValidatePlayer(player);
            playerRepo.UpdatePlayer(player);
        }

        public void RemovePlayer(Player player)
        {
            playerRepo.DeletePlayer(player);
        }

        public Player GetPlayerByEmail(string email)
        {
            return playerRepo.GetPlayerByEmail(email);
        }

        public Player GetPlayerById(int playerId)
        {
            return playerRepo.GetPlayerById(playerId);
        }

        public void UpdateProfiles(int gameId, int playerId)
        {
            var game = new GameManager().GetGameById(gameId);
            var otherPlayer = GetOtherPlayer(gameId, playerId);
            var player = new PlayerManager().GetPlayerById(playerId);
            var gameScoreManager = new GameScoresManager();
            var gameScorePlayer = gameScoreManager.GetGameScore(gameId, playerId);
            var gameScoreOtherPlayer = gameScoreManager.GetGameScore(gameId, otherPlayer.PlayerId);

            player.GamesPlayed++;
            otherPlayer.GamesPlayed++;

            if (gameScorePlayer.ScoreTotal > gameScoreOtherPlayer.ScoreTotal)
            {
                player.GamesWon++;
            }
            else
            {
                otherPlayer.GamesWon++;
            }

            if(player.MaxScore < gameScorePlayer.ScoreTotal)
            {
                player.MaxScore = gameScorePlayer.ScoreTotal;
            }

            if (otherPlayer.MaxScore < gameScoreOtherPlayer.ScoreTotal)
            {
                otherPlayer.MaxScore = gameScoreOtherPlayer.ScoreTotal;
            }

            if (player.AverageScore == 0)
            {
                player.AverageScore = gameScorePlayer.ScoreTotal;
            } 
            else
            {
                player.AverageScore = (player.AverageScore + gameScorePlayer.ScoreTotal)/2;
            }

            playerRepo.UpdatePlayer(player);
            playerRepo.UpdatePlayer(otherPlayer);

        }

        public bool CheckPrivacy(string email)
        {
            if (GetPlayerByEmail(email).Privacy == Privacy.Global)
            {
                return true;
            }
            return false;
        }

        public void UpdateProfile(int playerId, string name, bool privacy)
        {
            var player = GetPlayerById(playerId);
            player.Name = name;
            if (privacy)
            {
                player.Privacy = Privacy.Private;
            } 
            else
            {
                player.Privacy = Privacy.Global;
            }

            playerRepo.UpdatePlayer(player);
        }

        public ProfileViewModel GetPlayerModel(int playerId)
        {
            var player = GetPlayerById(playerId);
            var profile = new ProfileViewModel()
            {
                PlayerId = player.PlayerId,
                Email = player.Email,
                Name = player.Name,
                GamesPlayed = player.GamesPlayed,
                GamesWon = player.GamesWon,
                MaxScore = player.MaxScore,
                AverageScore = player.AverageScore,
                Privacy = player.Privacy
            };
            return profile;
        }

        public int GetPlayerIdByEmail(string email)
        {
            try { 
                return playerRepo.GetPlayerByEmail(email).PlayerId;
            } catch (NullReferenceException e)
            {
                return -1;
            }
        }

        public string GetNameById(int playerId)
        {
            return playerRepo.GetNameById(playerId);
        }
        

        public NewGameViewModel GetFriendsByPlayerId(string pId)
        {
            int playerId = Int32.Parse(pId);
            var listOfFriends = new NewGameViewModel();
            var gameMgr = new GameManager();
            var allMyGames = gameMgr.GetAllGamesByPlayerId(playerId);


            foreach (Game g in allMyGames)
            {
                if (playerId == g.InviterId)
                {
                    if (!FriendInList(listOfFriends.PlayersInfo, g.MemberId))
                    {
                        Player playerToAdd = playerRepo.GetPlayerById(g.MemberId);
                        listOfFriends.PlayersInfo.Add(
                            new NewGameViewModel.PlayerInfo()
                            {
                                PlayerInfoId = g.MemberId,
                                Email = playerToAdd.Email,
                                Name = playerToAdd.Name
                            });
                    }
                }
                else
                {
                    if (!FriendInList(listOfFriends.PlayersInfo, g.InviterId))
                    {
                        Player playerToAdd = playerRepo.GetPlayerById(g.InviterId);
                        listOfFriends.PlayersInfo.Add(
                            new NewGameViewModel.PlayerInfo()
                            {
                                PlayerInfoId = g.InviterId,
                                Email = playerToAdd.Email,
                                Name = playerToAdd.Name
                            });
                    }
                }
            }

            return listOfFriends;
        }

        //Returns true if Id is in the list
        private bool FriendInList(IEnumerable<NewGameViewModel.PlayerInfo> friendsList, int friendId)
        {
            //if (friendsList.Count() == 0)
            //{
            //    return true;
            //}

            //bool playerInList = false;
            foreach (NewGameViewModel.PlayerInfo playerInfo in friendsList)
            {
                if (playerInfo.PlayerInfoId == friendId)
                {
                    return true;
                }
            }
            return false;
        }

        private Player GetOtherPlayer(int gameId, int playerId)
        {
            var game = new GameManager().GetGameById(gameId);
            if (game.InviterId == playerId)
            {
                return new PlayerManager().GetPlayerById(game.MemberId);
            }
            else
            {
                return new PlayerManager().GetPlayerById(game.InviterId);
            }
        }
    }
}
