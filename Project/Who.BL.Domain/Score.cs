using System;

namespace Who.BL.Domain
{
    public class Score
    {
        public TimeSpan DurationOfBestGame { get; set; }

        public int AmountOfCorrectRoundsInBestGame { get; set; }

        public int TotalAnswersInBestGame { get; set; }

        public int AmountOfGamesPlayed { get; set; }
    }
}
