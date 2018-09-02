using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Who.BL.Domain
{
   public class Score
    {
        public int Points { get; set; }

        public DateTime StartDate { get; set; }

        public TimeSpan Duration { get; set; }

        public int AmountOfCorrectAnswers { get; set; }

        public int AmountOfRoundsPlayed { get; set; }
    }
}
