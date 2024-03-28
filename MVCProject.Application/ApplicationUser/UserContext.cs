using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MVCProject.Application.ApplicationUser {
    public class UserContext : IUserContext {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;

        public UserContext(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager) {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }
        public CurrentUser? GetCurrentUser() {
            var user = httpContextAccessor?.HttpContext?.User;
            if (user == null) {
                throw new InvalidOperationException("Context user is not present.");
            }
            if (user.Identity == null || !user.Identity.IsAuthenticated) {
                return null;
            }

            var id = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            return new CurrentUser(id, email);
        }
        public string? GetUserIdByName(string userName) {
            var user = userManager.FindByNameAsync(userName).Result;

            if (user == null) {
                return null;
            }

            return user.Id;
        }
    }
}
