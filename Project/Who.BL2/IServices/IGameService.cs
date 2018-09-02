using System;
using System.Collections.Generic;
using Who.BL.Domain;
using Who.Data;

namespace Who.BL.IServices
{
    public interface IGameService
    {
        int StartGame(int userId);

        bool MayTheGameHaveMoreRounds(int gameId);

        Round StartRound(int gameId);

        bool AnswerRound(Round round, int answer, int gameId);

        IEnumerable<Score> GetAllGamesForPlayer(int userId, DateTime startDate, DateTime endDate);
    }
}
