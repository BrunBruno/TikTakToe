using MediatR;
using MVCProject.Domain.Entities;
using MVCProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Application.Queries.GetAllGames
{
    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, IEnumerable<Game>> {
        private readonly IGameRepository repository;
        public GetAllGamesQueryHandler(IGameRepository repository) {
            this.repository = repository;
        }
        public async Task<IEnumerable<Game>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken) {
            var allGames = await repository.GetAllGames();
            return allGames;
        }
    }
}
