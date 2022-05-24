using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void UpdateHealthText(int newHealth)
    {
        healthText.text = healthTextBase + newHealth;
    }
}
