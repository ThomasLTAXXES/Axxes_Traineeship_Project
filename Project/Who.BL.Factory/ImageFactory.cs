using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.BL.Domain;
using Who.Data;

namespace Who.BL.Factory
{
   public static class ImageFactory
    {
        public static Image Create(string url, int id)
        {
            return new Image
            {
                Url = url,
                Id = id
            };
        }


        public static Image Create(ImageInRoundEntity imageInRoundEntity)
        {
            return new Image
            {
                Id = imageInRoundEntity.ImageId,
                Url = imageInRoundEntity.Image.Url
            };
        }
    }
}
