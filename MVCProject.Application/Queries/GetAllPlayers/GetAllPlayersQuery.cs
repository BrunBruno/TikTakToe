using MediatR;
using MVCProject.Domain.Entities;

namespace MVCProject.Application.Queries.GetAllPlayers {
    public class GetAllPlayersQuery : IRequest<IEnumerable<Player>> { }
}
