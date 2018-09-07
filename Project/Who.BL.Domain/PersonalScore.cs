using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Who.BL.Domain
{
    public class PersonalScore
    {
        public IEnumerable<PersonalScoreItem> PersonalScoreItems { get; set; }

        public int Rank { get; set; }
    }

    public class PersonalScoreItem
    {
        public TimeSpan Duration { get; set; }

        public int AmountOfCorrectAnswers { get; set; }

        public int AmountOfRoundsPerGame { get; set; }
    }
}
