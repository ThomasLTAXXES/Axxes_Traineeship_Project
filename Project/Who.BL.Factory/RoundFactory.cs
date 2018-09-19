using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.BL.Domain;

namespace Who.BL.Factory
{
   public static class RoundFactory
    {
        public static Round Create(List<Image> images, int amountOfRoundsPlayed, int totalRounds, string name)
        {
            return new Round
            {
                Images = images,
                AmountOfRoundsPlayed = amountOfRoundsPlayed,
                TotalRounds = totalRounds,
                Name = name
            };
        }
    }
}
