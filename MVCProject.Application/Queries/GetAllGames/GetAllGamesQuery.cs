using MediatR;
using MVCProject.Domain.Entities;

namespace MVCProject.Application.Queries.GetAllGames
{
    public class GetAllGamesQuery : IRequest<IEnumerable<Game>> { }
}
