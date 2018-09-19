using Who.BL.Domain;
using Who.Data.Results;

namespace Who.BL.Factory
{
    public static class PersonalScoreItemFactory
    {
        public static PersonalScoreItem Create(GetHighScoresForIndividualPlayerResultItem item)
        {
            return new PersonalScoreItem
            {
                AmountOfCorrectAnswers = item.AmountOfCorrectAnswers,
                AmountOfRoundsPerGame = item.AmountOfRoundsPerGame,
                Duration = item.Duration
            };
        }
    }
}
