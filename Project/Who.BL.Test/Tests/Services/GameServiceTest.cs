using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Who.BL.Domain;
using Who.BL.IRepositories;
using Who.BL.IServices;
using Who.BL.Test.Mocks.Repositories;
using Who.Data;

namespace Who.BL.Test.Tests.Services
{
    [TestClass]
    public class GameServiceTest
    {
        private IGameService _gameService;
        private IRepository<GameEntity> _gameRepository;
        private IRepository<ImageEntity> _imageRepository;

        [TestInitialize]
        public void Initialize()
        {
            _gameRepository = new MemoryRepository<GameEntity>();
            _imageRepository = new MemoryRepository<ImageEntity>();
            // Create 100 images
            for (int i = 1; i < 100; i++)
            {
                _imageRepository.Create(CreateNewImageWithDefaults(_imageRepository.GetAll()?.LastOrDefault()?.Id ?? 1));
            }
            //  _gameService = new GameService(_gameRepository, _imageRepository);
        }

        private ImageEntity CreateNewImageWithDefaults(int id)
        {
            return new ImageEntity
            {
                Name = id + "",
                Url = id + ""
            };
        }

        [TestMethod]
        public void StartGameTest()
        {
            int gameEntityId = _gameService.StartNewGameOrGetExistingId(1);
            GameEntity gameEntity = _gameRepository.Get(gameEntityId);
            Assert.IsNotNull(gameEntity);
            Assert.IsNull(gameEntity.Rounds);

            // We don't know the exact start date, but if it's between now and 5 minutes ago, it's okay
            Assert.IsTrue(DateTime.Now > gameEntity.StartDate);
            Assert.IsTrue(DateTime.Now.AddMinutes(-5) < gameEntity.StartDate);
        }

        [TestMethod]
        public void StartRoundTest()
        {
            int gameEntityId = _gameService.StartNewGameOrGetExistingId(1);
            Round round = _gameService.StartRoundOrGetExisting(gameEntityId);
            Assert.IsNotNull(round);
            Assert.IsNotNull(round.Images);
            Assert.AreEqual(4, round.Images.Count); //TODO: config
            Assert.IsNotNull(round.Name);
        }

        [TestMethod]
        public void AnswerRoundCorrectlyTest()
        {
            int gameEntityId = _gameService.StartNewGameOrGetExistingId(1);
            Round round = _gameService.StartRoundOrGetExisting(gameEntityId);
            int indexOf = IndexOfCorrectImageInRound(round);
            //  bool succes = _gameService.AnswerRound(round, indexOf, gameEntityId);
            //     Assert.IsTrue(succes);
        }

        private int IndexOfCorrectImageInRound(Round round)
        {
            var imageFromDb = _imageRepository.GetAll().First(i => i.Name == round.Name);
            int indexOf = -1;
            for (int i = 0; i < round.Images.Count; i++)
            {
                if (round.Images[i].Url == imageFromDb.Url)
                {
                    indexOf = i;
                }
            }

            return indexOf;
        }

        [TestMethod]
        public void AnswerRoundIncorrectlyTest()
        {
            int gameEntityId = _gameService.StartNewGameOrGetExistingId(1);
            Round round = _gameService.StartRoundOrGetExisting(gameEntityId);
            int indexOf = IndexOfCorrectImageInRound(round);
            //   bool succes = _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntityId);
            //    Assert.IsFalse(succes);
        }

        [TestMethod]
        public void PlayFullGameWithAllAnswersCorrectTest()
        {
            int gameEntityId = _gameService.StartNewGameOrGetExistingId(1);
            while (_gameService.MayTheGameHaveMoreRounds(gameEntityId))
            {
                Round round = _gameService.StartRoundOrGetExisting(gameEntityId);
                int indexOf = IndexOfCorrectImageInRound(round);
                //     _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntityId);
            }
            //TODO: score testing
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PlayTooManyRoundsInAGameTest()
        {
            int gameEntityId = _gameService.StartNewGameOrGetExistingId(1);
            while (_gameService.MayTheGameHaveMoreRounds(gameEntityId))
            {
                Round round = _gameService.StartRoundOrGetExisting(gameEntityId);
                int indexOf = IndexOfCorrectImageInRound(round);
                //    _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntityId);
            }
            _gameService.StartRoundOrGetExisting(gameEntityId);
        }

        [TestMethod]
        public void GetAllGamesForPlayerTest()
        {
            int amountOfGames = 5;
            int userId = 1;
            for (int i = 0; i < amountOfGames; i++)
            {
                int gameEntityId = _gameService.StartNewGameOrGetExistingId(userId);
                while (_gameService.MayTheGameHaveMoreRounds(gameEntityId))
                {
                    Round round = _gameService.StartRoundOrGetExisting(gameEntityId);
                    int indexOf = IndexOfCorrectImageInRound(round);
                    //         _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntityId);
                }
            }

            //   Assert.AreEqual(amountOfGames, 
            //     _gameService.GetAllGamesForPlayer(userId, DateTime.MinValue, DateTime.MaxValue).Count());
        }

        [TestMethod]
        public void GetAllGamesForPlayerWithOtherGamesTest()
        {
            int amountOfGames = 5;
            int userIdPlayerOne = 1;
            int userIdPlayerTwo = 2;
            for (int i = 0; i < amountOfGames; i++)
            {
                int gameEntityId = _gameService.StartNewGameOrGetExistingId(userIdPlayerOne);
                while (_gameService.MayTheGameHaveMoreRounds(gameEntityId))
                {
                    Round round = _gameService.StartRoundOrGetExisting(gameEntityId);
                    int indexOf = IndexOfCorrectImageInRound(round);
                    //  _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntityId);
                }
            }

            int gameEntityIdOtherPlayer = _gameService.StartNewGameOrGetExistingId(userIdPlayerTwo);
            while (_gameService.MayTheGameHaveMoreRounds(gameEntityIdOtherPlayer))
            {
                Round round = _gameService.StartRoundOrGetExisting(gameEntityIdOtherPlayer);
                int indexOf = IndexOfCorrectImageInRound(round);
                //  _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntityIdOtherPlayer);
            }

            //     Assert.AreEqual(amountOfGames,
            //        _gameService.GetAllGamesForPlayer(userIdPlayerOne, DateTime.MinValue, DateTime.MaxValue).Count());

            //  Assert.AreEqual(1,
            //   _gameService.GetAllGamesForPlayer(userIdPlayerTwo, DateTime.MinValue, DateTime.MaxValue).Count());
        }

    }
}
