using System;
using System.Collections.Generic;

namespace Who.Data.Results
{
    public class GetHighScoresForIndividualPlayerResult
    {
        public IEnumerable<GetHighScoresForIndividualPlayerResultItem> Results { get; set; }
    }

    public class GetHighScoresForIndividualPlayerResultItem
    {
        public int AmountOfCorrectAnswers { get; set; }

        public int AmountOfRoundsPerGame { get; set; }

        public int GameId { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
