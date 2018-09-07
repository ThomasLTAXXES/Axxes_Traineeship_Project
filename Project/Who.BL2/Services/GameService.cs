using System;
using System.Collections.Generic;
using System.Linq;
using Who.BL.Domain;
using Who.BL.IRepositories;
using Who.BL.IServices;
using Who.Data;
using Who.Data.Results;
using Who.Utils;

namespace Who.BL.Services
{
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        private IRepository<ImageEntity> _imageRepository;
        private IRepository<RoundEntity> _roundRepository;
        private IRepository<ImageInRoundEntity> _imageInRoundRepository;
        private IRepository<MetaDataEntity> _metaDataRepository;
        private IRepository<UserEntity> _userRepository;
        private const int IMAGES_PER_ROUND = 4; //TODO: move to config or db
        public const int ROUNDS_PER_GAME = 5;//TODO: move to config or db (should be 20 but testing)

        public GameService(
            IGameRepository gameRepository,
            IRepository<ImageEntity> imageRepository,
            IRepository<RoundEntity> roundRepository,
            IRepository<ImageInRoundEntity> imageInRoundRepository,
            IRepository<UserEntity> userRepository)
        {
            _gameRepository = gameRepository;
            _imageRepository = imageRepository;
            _roundRepository = roundRepository;
            _imageInRoundRepository = imageInRoundRepository;
            _userRepository = userRepository;
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
            List<ImageInRoundEntity> imageInRoundEntitiesToSave = new List<ImageInRoundEntity>(IMAGES_PER_ROUND);
            _roundRepository.Create(roundEntity);
            roundEntity.ImagesInRound = new List<ImageInRoundEntity>();
            ImageInRoundEntity imageInRoundEntityCorrect = new ImageInRoundEntity { ImageId = image.Id, RoundId = roundEntity.Id };
            imageInRoundEntitiesToSave.Add(imageInRoundEntityCorrect);
            roundEntity.ImagesInRound.Add(imageInRoundEntityCorrect);

            round.Name = image.Name;

            for (int i = roundEntity.ImagesInRound.Count; i < IMAGES_PER_ROUND; i++)
            {
                bool alreadyInList = false;
                while (!alreadyInList)
                {
                    image = images.ElementAt(rand.Next(images.Count()));
                    if (!roundEntity.ImagesInRound.Any(iir => iir.ImageId == image.Id))
                    {
                        round.Images.Add(new Image
                        {
                            Id = image.Id,
                            Url = image.Url
                        });
                        ImageInRoundEntity imageInRoundEntity = new ImageInRoundEntity { ImageId = image.Id, RoundId = roundEntity.Id };
                        roundEntity.ImagesInRound.Add(imageInRoundEntity);
                        imageInRoundEntitiesToSave.Add(imageInRoundEntity);
                        alreadyInList = true;
                    }
                }
            }

            if (game.Rounds == null)
            {
                game.Rounds = new List<RoundEntity>();
            }

            Random rng = new Random();

            int n = round.Images.Count();
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Swap(round.Images, n, k);
                Swap(imageInRoundEntitiesToSave, n, k);
            }
            
            foreach (ImageInRoundEntity imageInRoundEntityToSave in imageInRoundEntitiesToSave)
            {
                _imageInRoundRepository.Create(imageInRoundEntityToSave);
            }

            game.Rounds.Add(roundEntity);

            _gameRepository.Update(game);

            return round;
        }

        private static void Swap<T>(List<T> list, int n, int k)
        {
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
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


        public IEnumerable<Score> GetHighScoresForAllPlayers(DateTime startDate, DateTime endDate)
        {
            GetHighScoresForAllPlayersResult dbResult = _gameRepository.GetHighScoresForAllPlayers(ROUNDS_PER_GAME, startDate, endDate);

            return dbResult.Results.Select(r =>
             new Score
             {
                 AmountOfCorrectAnswers = r.AmountOfCorrectAnswers,
                 Duration = r.Duration,
                 AmountOfGamesPlayed = r.AmountOfGamesPlayed,
                 AmountOfRoundsPerGame = r.AmountOfRoundsPerGame,
                 FullName = _userRepository.Get(r.UserId).FullName
             }).OrderBy(x => x.AmountOfCorrectAnswers).ThenBy(x => x.Duration).ToList();
        }

        public PersonalScore GetCurrentScorePreviousScoreAndRank(int userId, DateTime startDate, DateTime endDate)
        {
            PersonalScore personalScore = new PersonalScore();
            List<GetHighScoresForAllPlayersResultItem> dbResult = _gameRepository.GetHighScoresForAllPlayers(ROUNDS_PER_GAME, startDate, endDate).Results.OrderBy(x => x.AmountOfCorrectAnswers).ThenBy(x => x.Duration).ToList();
            for (int i = 0; i < dbResult.Count(); i++)
            {
                if (userId == dbResult[i].UserId)
                {
                    personalScore.Rank = i +1;
                    break;
                }
            }

            GetHighScoresForIndividualPlayerResult highScoresIndividualResult = _gameRepository.GetHighScoresForIndividualPlayer(ROUNDS_PER_GAME, startDate, endDate, userId);
            personalScore.PersonalScoreItems = highScoresIndividualResult.Results
                .Select(r => new PersonalScoreItem
                {
                    AmountOfCorrectAnswers = r.AmountOfCorrectAnswers,
                    AmountOfRoundsPerGame = r.AmountOfRoundsPerGame,
                    Duration = r.Duration
                });

            return personalScore;
        }

        public RoundInfo GetRoundInfo(int roundId)
        {
            RoundInfo roundInfo = new RoundInfo();

            RoundEntity roundEntity = _roundRepository.Get(roundId);
            IEnumerable<ImageInRoundEntity> imageInRoundEntity = _imageInRoundRepository.GetAll().Where(x => x.RoundId == roundId);
            roundInfo.GameId = roundEntity.GameId;
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
