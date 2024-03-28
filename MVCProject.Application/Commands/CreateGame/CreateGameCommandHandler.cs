using MediatR;
using MVCProject.Application.ApplicationUser;
using MVCProject.Domain.Entities;
using MVCProject.Domain.Interfaces;

namespace MVCProject.Application.Commands.CreateGame {
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand> {
        private readonly IGameRepository repository;
        private readonly IPlayerRepository playerRepository;
        private readonly IUserContext userContext;

        public CreateGameCommandHandler(IGameRepository repository, IPlayerRepository playerRepository, IUserContext userContext) {
            this.repository = repository;
            this.playerRepository = playerRepository;
            this.userContext = userContext;
        }
        public async Task<Unit> Handle(CreateGameCommand request, CancellationToken cancellationToken) {
            var player1Id = userContext.GetCurrentUser()!.Id;
            request.Player1Id = player1Id;
            request.Player1 = await playerRepository.GetPlayer(player1Id);




            if (request.Player2!.Name != null) {
                var player2Id = userContext.GetUserIdByName(request.Player2.Name);
                var existingPlayer = await playerRepository.GetPlayer(player2Id!);
                request.Player2Id = player2Id;
                if (existingPlayer != null) {
                    request.Player2 = existingPlayer;
                } else {
                    request.Player2 = new Player() {
                        Id = player2Id,
                        Name = request.Player2.Name,
                    };
                }

            } else { 
                request.Player2 = null; 
            }

            await repository.CreateGame(request);
            return Unit.Value;
        }
    }
}
