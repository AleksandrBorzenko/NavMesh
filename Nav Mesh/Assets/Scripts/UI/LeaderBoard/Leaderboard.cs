using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class that holds and manages all lines
/// </summary>
public class Leaderboard : MonoBehaviour,ILeaderboard
{
    private GameController gameController;

    private GameObject leaderboardLinePrefab;

    /// <summary>
    /// List of leaderboard lines
    /// </summary>
    public List<LeaderboardLine> lines { get; private set; }

    /// <summary>
    /// Add new line to leaderboard
    /// </summary>
    /// <param name="newLine">New leaderboard line</param>
    public void AddLine(Bot bot)
    {
        var newLine = Instantiate(leaderboardLinePrefab, transform);
        newLine.GetComponent<LeaderboardLine>().CreateNewLine(bot);
        lines.Add(newLine.GetComponent<LeaderboardLine>());
    }
    /// <summary>
    /// Remove line from leaderboard
    /// </summary>
    /// <param name="oldLine">Old leaderboard line</param>
    public void RemoveLine(Bot bot)
    {
        var lineToRemove = lines.FindIndex(leaderboardLine => leaderboardLine.bot == bot);
        lines.RemoveAt(lineToRemove);
        Destroy(transform.GetChild(lineToRemove).gameObject);
            
    }
    /// <summary>
    /// Initializing leaderboard in order to add start bots
    /// </summary>
    /// <param name="gameController">GameController component</param>
    public void InitializeLeaderboard(GameController gameController)
    {
        lines = new List<LeaderboardLine>();
        leaderboardLinePrefab = Resources.Load<GameObject>("Prefabs/LeaderLine");
        this.gameController = gameController;
        this.gameController.NewBotAdded.AddListener(AddLine);
        this.gameController.objectsPool.botAddedToObjectsPool.AddListener(RemoveLine);
    }

}
