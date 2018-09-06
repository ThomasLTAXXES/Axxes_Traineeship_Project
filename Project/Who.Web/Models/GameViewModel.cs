using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Who.Web.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public int AmountOfRoundsPlayed { get; set; }

        public int TotalRounds { get; set; }
    }
}