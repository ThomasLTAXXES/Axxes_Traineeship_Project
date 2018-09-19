using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.BL.Domain;
using Who.Data;

namespace Who.BL.Factory
{
    public static class RoundEntityFactory
    {
        public static RoundEntity Create(int gameId)
        {
            return new RoundEntity
            {
                GameId = gameId
            };
        }

        public static RoundEntity Create(int gameId, int correctImageId)
        {
            return new RoundEntity
            {
                GameId = gameId,
                CorrectImageId = correctImageId
            };
        }

        public static Round Create(RoundEntity roundEntity, int totalRounds, int amountOfRoundsPlayed)
        {
            Round round = new Round
            {
                Name = roundEntity.CorrectImage.Name,
                TotalRounds = totalRounds,
                AmountOfRoundsPlayed = amountOfRoundsPlayed,
                Images = roundEntity.ImagesInRound.Select(ImageFactory.Create).ToList()
            };

            return round;
        }

    }
}
