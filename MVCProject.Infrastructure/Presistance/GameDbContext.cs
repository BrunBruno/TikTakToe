using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCProject.Domain.Entities;

namespace MVCProject.Infrastructure.Presistance {
	public class GameDbContext : IdentityDbContext {
		public DbSet<Game> Games { get; set; }
		public DbSet<Player> Players { get; set; }
		public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Game>()
				.HasOne(g => g.Player1)
				.WithMany()
				.HasForeignKey(g => g.Player1Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Game>()
				.HasOne(g => g.Player2)
				.WithMany()
				.HasForeignKey(g => g.Player2Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Game>()
				.HasOne(g => g.Winner)
				.WithMany()
				.HasForeignKey(g => g.WinnerId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
