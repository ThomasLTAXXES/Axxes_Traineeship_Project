using System.Collections.Generic;
using Who.BL.IServices;
using Who.Data;
using Who.Data.Results;

namespace Who.BL.IRepositories
{
    public interface IGameRepository : IRepository<GameEntity>
    {
        GetHighScoresForAllPlayersResult GetHighScoresForAllPlayers();
    }
}
