using MediatR;
using MVCProject.Domain.Entities;

namespace MVCProject.Application.Queries.GetAllGamesForUser {
    public class GetAllGamesForUserQuery : IRequest<IEnumerable<Game>> { }
}
