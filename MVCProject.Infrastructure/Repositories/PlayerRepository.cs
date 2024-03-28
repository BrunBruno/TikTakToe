using Microsoft.EntityFrameworkCore;
using MVCProject.Domain.Entities;
using MVCProject.Domain.Interfaces;
using MVCProject.Infrastructure.Presistance;

namespace MVCProject.Infrastructure.Repositories {
    public class PlayerRepository : IPlayerRepository {
        private readonly GameDbContext dbContext;
        public PlayerRepository(GameDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task CreatePlayer(Player player) {
            dbContext.Players.Add(player);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Player>> GetAllPlayers()
            => await dbContext.Players
                .Where(p => p.Id != "-")
                .OrderByDescending(p => p.GameWon)
                .ThenByDescending(p => p.GamePlayed)
                .ToListAsync();

        public Task<Player?> GetPlayer(string id) 
            => dbContext.Players.FirstOrDefaultAsync(i => i.Id == id);
    }
}
