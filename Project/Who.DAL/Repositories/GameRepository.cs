using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Who.BL.IRepositories;
using Who.DAL.Services;
using Who.Data;
using Who.Data.Results;

namespace Who.DAL.Repositories
{
    public class GameRepository : Repository<GameEntity>, IGameRepository
    {
        public GetHighScoresForAllPlayersResult GetHighScoresForAllPlayers(int amountOfRounds, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] sqlParams;
            using (var context = new ApplicationDbContext())
            {
                sqlParams = new SqlParameter[]
            {
                 new SqlParameter
                 {
                     ParameterName = "@p_AmountOfRounds",
                     SqlDbType = SqlDbType.Int,
                     Value  =amountOfRounds
                 },
                 new SqlParameter
                 {
                     ParameterName = "@p_StartDate",
                     SqlDbType = SqlDbType.DateTime,
                     Value  =startDate
                 },
                 new SqlParameter
                 {
                     ParameterName = "@p_EndDate",
                     SqlDbType = SqlDbType.DateTime,
                     Value  =endDate
                 }
            };
                return new GetHighScoresForAllPlayersResult
                {
                    Results = context.Database
                    .SqlQuery<GetHighScoresForAllPlayersResultItem>("USP_GetHighScoresForEachPlayer @p_AmountOfRounds, @p_StartDate, @p_EndDate", sqlParams)
                    .ToList()
                };
            }
        }
    }
}
