using System.Collections.Generic;
using System.Linq;
using Who.BL.IRepositories;
using Who.Data;

namespace Who.DAL.Repositories
{
    public class RoundRepository : Repository<RoundEntity>, IRoundRepository
    {
        public int GetAmountOfRoundsForGame(int gameId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Rounds.Where(x => x.GameId == gameId)?.Count() ?? 0;
            }
        }

        public RoundEntity GetCurrentOpenRound(int gameId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Rounds
                    .Include("ImagesInRound")
                    .Include("ImagesInRound.Image")
                    .Include("CorrectImage")
                    .FirstOrDefault(r => r.GameId == gameId && null == r.GuessedImageId);
            }
        }

        public IEnumerable<RoundEntity> GetRoundsForGame(int gameId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Rounds.Where(x => x.GameId == gameId).ToList();
            }
        }
    }
}
