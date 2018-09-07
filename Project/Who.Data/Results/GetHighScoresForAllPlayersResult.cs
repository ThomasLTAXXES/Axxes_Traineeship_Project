using System.Collections.Generic;

namespace Who.Data.Results
{
    public class GetHighScoresForAllPlayersResult
    {
        public IEnumerable<GetHighScoresForAllPlayersResultItem> Results { get; set; }
    }

    public class GetHighScoresForAllPlayersResultItem
    {
        public int Max { get; set; }

        public int Total { get; set; }

        public int UserId { get; set; }
    }
}
