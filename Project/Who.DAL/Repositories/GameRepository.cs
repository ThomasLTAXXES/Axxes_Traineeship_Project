using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.BL.IRepositories;
using Who.DAL.Services;
using Who.Data;
using Who.Data.Results;

namespace Who.DAL.Repositories
{
    public class GameRepository : Repository<GameEntity>, IGameRepository
    {
        public GetHighScoresForAllPlayersResult GetHighScoresForAllPlayers()
        {
            using (var context = new ApplicationDbContext())
            {
                var clientIdParameter = new SqlParameter("@ClientId", 4);

                return new GetHighScoresForAllPlayersResult {
                    Results = context.Database
                    .SqlQuery<GetHighScoresForAllPlayersResultItem>("GetResultsForCampaign @ClientId", clientIdParameter)
                    .ToList()
            };
            }
        }
    }
}
