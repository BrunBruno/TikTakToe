using Microsoft.AspNetCore.SignalR;
using MVCProject.Application.ApplicationUser;
using MVCProject.Domain.Entities;
using MVCProject.Domain.Interfaces;

namespace MVCProject.Application.Hubs {
    public class GameHub : Hub {
        private static Dictionary<string, List<string>> playersByGameId = new();
        private static Dictionary<string, List<bool?>> gameStatusByGameId = new();
        private readonly IUserContext userContext;
        private readonly IGameRepository gameRepository;
        private readonly IPlayerRepository playerRepository;

        public GameHub(IUserContext userContext, IGameRepository gameRepository, IPlayerRepository playerRepository) {
            this.userContext = userContext;
            this.gameRepository = gameRepository;
            this.playerRepository = playerRepository;
        }
        public async Task JoinGame(Game game) {
            var gameId = game.Id;
            var user = userContext.GetCurrentUser();
            if(user == null) {
                return;
            }

            if (!playersByGameId.ContainsKey(gameId))
                playersByGameId[gameId] = new List<string>();

            if (playersByGameId[gameId].Count < 2) {
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId);

                if (!playersByGameId[gameId].Contains(user.Id)) {
                    playersByGameId[gameId].Add(user.Id);
                }


                if (game.Player2Id == null && user.Id != game.Player1Id) {
                    game.Player2Id = user.Id;
                    game.Player2 = await playerRepository.GetPlayer(user.Id);
                    
                }

                if (playersByGameId[gameId].Count == 2) {
                    var rand = new Random().Next(2);
                    if (rand == 1) {
                        (playersByGameId[gameId][1], playersByGameId[gameId][0]) = (playersByGameId[gameId][0], playersByGameId[gameId][1]);
                    }

                    gameStatusByGameId[gameId] = Enumerable.Repeat<bool?>(null, 9).ToList();

                    await Clients.Group(gameId).SendAsync("GameStarted", gameId, gameStatusByGameId[gameId]);
                    await Clients.User(playersByGameId[gameId][0]).SendAsync("GameStartInfo", true);
                    await Clients.User(playersByGameId[gameId][1]).SendAsync("GameStartInfo", false);

                    game.Player1!.GamePlayed += 1;
                    game.Player2!.GamePlayed += 1;
                }

                await gameRepository.UpdateGame(game);
                await Clients.All.SendAsync("UpdateGames");
            } else {
                bool belongs = false;
                foreach (var id in playersByGameId[gameId]) {
                    if (user.Id == id) {
                        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
                        await Clients.Caller.SendAsync("GameRejoined", gameId, gameStatusByGameId[gameId]);
                        await Clients.Caller.SendAsync("UpdateTurn", playersByGameId[gameId].IndexOf(user.Id) == gameStatusByGameId[gameId].Count(value => value != null) % 2);
                        belongs = true;
                        break;
                    };
                }

                if(!belongs || game.WinnerId != null)
                    await Clients.Caller.SendAsync("GameFull");
            }
        }

        public async Task AreaClicked(string gameId, int i) {
            var user = userContext.GetCurrentUser();
            if(user == null) {
                return;
            }
            var playerIndex = playersByGameId[gameId].IndexOf(user.Id);
            var gameStatus = gameStatusByGameId[gameId].Count(value => value != null) % 2;

            if (playerIndex >= 0 && playerIndex == gameStatus) {
                if (gameStatusByGameId[gameId][i] == null) {
                    gameStatusByGameId[gameId][i] = playerIndex == 0;

                    await Clients.Group(gameId).SendAsync("AreaClicked", gameStatusByGameId[gameId]);
                    await Clients.User(playersByGameId[gameId][0]).SendAsync("UpdateTurn", 1 == gameStatus);
                    await Clients.User(playersByGameId[gameId][1]).SendAsync("UpdateTurn", 0 == gameStatus);

                    var checkWin = CheckWin(gameId, playerIndex == 0);
                    if (checkWin.Item1) {
                        var game = await gameRepository.GetGameById(gameId);

                        if (checkWin.Item2.Count != 0) {
                            game.WinnerId = user.Id;
                            game.Winner = await playerRepository.GetPlayer(user.Id);
                            game.Winner!.GameWon += 1;                                                     
                            
                            await Clients.Group(gameId).SendAsync("GameWon", user.Email, checkWin.Item2);
                        } else {
                            game.WinnerId = "-";

                            await Clients.Group(gameId).SendAsync("GameDraw");
                        }

                        await gameRepository.UpdateGame(game);
                        await Clients.All.SendAsync("UpdateGames");

                        playersByGameId.Remove(gameId);
                        gameStatusByGameId.Remove(gameId);
                    }
                } else {
                    await Clients.Caller.SendAsync("InvalidMove", "Incorrect move!");
                }
            } else {
                await Clients.Caller.SendAsync("InvalidMove", "Not your turn!");
            }
        }

        private Tuple<bool, List<int>> CheckWin(string gameId, bool symbol) {
            int[,] winConditions = new int[,] {{0, 1, 2}, {3, 4, 5}, {6, 7, 8}, {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, {0, 4, 8}, {2, 4, 6}};

            for (int i = 0; i < winConditions.GetLength(0); i++) {
                if (gameStatusByGameId[gameId][winConditions[i, 0]] == symbol &&
                    gameStatusByGameId[gameId][winConditions[i, 1]] == symbol &&
                    gameStatusByGameId[gameId][winConditions[i, 2]] == symbol) {
                    return Tuple.Create(true, new List<int>() { winConditions[i, 0], winConditions[i, 1], winConditions[i, 2] });
                }
            }

            if (!gameStatusByGameId[gameId].Any(value => value == null)) {
                return Tuple.Create(true, new List<int>());
            }

            return Tuple.Create(false, new List<int>());
        }
    }
}
