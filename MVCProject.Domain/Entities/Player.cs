
namespace MVCProject.Domain.Entities {
    public class Player {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int GamePlayed { get; set; } = 0;
        public int GameWon { get; set; } = 0;
    }
}
