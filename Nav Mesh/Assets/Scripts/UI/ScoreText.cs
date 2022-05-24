using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Holds and controls the Score Text UI element of the bot
/// </summary>
public class ScoreText : MonoBehaviour
{
    private readonly string scoreTextBase = "Score: ";

    private IPlayer player;

    private Text scoreText;

    void Awake()
    {
        player = GetComponentInParent<IPlayer>();
        scoreText = GetComponent<Text>();
    }

    void Start()
    {
        player.scoreChanged.AddListener(UpdateScoreText);
    }
    /// <summary>
    /// Updates the Text UI component after destroying an opponent
    /// </summary>
    /// <param name="newScore">The new score of bot</param>
    private void UpdateScoreText(int newScore)
    {
        scoreText.text = scoreTextBase + newScore;
    }
}
