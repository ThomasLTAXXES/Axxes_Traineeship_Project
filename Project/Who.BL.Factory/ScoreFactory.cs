using System.Collections.Generic;
using Who.BL.Domain;
using Who.Data;
using Who.Data.Results;

namespace Who.BL.Factory
{
    public static class ScoreFactory
    {
        public static Score Create(GetHighScoresForAllPlayersResultItem item, Dictionary<int, UserEntity> users)
        {
            return new Score
            {
                AmountOfCorrectAnswers = item.AmountOfCorrectAnswers,
                Duration = item.Duration,
                AmountOfGamesPlayed = item.AmountOfGamesPlayed,
                AmountOfRoundsPerGame = item.AmountOfRoundsPerGame,
                FullName = users[item.UserId].FullName
            };
        }
    }
}
