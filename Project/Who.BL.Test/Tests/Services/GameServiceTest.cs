using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Who.BL.Domain;
using Who.BL.IServices;
using Who.BL.Services;
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
            _gameService = new GameService(_gameRepository, _imageRepository);
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
            GameEntity gameEntity = _gameService.StartGame(1);
            Assert.IsNotNull(gameEntity);
            Assert.IsNull(gameEntity.Rounds);
            Assert.AreEqual(0, gameEntity.Score);

            // We don't know the exact start date, but if it's between now and 5 minutes ago, it's okay
            Assert.IsTrue(DateTime.Now > gameEntity.StartDate);
            Assert.IsTrue(DateTime.Now.AddMinutes(-5) < gameEntity.StartDate);
        }

        [TestMethod]
        public void StartRoundTest()
        {
            GameEntity gameEntity = _gameService.StartGame(1);
            Round round = _gameService.StartRound(gameEntity.Id);
            Assert.IsNotNull(round);
            Assert.IsNotNull(round.Images);
            Assert.AreEqual(4, round.Images.Count); //TODO: config
            Assert.IsNotNull(round.Name);
        }

        [TestMethod]
        public void AnswerRoundCorrectlyTest()
        {
            GameEntity gameEntity = _gameService.StartGame(1);
            Round round = _gameService.StartRound(gameEntity.Id);
            int indexOf = IndexOfCorrectImageInRound(round);
            bool succes = _gameService.AnswerRound(round, indexOf, gameEntity.Id);
            Assert.IsTrue(succes);
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
            GameEntity gameEntity = _gameService.StartGame(1);
            Round round = _gameService.StartRound(gameEntity.Id);
            int indexOf = IndexOfCorrectImageInRound(round);
            bool succes = _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntity.Id);
            Assert.IsFalse(succes);
        }

        [TestMethod]
        public void PlayFullGameWithAllAnswersCorrectTest()
        {
            GameEntity gameEntity = _gameService.StartGame(1);
            while (_gameService.MayTheGameHaveMoreRounds(gameEntity.Id))
            {
                Round round = _gameService.StartRound(gameEntity.Id);
                int indexOf = IndexOfCorrectImageInRound(round);
                _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntity.Id);
            }
            //TODO: score testing
        }

        [ExpectedException(typeof(Exception))]
        [TestMethod]
        public void PlayTooManyRoundsInAGameTest()
        {
            GameEntity gameEntity = _gameService.StartGame(1);
            while (_gameService.MayTheGameHaveMoreRounds(gameEntity.Id))
            {
                Round round = _gameService.StartRound(gameEntity.Id);
                int indexOf = IndexOfCorrectImageInRound(round);
                _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntity.Id);
            }
            _gameService.StartRound(gameEntity.Id);
        }

        [TestMethod]
        public void GetAllGamesForPlayerTest()
        {
            int amountOfGames = 5;
            int userId = 1;
            for(int i=0; i < amountOfGames;i++)
            {
                GameEntity gameEntity = _gameService.StartGame(userId);
                while (_gameService.MayTheGameHaveMoreRounds(gameEntity.Id))
                {
                    Round round = _gameService.StartRound(gameEntity.Id);
                    int indexOf = IndexOfCorrectImageInRound(round);
                    _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntity.Id);
                }
            }

            Assert.AreEqual(amountOfGames, 
                _gameService.GetAllGamesForPlayer(userId, DateTime.MinValue, DateTime.MaxValue).Count());
        }

        [TestMethod]
        public void GetAllGamesForPlayerWithOtherGamesTest()
        {
            int amountOfGames = 5;
            int userIdPlayerOne = 1;
            int userIdPlayerTwo = 2;
            for (int i = 0; i < amountOfGames; i++)
            {
                GameEntity gameEntity = _gameService.StartGame(userIdPlayerOne);
                while (_gameService.MayTheGameHaveMoreRounds(gameEntity.Id))
                {
                    Round round = _gameService.StartRound(gameEntity.Id);
                    int indexOf = IndexOfCorrectImageInRound(round);
                    _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntity.Id);
                }
            }

            GameEntity gameEntityOtherPlayer = _gameService.StartGame(userIdPlayerTwo);
            while (_gameService.MayTheGameHaveMoreRounds(gameEntityOtherPlayer.Id))
            {
                Round round = _gameService.StartRound(gameEntityOtherPlayer.Id);
                int indexOf = IndexOfCorrectImageInRound(round);
                _gameService.AnswerRound(round, (indexOf + 1) % round.Images.Count, gameEntityOtherPlayer.Id);
            }

            Assert.AreEqual(amountOfGames,
                _gameService.GetAllGamesForPlayer(userIdPlayerOne, DateTime.MinValue, DateTime.MaxValue).Count());

            Assert.AreEqual(1,
              _gameService.GetAllGamesForPlayer(userIdPlayerTwo, DateTime.MinValue, DateTime.MaxValue).Count());
        }

    }
}
