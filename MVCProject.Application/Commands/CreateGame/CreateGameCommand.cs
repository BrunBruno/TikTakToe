using MediatR;
using MVCProject.Domain.Entities;

namespace MVCProject.Application.Commands.CreateGame {
    public class CreateGameCommand : Game, IRequest{ }
}
