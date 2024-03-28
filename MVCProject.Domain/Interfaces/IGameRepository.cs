using MVCProject.Domain.Entities;

namespace MVCProject.Domain.Interfaces {
    public interface IGameRepository {
        Task<IEnumerable<Game>> GetAllGames();
        Task<IEnumerable<Game>> GetAllGamesForUser(string currentUserId);
        Task CreateGame(Game game);
        Task<Game> GetGameById(string id);
        Task<Game?> GetGameByName(string name);
        Task UpdateGame(Game game);
    }
}
