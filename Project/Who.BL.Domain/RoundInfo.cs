using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Who.BL.Domain
{
    public class RoundInfo
    {
        public int CorrectImageId { get; set; }

        public int GuessedImageId { get; set; }

        public string Name { get; set; }

        public List<Image> Images { get; set; }

        public int AmountOfRoundsPlayed { get; set; }

        public int TotalRounds { get; set; }
    }
}
