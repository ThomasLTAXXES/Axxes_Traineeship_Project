using System.Linq;
using System.Web.Mvc;
using Who.BL.Domain;
using Who.BL.IServices;
using Who.Web.Models;

namespace Who.Web.Controllers
{
    [Authorize]
    public class RoundController : BaseController
    {
        private IGameService _gameService;

        public RoundController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: Round
        public ActionResult Play(int id) //TODO: remove
        {
            int gameId = _gameService.StartGame(GetUserIdFromSessionStorage());
            Round currentRound = _gameService.StartRound(gameId);
            RoundViewModel roundViewModel = new RoundViewModel
            {
                Images = currentRound.Images.Select(i => new ImageViewModel { Url = i.Url, Id = i.Id }).ToList(),
                Name = currentRound.Name,
                RoundIndex = currentRound.AmountOfRoundsPlayed+1,
                TotalRounds = currentRound.TotalRounds
            };
            return View(roundViewModel);
        }

        public ActionResult Guess(int id)
        {
            _gameService.AnswerRound(id, GetUserIdFromSessionStorage());
            RoundInfo roundInfo = _gameService.GetLatestRoundInfo(GetUserIdFromSessionStorage());
            RoundAnsweredViewModel ravm = new RoundAnsweredViewModel
            {
                GameId = roundInfo.GameId,
                AmountOfRoundsPlayed = roundInfo.AmountOfRoundsPlayed,
                CorrectImageId = roundInfo.CorrectImageId,
                GuessedImageId = roundInfo.GuessedImageId,
                Name = roundInfo.Name,
                TotalRounds = roundInfo.TotalRounds,
                Images = roundInfo.Images.Select(x => new ImageViewModel { Id = x.Id, Url = x.Url }).ToList()
            };
            return View("RoundAnswered", ravm);
        }
    }
}