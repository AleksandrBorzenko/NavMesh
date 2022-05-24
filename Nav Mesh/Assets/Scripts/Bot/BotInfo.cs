using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Contains the info of the bot
/// </summary>
public class BotInfo : IBotInfo
{
    public int damage { get; private set; }

    public int health { get; private set; }

    public int velocity { get; private set; }

    public bool isAlive { get; set; } = true;

    public int score { get; private set; }
    /// <summary>
    /// Decreases the health of the bot from the amount of taken damage
    /// </summary>
    /// <param name="amount">Amount of damage</param>
    public void DecreaseHealth(int amount)
    {
        health -= amount;
    }
    /// <summary>
    /// Increases the score of the bot after destroying an opponent
    /// </summary>
    public void IncreaseScore()
    {
        score++;
    }
    /// <summary>
    /// Set damage amount to the bot by random
    /// </summary>
    /// <param name="minDamage">Minimum damage</param>
    /// <param name="maxDamage">Maximum damage</param>
    public void SetDamage(int minDamage, int maxDamage)
    {
        damage = Random.Range(minDamage, maxDamage);
    }
    /// <summary>
    /// Set health amount to the bot by random
    /// </summary>
    /// <param name="minHealth">Minimum health</param>
    /// <param name="maxHealth">Maximum health</param>
    public void SetHealth(int minHealth, int maxHealth)
    {
        health = Random.Range(minHealth, maxHealth);
    }
    /// <summary>
    /// Method to set health to zero
    /// </summary>
    public void SetHealthToZero()
    {
        health = 0;
    }
    /// <summary>
    /// Set velocity which will be used by Nav Mesh Agent
    /// </summary>
    /// <param name="minVelocity">Minimum velocity</param>
    /// <param name="maxVelocity">Maximum velocity</param>
    public void SetVelocity(int minVelocity, int maxVelocity)
    {
        velocity = Random.Range(minVelocity, maxVelocity);
    }
}
