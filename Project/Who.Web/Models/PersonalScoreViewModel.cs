using System.Collections.Generic;

namespace Who.Web.Models
{
    public class PersonalScoreViewModel
    {
        public List<PersonalScoreItemViewModel> PersonalScoreItems { get; set; }

        public int Rank { get; set; }
    }

    public class PersonalScoreItemViewModel
    {
        public System.TimeSpan Duration { get; set; }

        public int AmountOfCorrectAnswers { get; set; }

        public int AmountOfRoundsPerGame { get; set; }
    }
}