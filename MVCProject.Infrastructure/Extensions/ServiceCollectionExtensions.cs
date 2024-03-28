using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCProject.Domain.Interfaces;
using MVCProject.Infrastructure.Presistance;
using MVCProject.Infrastructure.Repositories;


namespace MVCProject.Infrastructure.Extensions {
    public static class ServiceCollectionExtensions {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            services.AddDbContext<GameDbContext>(options 
                => options.UseSqlServer(configuration.GetConnectionString("GameConnectionString")));

            services.AddDefaultIdentity<IdentityUser>(options => {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<GameDbContext>();


            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
        }
    }
}
