using System.Web.Mvc;
using Who.BL.IServices;
using Who.Web.Models;

namespace Who.Web.Controllers
{
    public class GameController : Controller
    {
        private IGameService _gameService;


        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public ActionResult Index()
        {
            int gameId = _gameService.StartGame(1);
            return View(new GameViewModel { AmountOfRoundsPlayed = _gameService.RoundsPlayedInGame(gameId), TotalRounds = _gameService.GetRoundsPerGame(), Id = gameId });
        }
    }
}