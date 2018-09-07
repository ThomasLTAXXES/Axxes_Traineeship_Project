using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Who.Web.Models
{
    public class RoundAnsweredViewModel
    {
        public int GameId { get; set; }
        public int CorrectImageId { get; set; }

        public int GuessedImageId { get; set; }

        public string Name { get; set; }

        public List<ImageViewModel> Images { get; set; }

        public int AmountOfRoundsPlayed { get; set; }

        public int TotalRounds { get; set; }
    }
}