﻿using System;
using System.Collections.Generic;
using Who.BL.Domain;

namespace Who.BL.IServices
{
    public interface IGameService
    {
        int StartNewGameOrGetExistingId(int userId);

        bool MayTheGameHaveMoreRounds(int gameId);

        Round StartRoundOrGetExisting(int userId);

        bool AnswerRound(int answerImageId, int playerId);
        
        RoundInfo GetRoundInfo(int roundId);

        RoundInfo GetLatestRoundInfo(int userId);

        int RoundsPlayedInGame(int gameId);

        int GetRoundsPerGame();

        IEnumerable<Score> GetHighScoresForAllPlayers(DateTime startDate, DateTime endDate);

        PersonalScore GetCurrentScorePreviousScoreAndRank(int userId, DateTime startDate, DateTime endDate);
    }
}
