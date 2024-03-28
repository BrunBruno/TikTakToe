using MediatR;
using MVCProject.Domain.Entities;
using MVCProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.Application.Queries.GetGameById {
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, Game> {
        private readonly IGameRepository repository;
        public GetGameByIdQueryHandler(IGameRepository repository) {
            this.repository = repository;
        }
        public async Task<Game> Handle(GetGameByIdQuery request, CancellationToken cancellationToken) {
            var game = await repository.GetGameById(request.Id);
            return game;
        }
    }
}
