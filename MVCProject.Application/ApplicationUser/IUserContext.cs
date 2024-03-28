
namespace MVCProject.Application.ApplicationUser {
    public interface IUserContext {
        string? GetUserIdByName(string userName);
        CurrentUser? GetCurrentUser();
    }
}
