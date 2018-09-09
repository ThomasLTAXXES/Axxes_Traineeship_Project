using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Who.Data
{
    public class RoundEntity : Entity
    {
        public int? GuessedImageId { get; set; }

        public int CorrectImageId { get; set; }

        public int GameId { get; set; }

        public virtual ICollection<ImageInRoundEntity> ImagesInRound { get; set; }

        [ForeignKey(nameof(GuessedImageId))]
        public virtual ImageEntity GuessedImage { get; set; }

        [ForeignKey(nameof(CorrectImageId))]
        public virtual ImageEntity CorrectImage { get; set; }

        [ForeignKey(nameof(GameId))]
        public virtual GameEntity Game { get; set; }
    }
}
