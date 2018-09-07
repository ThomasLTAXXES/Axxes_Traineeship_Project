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
        private IRepository<RoundEntity> _roundRepository;
        private IRepository<ImageInRoundEntity> _imageInRoundRepository;
        private IRepository<MetaDataEntity> _metaDataRepository;
        private const int IMAGES_PER_ROUND = 4; //TODO: move to config or db
        public const int ROUNDS_PER_GAME = 5;//TODO: move to config or db (should be 20 but testing)

        public GameService(
            IRepository<GameEntity> gameRepository,
            IRepository<ImageEntity> imageRepository,
            IRepository<RoundEntity> roundRepository,
            IRepository<ImageInRoundEntity> imageInRoundRepository)
        {
            _gameRepository = gameRepository;
            _imageRepository = imageRepository;
            _roundRepository = roundRepository;
            _imageInRoundRepository = imageInRoundRepository;
        }

        public int StartGame(int userId)
        {
            GameEntity lastGame = _gameRepository.GetAll().OrderByDescending(x => x.StartDate).FirstOrDefault(x => x.UserId == userId);

            if (null != lastGame && MayTheGameHaveMoreRounds(lastGame.Id))
            {
                return lastGame.Id;
            }

            GameEntity game = new GameEntity
            {
                UserId = userId,
                StartDate = DateTime.Now
            };

            return _gameRepository.Create(game).Id;
        }

        public bool MayTheGameHaveMoreRounds(int gameId)
        {
            return RoundsPlayedInGame(gameId) < ROUNDS_PER_GAME;
        }

        public int RoundsPlayedInGame(int gameId)
        {
            GameEntity game = _gameRepository.Get(gameId);
            IEnumerable<RoundEntity> rounds = _roundRepository.GetAll().Where(x => x.GameId == game.Id);

            return rounds == null ? 0 : rounds.Count();
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
                Images = new List<Image>(),
                AmountOfRoundsPlayed = _roundRepository.GetAll().Where(x => x.GameId == gameId).Count(), //TODO: include,
                TotalRounds = ROUNDS_PER_GAME
            };

            var rand = new Random();

            // Add correct one
            var image = images.ElementAt(rand.Next(images.Count()));
            round.Images.Add(new Image
            {
                Url = image.Url,
                Id = image.Id
            });

            RoundEntity roundEntity = new RoundEntity
            {
                GameId = gameId,
                CorrectImageId = image.Id
            };
            _roundRepository.Create(roundEntity);
            roundEntity.ImagesInRound = new List<ImageInRoundEntity>();
            ImageInRoundEntity imageInRoundEntityCorrect = new ImageInRoundEntity { ImageId = image.Id, RoundId = roundEntity.Id };
            roundEntity.ImagesInRound.Add(imageInRoundEntityCorrect);
            _imageInRoundRepository.Create(imageInRoundEntityCorrect);

            round.Name = image.Name;

            for (int i = roundEntity.ImagesInRound.Count; i < IMAGES_PER_ROUND; i++)
            {
                bool alreadyInList = false;
                while (!alreadyInList)
                {
                    image = images.ElementAt(rand.Next(images.Count()));
                    if (!roundEntity.ImagesInRound.Any(iir=>iir.Id == image.Id))
                    {
                        round.Images.Add(new Image
                        {
                            Id = image.Id,
                            Url = image.Url
                        });
                        ImageInRoundEntity imageInRoundEntity = new ImageInRoundEntity { ImageId = image.Id, RoundId = roundEntity.Id };
                        roundEntity.ImagesInRound.Add(imageInRoundEntity);
                        _imageInRoundRepository.Create(imageInRoundEntity);
                        alreadyInList = true;
                    }
                }
            }

            if (game.Rounds == null)
            {
                game.Rounds = new List<RoundEntity>();
            }

            // Randomize the order (otherwise #1 would always be the correct answer)
            // round.Images.Shuffle();//commented this out to make it easier to test


            game.Rounds.Add(roundEntity);

            _gameRepository.Update(game);

            return round;
        }

        public bool AnswerRound(int answerImageId, int playerId)
        {
            GameEntity currentGameForPlayer = _gameRepository
                 .GetAll()
                 .Where(x => x.UserId == playerId)
                 .OrderByDescending(x => x.StartDate)
                 .FirstOrDefault();

            // id should be replaced with start date
            RoundEntity currentRound = _roundRepository.GetAll()
                .Where(x => x.GameId == currentGameForPlayer.Id)
                .OrderByDescending(x => x.Id).FirstOrDefault();

            ImageEntity answerImage = _imageRepository.Get(answerImageId);

            currentRound.GuessedImageId = answerImage.Id;
            currentGameForPlayer.Duration = DateTime.Now - currentGameForPlayer.StartDate;

            _gameRepository.Update(currentGameForPlayer);
            _roundRepository.Update(currentRound);

            return currentRound.GuessedImageId == currentRound.CorrectImageId;
        }


        public IEnumerable<Score> GetAllHighScores(int userId, DateTime startDate, DateTime endDate)
        {
            /*IEnumerable<GameEntity> relevantGameEntities = _gameRepository.GetAll()
                .Where(g => g.StartDate.BetweenIncludeBoundaries(startDate, endDate) 
                && !MayTheGameHaveMoreRounds(g.Id));*/

            // RoundEntity roundEntity = _roundRepository.GetAll().Where(x => relevantGameEntities.Any(y => x.GameId == y.UserId)).OrderByDescending(x=>x.);

            /* return relevantGameEntities.Select(g => new Score
              {
                  AmountOfGamesPlayed = relevantGameEntities.Where(x2 => x2.UserId == g.UserId).Count(),
                  Duration = TimeSpan.FromMilliseconds(_gameRepository.GetAll().Where(x=>x.UserId == g.UserId).Sum(x=>x.Duration.TotalMilliseconds)),
                  AmountOfCorrectAnswers = g.Rounds.Where(x => x.CorrectImageId == x.GuessedImageId).Count()
              }).ToList();*/

            return null;
        }

        public RoundInfo GetRoundInfo(int roundId)
        {
            RoundInfo roundInfo = new RoundInfo();

            RoundEntity roundEntity = _roundRepository.Get(roundId);
            IEnumerable<ImageInRoundEntity> imageInRoundEntity = _imageInRoundRepository.GetAll().Where(x => x.RoundId == roundId);
            roundInfo.CorrectImageId = roundEntity.CorrectImageId;
            roundInfo.GuessedImageId = roundEntity.GuessedImageId.Value;
            roundInfo.Name = _imageRepository.Get(roundEntity.CorrectImageId).Name; //TODO include
            roundInfo.Images = imageInRoundEntity.Select(x => new Image { Id = x.ImageId, Url = _imageRepository.Get(x.ImageId).Url }).ToList();
            roundInfo.AmountOfRoundsPlayed = _roundRepository.GetAll().Where(x => x.GameId == roundEntity.GameId).Count(); //TODO: include
            roundInfo.TotalRounds = ROUNDS_PER_GAME;

            return roundInfo;
        }

        public RoundInfo GetLatestRoundInfo(int userId)
        {
            GameEntity lastGame = _gameRepository.GetAll().Where(x => x.UserId == userId).OrderByDescending(x => x.StartDate).FirstOrDefault();
            RoundEntity lastRound = _roundRepository.GetAll().Where(x => x.GameId == lastGame.Id).OrderByDescending(x => x.Id).FirstOrDefault();

            return GetRoundInfo(lastRound.Id);
        }

        public int GetRoundsPerGame()
        {
            return ROUNDS_PER_GAME;// metaDataRepository.;
        }
    }
}
