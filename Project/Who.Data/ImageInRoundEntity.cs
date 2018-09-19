using System;
using System.Collections.Generic;
using System.Text;

namespace Who.Data
{
    public class ImageInRoundEntity : Entity
    {

        public int ImageId { get; set; }

        public int RoundId { get; set; }

        public virtual ImageEntity Image { get; set; }

        public virtual RoundEntity Round { get; set; }
    }
}
