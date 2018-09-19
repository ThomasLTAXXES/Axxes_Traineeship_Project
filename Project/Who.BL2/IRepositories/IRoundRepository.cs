using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.BL.IServices;
using Who.Data;

namespace Who.BL.IRepositories
{
    public interface IRoundRepository : IRepository<RoundEntity>
    {
        IEnumerable<RoundEntity> GetRoundsForGame(int gameId);

        int GetAmountOfRoundsForGame(int gameId);

        RoundEntity GetCurrentOpenRound(int gameId);
    }
}
