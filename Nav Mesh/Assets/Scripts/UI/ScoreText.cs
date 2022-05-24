using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void UpdateScoreText(int newScore)
    {
        scoreText.text = scoreTextBase + newScore;
    }
}
