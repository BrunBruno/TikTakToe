using FluentValidation;
using MediatR;
using MVCProject.Application.ApplicationUser;
using MVCProject.Domain.Interfaces;

namespace MVCProject.Application.Commands.CreateGame {
    public class CreateGameCommandValidation : AbstractValidator<CreateGameCommand> {
        public CreateGameCommandValidation(IGameRepository gameRepository, IUserContext userContext) { 
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Game name should not be empty.")
                .Custom((value, context) => {
                    var existingGame = gameRepository.GetGameByName(value).Result;
                    if(existingGame != null && existingGame.WinnerId == null) {
                        context.AddFailure("Game already exists.");
                    }
                });
            RuleFor(g => g.Player2!.Name)
                .Custom((value, context) => {
                    if (value != null) {
                        var opponentId = userContext.GetUserIdByName(value);
                        var user = userContext.GetCurrentUser();
                        if(opponentId == null) {
                            context.AddFailure("User doesn't exist.");
                        }
                        if(user != null && user.Email == value) {
                            context.AddFailure("Can't play with yourself.");
                        }
                    }
                });
        }
    }
}
