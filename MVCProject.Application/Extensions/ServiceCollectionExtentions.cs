using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MVCProject.Application.ApplicationUser;
using MVCProject.Application.Commands.CreateGame;
using MVCProject.Application.Hubs;
using MVCProject.Application.Queries.GetAllGames;

namespace MVCProject.Application.Extensions {
    public static class ServiceCollectionExtentions {
        public static void AddApplication(this IServiceCollection services) {
            services.AddScoped<IUserContext, UserContext>();

            services.AddMediatR(typeof(GetAllGamesQuery));

            services.AddValidatorsFromAssemblyContaining<CreateGameCommandValidation>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            services.AddSignalR();
            services.AddScoped<GameHub>();
        }
    }
}
