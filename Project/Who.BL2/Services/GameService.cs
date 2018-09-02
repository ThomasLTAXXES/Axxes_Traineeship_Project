using System;
using System.Collections.Generic;
using System.Linq;
using Who.BL.Domain;
using Who.BL.IServices;
using Who.Data;
using Who.Utils;

namespace Who.BL.Services
{
    public class GameService : IGameService
    {
        private IRepository<GameEntity> _gameRepository;
        private IRepository<ImageEntity> _imageRepository;
        private const int IMAGES_PER_ROUND = 4; //TODO: move to config or db
        public const int ROUNDS_PER_GAME = 20;//TODO: move to config or db

        public GameService(IRepository<GameEntity> gameRepository, IRepository<ImageEntity> imageRepository)
        {
            _gameRepository = gameRepository;
            _imageRepository = imageRepository;
        }

        public GameEntity StartGame(int userId)
        {
            GameEntity game = new GameEntity
            {
                UserId = userId,
                StartDate = DateTime.Now
            };

            return _gameRepository.Create(game);
        }

        public bool MayTheGameHaveMoreRounds(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            return game.Rounds == null || game.Rounds.Count < ROUNDS_PER_GAME;
        }

        public Round StartRound(int gameId)
        {
            var game = _gameRepository.Get(gameId);
            if (game.Rounds == null)
            {
                game.Rounds = new List<RoundEntity>();
            }
            else if (game.Rounds.Count >= ROUNDS_PER_GAME)
            {
                // TODO: think about custom exceptions
                throw new Exception("You can't create any more rounds for this game");
            }
            IEnumerable<ImageEntity> images = _imageRepository.GetAll();
            Round round = new Round
            {
                Images = new List<Image>()
            };

            var rand = new Random();

            // Add correct one
            var image = images.ElementAt(rand.Next(images.Count()));
            round.Images.Add(new Image
            {
                Url = image.Url
            });

            round.Name = image.Name;

            for (int i = 1; i < IMAGES_PER_ROUND; i++)
            {
                round.Images.Add(new Image
                {

                    Url = images.ElementAt(rand.Next(images.Count())).Url
                });
            }
            // Randomize the order (otherwise #1 would always be the correct answer)
            round.Images.Shuffle();

            if (game.Rounds == null)
            {
                game.Rounds = new List<RoundEntity>();
            }
            game.Rounds.Add(new RoundEntity());
            _gameRepository.Update(game);

            return round;
        }

        public bool AnswerRound(Round round, int answer, int gameId)
        {
            bool succes = false;
            var images = _imageRepository.GetAll();
            var image = images.First(i => i.Url == round.Images[answer].Url);
            if (round.Name.Equals(image.Name))
            {
                succes = true;
            }

            var game = _gameRepository.Get(gameId);
            game.Duration = DateTime.Now - game.StartDate;
            game.Rounds.Last().CorrectAnswer = succes;
            if(game.Rounds.Count == ROUNDS_PER_GAME)
            {

            }
            _gameRepository.Update(game);

            return succes;
        }

        private void CalculateScore(GameEntity gameEntity)
        {
            //TODO: everything
            gameEntity.Score = 1;
        }

        public IEnumerable<GameEntity> GetAllGamesForPlayer(int userId, DateTime startDate, DateTime endDate)
        {
            return _gameRepository.GetAll().Where(g => g.UserId == userId && g.StartDate.BetweenIncludeBoundaries(startDate, endDate));
        }
    }
}
