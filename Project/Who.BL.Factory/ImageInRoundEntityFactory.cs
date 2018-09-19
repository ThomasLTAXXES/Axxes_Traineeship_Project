using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.Data;

namespace Who.BL.Factory
{
    public static class ImageInRoundEntityFactory
    {
        public static ImageInRoundEntity Create(int imageId, int roundId)
        {
            return new ImageInRoundEntity
            {
                ImageId = imageId,
                RoundId = roundId
            };
        }
    }
}
