using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A line with bot name and score in leaderboard
/// </summary>
public class LeaderboardLine : MonoBehaviour
{
    public Bot bot { get; private set; }

    private Text botNameText;
    private Text botScoreText;

    /// <summary>
    /// Set a bot to leaderboard line
    /// </summary>
    /// <param name="bot">Bot with whom this line will be linked</param>
    public void SetBot(Bot bot)
    {
        this.bot = bot;
        this.bot.scoreChanged.AddListener(UpdateScoreText);
    }
    /// <summary>
    /// Initializing texts
    /// </summary>
    public void InitializeTexts()
    {
        botNameText = transform.GetChild(0).GetComponent<Text>();
        botScoreText = transform.GetChild(1).GetComponent<Text>();
    }
    /// <summary>
    /// Set the first parameters of bot
    /// </summary>
    public void SetInitialParameters()
    {
        botNameText.text = bot.botInfo.name;
        botScoreText.text = bot.botInfo.score.ToString();
    }
    /// <summary>
    /// Update the score text
    /// </summary>
    public void UpdateScoreText(int score)
    {
        botScoreText.text = score.ToString();
    }
    /// <summary>
    /// Method for creating lines
    /// </summary>
    /// <param name="bot">Bot who will be linked with this line</param>
    public void CreateNewLine(Bot bot)
    {
        InitializeTexts();
        SetBot(bot);
        SetInitialParameters();
    }
}
