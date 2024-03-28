using Microsoft.EntityFrameworkCore;
using MVCProject.Domain.Entities;
using MVCProject.Domain.Interfaces;
using MVCProject.Infrastructure.Presistance;

namespace MVCProject.Infrastructure.Repositories {
    public class GameRepository : IGameRepository {
        private readonly GameDbContext dbContext;
        public GameRepository(GameDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Game>> GetAllGames() 
            => await dbContext.Games.ToListAsync();
        public async Task<IEnumerable<Game>> GetAllGamesForUser(string currentUserId)
            => await dbContext.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .Include(g => g.Winner)
                .Where(item => item.WinnerId == null && (item.Player1Id == currentUserId || item.Player2Id == currentUserId || item.Player2Id == null))
                .ToListAsync();
        public async Task<Game> GetGameById(string id) 
            => await dbContext.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .Include(g => g.Winner)
                .FirstAsync(i => i.Id == id);
        public Task<Game?> GetGameByName(string name) 
            => dbContext.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .Include(g => g.Winner)
                .FirstOrDefaultAsync(g => g.Name == name);
        public async Task CreateGame(Game game) { 
            dbContext.Add(game);
            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateGame(Game game) {
            dbContext.Update(game);
            await dbContext.SaveChangesAsync();
        }
    }
}
