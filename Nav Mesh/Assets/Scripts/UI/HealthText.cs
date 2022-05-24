using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Health Text UI component of the bot
/// </summary>
public class HealthText : MonoBehaviour
{
    private readonly string healthTextBase = "Health: ";

    private IPlayer player;

    private Text healthText;


    void Awake()
    {
        player = GetComponentInParent<IPlayer>();
        healthText = GetComponent<Text>();
        player.healthChanged.AddListener(UpdateHealthText);
    }
    /// <summary>
    /// Updates the UI Text component of health after taking damage
    /// </summary>
    /// <param name="newHealth">New health number</param>
    private void UpdateHealthText(int newHealth)
    {
        healthText.text = healthTextBase + newHealth;
    }
}
