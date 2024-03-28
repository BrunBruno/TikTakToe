
namespace MVCProject.Domain.Entities {
    public class Game {
        public required string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public ICollection<GameStatus> GameStatuses { get; set; }

        public string? Player1Id { get; set; }
        public string? Player2Id { get; set; }
        public string? WinnerId { get; set; }

        public Player? Player1 { get; set; }
        public Player? Player2 { get; set; }
        public Player? Winner { get; set; }

        //////public Game() {
        //////    GameStatuses = Enumerable.Range(1, 9)
        //////       .Select(index => new GameStatus { GameId = Id, Game = this, Status = null })
        //////       .ToList();
        //////}
    }
}
