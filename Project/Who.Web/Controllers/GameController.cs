﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Who.BL.Domain;
using Who.BL.IServices;
using Who.Web.Models;

namespace Who.Web.Controllers
{
    [AuthorizeWithUnauthorizedRedirectAttribute]
    public class GameController : BaseController
    {
        private IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public ActionResult Index()
        {
            int gameId = _gameService.StartNewGameOrGetExistingId(GetUserIdFromSessionStorage());
            return View(new GameViewModel { AmountOfRoundsPlayed = _gameService.RoundsPlayedInGame(gameId), TotalRounds = _gameService.GetRoundsPerGame(), Id = gameId });
        }

        public ActionResult HighScoresForAllPlayers()
        {
            IEnumerable<Score> scores = _gameService.GetHighScoresForAllPlayers(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01), DateTime.MaxValue);
            return View(scores.Select(s => 
            new ScoreViewModel
            {
                AmountOfCorrectAnswers = s.AmountOfCorrectAnswers,
                Duration = s.Duration,
                AmountOfGamesPlayed = s.AmountOfGamesPlayed,
                FullName = s.FullName,
                AmountOfRoundsPerGame = s.AmountOfRoundsPerGame
            }).ToList());
        }

        public ActionResult HighScoresPersonal()
        {
            var personalScore =_gameService.GetCurrentScorePreviousScoreAndRank(GetUserIdFromSessionStorage(), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01), DateTime.MaxValue);
            PersonalScoreViewModel psvm = new PersonalScoreViewModel
            {
                Rank = personalScore.Rank,
                PersonalScoreItems = personalScore.PersonalScoreItems.Select(psi => new PersonalScoreItemViewModel {
                    Duration = psi.Duration,
                    AmountOfCorrectAnswers = psi.AmountOfCorrectAnswers,
                    AmountOfRoundsPerGame = psi.AmountOfRoundsPerGame
                }).ToList()

            };
            return View(psvm);
        }
    }
}