using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MVCProject.Application.ApplicationUser;
using MVCProject.Application.Commands.CreateGame;
using MVCProject.Application.Commands.CreatePlayer;
using MVCProject.Application.Hubs;
using MVCProject.Application.Queries.GetAllGamesForUser;
using MVCProject.Application.Queries.GetAllPlayers;
using MVCProject.Application.Queries.GetGameById;

namespace MVCProject.Controllers {
    public class GameController : Controller {
        private readonly IHubContext<GameHub> gameHubContext;
        private readonly IMediator mediator;
        private readonly IUserContext userContext;


        public GameController(IHubContext<GameHub> gameHubContext, IMediator mediator, IUserContext userContext) {
            this.gameHubContext = gameHubContext;
            this.mediator = mediator;
            this.userContext = userContext;
        }

        [Authorize]
        public async Task<IActionResult> Index() {
            await mediator.Send(new CreatePlayerCommand());
            return View();
        }

        [Authorize]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateGameCommand command) { 
            if(!ModelState.IsValid) {
                return View(command);
            }

            await mediator.Send(command);
            await gameHubContext.Clients.All.SendAsync("UpdateGames");
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [Route("Game/Live/{id}")]
        public async Task<IActionResult> Live(string id) {        
            var game = await mediator.Send(new GetGameByIdQuery(id));
            return View(game);
        }

        public async Task<IActionResult> GetAllGames() {
            var games = await mediator.Send(new GetAllGamesForUserQuery());
            return PartialView("_GameList", games);
        }

        public async Task<IActionResult> Ranking() {
            var players = await mediator.Send(new GetAllPlayersQuery());
            return View(players);
        }
    }
}
