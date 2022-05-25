using System.Collections.Generic;

public interface ILeaderboard
{
    List<LeaderboardLine> lines { get; }
    void AddLine(Bot bot);
    void RemoveLine(Bot bot);
    void InitializeLeaderboard(GameController gameController);
}
