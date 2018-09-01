using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Who.Data
{
    public class Game : Entity
    {
        public User User { get; set; }
        public ICollection<Round> Rounds { get; set; }
        public int Score { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
