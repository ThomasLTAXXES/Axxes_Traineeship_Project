using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Who.Data
{
    public class RoundEntity : Entity
    {
        public int? GuessedImageId { get; set; }

        public int CorrectImageId { get; set; }

        public int GameId { get; set; }

        public ICollection<ImageInRoundEntity> ImagesInRound { get; set; }

        [ForeignKey(nameof(GuessedImageId))]
        public ImageEntity GuessedImage { get; set; }

        [ForeignKey(nameof(CorrectImageId))]
        public ImageEntity CorrectImage { get; set; }

        [ForeignKey(nameof(GameId))]
        public GameEntity Game { get; set; }
    }
}
