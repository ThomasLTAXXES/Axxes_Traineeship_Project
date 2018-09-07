using System;
using System.Collections.Generic;

namespace Who.Data.Results
{
    public class GetHighScoresForAllPlayersResult
    {
        public IEnumerable<GetHighScoresForAllPlayersResultItem> Results { get; set; }
    }

    public class GetHighScoresForAllPlayersResultItem
    {
        public int AmountOfCorrectAnswers { get; set; }

        public int AmountOfRoundsPerGame { get; set; }

        public int UserId { get; set; }

        public TimeSpan Duration { get; set; }

        public int AmountOfGamesPlayed { get; set; }
    }
}
