using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Who.Data
{
    public class GameEntity : Entity
    {
        public int UserId { get; set; }
        public UserEntity User { get; set; }
        public ICollection<RoundEntity> Rounds { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
