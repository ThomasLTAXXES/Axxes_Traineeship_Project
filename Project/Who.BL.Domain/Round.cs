using System.Collections.Generic;
using Who.Data;

namespace Who.BL.Domain
{
    public class Round
    {
        public List<Image> Images { get; set; }

        public string Name { get; set; }

        public int AmountOfRoundsPlayed { get; set; }

        public int TotalRounds { get; set; }
    }
}
