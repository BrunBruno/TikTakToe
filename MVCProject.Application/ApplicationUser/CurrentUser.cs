
namespace MVCProject.Application.ApplicationUser {
    public class CurrentUser {
        public string Id { get; set; }
        public string Email { get; set; }
        public CurrentUser(string id, string emial) {
            Id = id;
            Email = emial;
        }
    }
}
