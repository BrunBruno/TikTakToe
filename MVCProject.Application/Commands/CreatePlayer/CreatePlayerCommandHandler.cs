using MediatR;
using MVCProject.Application.ApplicationUser;
using MVCProject.Domain.Entities;
using MVCProject.Domain.Interfaces;

namespace MVCProject.Application.Commands.CreatePlayer {
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand> {
        private readonly IUserContext userContext;
        private readonly IPlayerRepository playerRepository;
        public CreatePlayerCommandHandler(IUserContext userContext, IPlayerRepository playerRepository) {
            this.userContext = userContext;
            this.playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(CreatePlayerCommand request, CancellationToken cancellationToken) {
            var user = userContext.GetCurrentUser()!;
            var existingUser = await playerRepository.GetPlayer(user.Id);
            if (existingUser != null) {
                return Unit.Value;
            }
            var player = new Player() {
                Id = user.Id,
                Name = user.Email,
            };
            await playerRepository.CreatePlayer(player);
            return Unit.Value;
        }
    }
}
