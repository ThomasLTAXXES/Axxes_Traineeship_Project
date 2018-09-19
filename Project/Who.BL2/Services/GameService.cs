using System;
using System.Collections.Generic;
using System.Linq;
using Who.BL.Domain;
using Who.BL.Factory;
using Who.BL.IRepositories;
using Who.BL.IServices;
using Who.Data;
using Who.Data.Enums;
using Who.Data.Results;

namespace Who.BL.Services
{
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        private IImageRepository _imageRepository;
        private IRoundRepository _roundRepository;
        private IRepository<ImageInRoundEntity> _imageInRoundRepository;
        private IMetaDataRepository _metaDataRepository;
        private IRepository<UserEntity> _userRepository;
        private readonly int IMAGES_PER_ROUND; 
        public readonly int ROUNDS_PER_GAME;

        public GameService(
            IGameRepository gameRepository,
            IImageRepository imageRepository,
            IRoundRepository roundRepository,
            IRepository<ImageInRoundEntity> imageInRoundRepository,
            IRepository<UserEntity> userRepository,
            IMetaDataRepository metaDataRepository)
        {
            _gameRepository = gameRepository;
            _imageRepository = imageRepository;
            _roundRepository = roundRepository;
            _imageInRoundRepository = imageInRoundRepository;
            _userRepository = userRepository;
            _metaDataRepository = metaDataRepository;
            IMAGES_PER_ROUND = _metaDataRepository.GetByName<int>(MetaDataEnum.ImagesPerRound);
            ROUNDS_PER_GAME = _metaDataRepository.GetByName<int>(MetaDataEnum.RoundsPerGame);
        }

        private GameEntity StartNewGameOrGetExisting(int userId)
        {
            GameEntity lastGame = _gameRepository.GetLatestGameForUser(userId);

            // When a user never has played a game before, it'll be null
            if (null == lastGame || !MayTheGameHaveMoreRounds(lastGame.Id))
            {
                lastGame = GameEntityFactory.Create(userId, DateTime.Now);
                _gameRepository.Create(lastGame);
            }
            return lastGame;
        }

        public int StartNewGameOrGetExistingId(int userId)
        {
            return StartNewGameOrGetExisting(userId).Id;
        }

        public bool MayTheGameHaveMoreRounds(int gameId)
        {
            return RoundsPlayedInGame(gameId) < ROUNDS_PER_GAME;
        }

        public int RoundsPlayedInGame(int gameId)
        {
            return _roundRepository.GetAmountOfRoundsForGame(gameId);
        }

        public Round StartRoundOrGetExisting(int userId)
        {
            GameEntity gameEntity = StartNewGameOrGetExisting(userId);
            RoundEntity roundEntity = _roundRepository.GetCurrentOpenRound(gameEntity.Id);
            if (null != roundEntity)
            {
                // Unfinished round
                return RoundEntityFactory.Create(roundEntity, ROUNDS_PER_GAME, RoundsPlayedInGame(gameEntity.Id));
            }
            roundEntity = RoundEntityFactory.Create(gameEntity.Id);

            // Determine the correct one
            var randomImages = _imageRepository.GetRandomImages(IMAGES_PER_ROUND, _userRepository.Get(userId).FullName);
            int indexOfCorrectImageInList = (new Random()).Next(IMAGES_PER_ROUND);
            var correctImage = randomImages.ElementAt(indexOfCorrectImageInList);

            Round round = RoundFactory.Create(new List<Image>(), RoundsPlayedInGame(gameEntity.Id), ROUNDS_PER_GAME, correctImage.Name);

            roundEntity.CorrectImageId = correctImage.Id;
            _roundRepository.Create(roundEntity);

            foreach (ImageEntity imageEntity in randomImages)
            {
                ImageInRoundEntity imageInRoundEntity = ImageInRoundEntityFactory.Create(imageEntity.Id, roundEntity.Id);
                _imageInRoundRepository.Create(imageInRoundEntity);
                round.Images.Add(ImageFactory.Create(imageEntity.Url, imageEntity.Id));
            }

            return round;
        }

        public bool AnswerRound(int answerImageId, int userId)
        {
            GameEntity currentGameForPlayer = _gameRepository.GetLatestGameForUser(userId);

            // id should be replaced with start date
            RoundEntity currentRound = _roundRepository.GetCurrentOpenRound(currentGameForPlayer.Id);

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
            // One extra database round trip to get all user names... on second thought, this should've been in the USP
            Dictionary<int, UserEntity> userIds = _userRepository.GetAll(dbResult.Results.Select(r => r.UserId));
            return dbResult.Results
                .Select(r => ScoreFactory.Create(r,userIds))
                .OrderBy(x => x.AmountOfCorrectAnswers)
                .ThenBy(x => x.Duration).ToList();
        }

        public PersonalScore GetCurrentScorePreviousScoreAndRank(int userId, DateTime startDate, DateTime endDate)
        {
            PersonalScore personalScore = new PersonalScore();
            List<GetHighScoresForAllPlayersResultItem> dbResult = _gameRepository.GetHighScoresForAllPlayers(ROUNDS_PER_GAME, startDate, endDate).Results.OrderBy(x => x.AmountOfCorrectAnswers).ThenBy(x => x.Duration).ToList();
            // Set the rank
            for (int i = 0; i < dbResult.Count(); i++)
            {
                if (userId == dbResult[i].UserId)
                {
                    personalScore.Rank = i + 1; // 0 indexed but our ranks start at 1
                    break;
                }
            }

            GetHighScoresForIndividualPlayerResult highScoresIndividualResult = _gameRepository.GetHighScoresForIndividualPlayer(ROUNDS_PER_GAME, startDate, endDate, userId);
            personalScore.PersonalScoreItems = highScoresIndividualResult.Results.Select(PersonalScoreItemFactory.Create);

            return personalScore;
        }

        public RoundInfo GetRoundInfo(int roundId)
        {//TODO optimize
            RoundInfo roundInfo = new RoundInfo();

            RoundEntity roundEntity = _roundRepository.Get(roundId);
            IEnumerable<ImageInRoundEntity> imageInRoundEntity = _imageInRoundRepository.GetAll().Where(x => x.RoundId == roundId);
            roundInfo.GameId = roundEntity.GameId;
            roundInfo.CorrectImageId = roundEntity.CorrectImageId;
            roundInfo.GuessedImageId = roundEntity.GuessedImageId.Value;
            roundInfo.Name = _imageRepository.Get(roundEntity.CorrectImageId).Name; 
            roundInfo.Images = imageInRoundEntity.Select(x => new Image { Id = x.ImageId, Url = _imageRepository.Get(x.ImageId).Url }).ToList();
            roundInfo.AmountOfRoundsPlayed = RoundsPlayedInGame(roundEntity.GameId);
            roundInfo.TotalRounds = ROUNDS_PER_GAME;

            return roundInfo;
        }

        public RoundInfo GetLatestRoundInfo(int userId)
        {//TODO optimize
            GameEntity lastGame = _gameRepository.GetLatestGameForUser(userId);
            RoundEntity lastRound = _roundRepository.GetAll().Where(x => x.GameId == lastGame.Id).OrderByDescending(x => x.Id).FirstOrDefault();

            return GetRoundInfo(lastRound.Id);
        }

        public int GetRoundsPerGame()
        {
            return ROUNDS_PER_GAME;
        }
    }
}
