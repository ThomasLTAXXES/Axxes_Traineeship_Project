﻿using System;

namespace Who.BL.Domain
{
    public class Score
    {
        public int AmountOfCorrectAnswers { get; set; }

        public int AmountOfRoundsPerGame { get; set; }

        public string FullName { get; set; }

        public TimeSpan Duration { get; set; }

        public int AmountOfGamesPlayed { get; set; }
    }
}
