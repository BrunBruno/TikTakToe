using MediatR;
using MVCProject.Domain.Entities;
using MVCProject.Domain.Interfaces;

namespace MVCProject.Application.Queries.GetAllPlayers {
    public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, IEnumerable<Player>> {
        private readonly IPlayerRepository repository;
        public GetAllPlayersQueryHandler(IPlayerRepository repository) {
            this.repository = repository;
        }
        public Task<IEnumerable<Player>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken) {
            var allPlayers = repository.GetAllPlayers();
            return allPlayers;
        }
    }
}
