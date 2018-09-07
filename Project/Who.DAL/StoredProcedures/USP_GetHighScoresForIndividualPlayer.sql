create procedure USP_GetHighScoresForIndividualPlayer
@p_AmountOfRounds int,
@p_StartDate datetime,
@p_EndDate datetime,
@p_UserId int
as
  select sum(CASE WHEN GuessedImageId = CorrectImageId THEN 1 ELSE 0 END) as 'AmountOfCorrectAnswers', 
  count(*) as 'AmountOfRoundsPerGame', 
  a.Id as 'GameId',
  a.Duration
  from Game a
  inner join [round] b on a.Id = b.GameId
  where a.StartDate between @p_StartDate and @p_EndDate and a.UserId = @p_UserId
  group by a.Id, a.Duration
  having count(*) = @p_AmountOfRounds
 
  go
