using System.Web.Mvc;
using Who.BL.IServices;
using Who.Web.Models;

namespace Who.Web.Controllers
{
    [Authorize]
    public class GameController : BaseController
    {
        private IGameService _gameService;


        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public ActionResult Index()
        {
            int gameId = _gameService.StartGame(GetUserIdFromSessionStorage());
            return View(new GameViewModel { AmountOfRoundsPlayed = _gameService.RoundsPlayedInGame(gameId), TotalRounds = _gameService.GetRoundsPerGame(), Id = gameId });
        }
    }
}