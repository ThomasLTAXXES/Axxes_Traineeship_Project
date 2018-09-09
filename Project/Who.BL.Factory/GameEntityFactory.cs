using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.Data;

namespace Who.BL.Factory
{
   public static class GameEntityFactory
    {
        public static GameEntity Create(int userId, DateTime startDate)
        {
            return new GameEntity
            {
                UserId = userId,
                StartDate = startDate
            };
        }
    }
}
