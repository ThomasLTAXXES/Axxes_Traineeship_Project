using System;
using Who.BL.IServices;
using Who.Data;
using Who.Data.Results;

namespace Who.BL.IRepositories
{
    public interface IGameRepository : IRepository<GameEntity>
    {
        GetHighScoresForAllPlayersResult GetHighScoresForAllPlayers(int amountOfRounds, DateTime startDate, DateTime endDate);
    }
}
