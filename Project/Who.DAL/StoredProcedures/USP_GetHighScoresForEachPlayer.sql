alter procedure USP_GetHighScoresForEachPlayer
@p_AmountOfRounds int,
@p_StartDate datetime,
@p_EndDate datetime
as

create table #tmp (won int, total int, GameId int, UserId int);

insert into #tmp
  select sum(CASE WHEN GuessedImageId = CorrectImageId THEN 1 ELSE 0 END) as 'Won', 
  count(*) as 'Total', 
  a.Id as 'GameId',
  a.UserId as 'UserId'
  from Game a
  inner join [round] b on a.Id = b.GameId
  where a.StartDate between @p_StartDate and @p_EndDate
  group by a.Id,  a.UserId
  having count(*) = @p_AmountOfRounds
 


  select max(a.Won) as 'AmountOfCorrectAnswers', 
  a.Total as 'AmountOfRoundsPerGame', 
  a.UserId,
  (select top 1 c.Duration from #tmp tmp2 inner join Game c on tmp2.GameId = c.Id and tmp2.won = max(a.won) and tmp2.UserId = a.UserId order by c.Duration) as 'Duration',
  (select count(*) from #tmp tmp2 where a.UserId = tmp2.UserId group by tmp2.UserId) as 'AmountOfGamesPlayed'
  from #tmp a
  group by a.Total, a.Userid
  

  drop table #tmp;
  go
