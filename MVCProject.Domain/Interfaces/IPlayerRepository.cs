
using MVCProject.Domain.Entities;

namespace MVCProject.Domain.Interfaces {
    public interface IPlayerRepository {
        Task<IEnumerable<Player>> GetAllPlayers();
        Task CreatePlayer(Player player);
        Task<Player?> GetPlayer(string id);
    }
}
