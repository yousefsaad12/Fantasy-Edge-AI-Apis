

namespace Api.Mapping
{
    public static class PlayerInfoMapping
    {
        public static PlayerInfo MapToPlayerInfo(this Player p)
        {
            return new PlayerInfo
            {
                FirstName = p.FirstName,
                SecondName = p.SecondName,
                WebName = p.WebName,
                Position = p.elementType.TypeName,
                Team = p.team.TeamName,
                Status = p.Status,
                SquadNumber = p.SquadNumber,
                News = p.News,
                NewsAdded = p.NewsAdded,
                ChancePlayingNextRound = p.ChancePlayingNextRound,
                ChancePlayingThisRound = p .ChancePlayingThisRound,
            };
        }
    }
}