using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Name text UI component of bot
/// </summary>
public class BotName : MonoBehaviour
{
    private Bot bot;

    private Text nameText;

    void Start()
    {
        nameText = GetComponent<Text>();
        bot = GetComponentInParent<Bot>();
        nameText.text = bot.botInfo.name;
    }

   
}
