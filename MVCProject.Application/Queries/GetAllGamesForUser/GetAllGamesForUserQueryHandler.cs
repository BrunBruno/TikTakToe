using MediatR;
using MVCProject.Application.ApplicationUser;
using MVCProject.Application.Queries.GetAllGames;
using MVCProject.Domain.Entities;
using MVCProject.Domain.Interfaces;

namespace MVCProject.Application.Queries.GetAllGamesForUser {
    public class GetAllGamesForUserQueryHandler : IRequestHandler<GetAllGamesForUserQuery, IEnumerable<Game>> {
        private readonly IGameRepository repository;
        private readonly IUserContext userContext;

        public GetAllGamesForUserQueryHandler(IGameRepository repository, IUserContext userContext) {
            this.repository = repository;
            this.userContext = userContext;
        }
        public async Task<IEnumerable<Game>> Handle(GetAllGamesForUserQuery request, CancellationToken cancellationToken) {
            var user = userContext.GetCurrentUser();
            if(user == null) {
                return Enumerable.Empty<Game>();
            }
            string userId = user.Id;
            var allGames = await repository.GetAllGamesForUser(userId);
            return allGames;
        }
    }
}
