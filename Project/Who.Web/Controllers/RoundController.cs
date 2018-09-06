﻿using System.Linq;
using System.Web.Mvc;
using Who.BL.Domain;
using Who.BL.IServices;
using Who.Web.Models;

namespace Who.Web.Controllers
{
    public class RoundController : Controller
    {
        private IGameService _gameService;

        public RoundController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: Round
        public ActionResult Play(int id)
        {
            Round currentRound = _gameService.StartRound(id);
            RoundViewModel roundViewModel = new RoundViewModel
            {
                Images = currentRound.Images.Select(i => new ImageViewModel { Url = i.Url, Id = i.Id }).ToList(),
                Name = currentRound.Name
            };
            return View(roundViewModel);
        }

        public ActionResult Guess(int id)
        {
            _gameService.AnswerRound(id, 1);
            RoundInfo roundInfo = _gameService.GetLatestRoundInfo(1);
            return View("RoundAnswered", new RoundAnsweredViewModel
            {
                AmountOfRoundsPlayed = roundInfo.AmountOfRoundsPlayed,
                CorrectImageId = roundInfo.CorrectImageId,
                GuessedImageId = roundInfo.GuessedImageId,
                Name = roundInfo.Name,
                TotalRounds = roundInfo.TotalRounds,
                Images = roundInfo.Images.Select(x => new ImageViewModel { Id = x.Id, Url = x.Url }).ToList()
            });
        }
    }
}