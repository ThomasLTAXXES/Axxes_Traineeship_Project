using System;
using System.Collections.Generic;
using Who.BL.Domain;

namespace Who.BL.IServices
{
    public interface IGameService
    {
        int StartGame(int userId);

        bool MayTheGameHaveMoreRounds(int gameId);

        Round StartRound(int gameId);

        bool AnswerRound(int answerImageId, int playerId);

        IEnumerable<Score> GetAllHighScores(int userId, DateTime startDate, DateTime endDate);

        RoundInfo GetRoundInfo(int roundId);

        RoundInfo GetLatestRoundInfo(int userId);

        int RoundsPlayedInGame(int gameId);

        int GetRoundsPerGame();
    }
}
