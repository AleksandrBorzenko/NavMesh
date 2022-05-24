using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInfo : IBotInfo
{
    public int damage { get; private set; }

    public int health { get; private set; }

    public int velocity { get; private set; }

    public bool isAlive { get; set; } = true;

    public void DecreaseHealth(int amount)
    {
        health -= amount;
    }

    public void SetDamage(int minDamage, int maxDamage)
    {
        damage = Random.Range(minDamage, maxDamage);
    }

    public void SetHealth(int minHealth, int maxHealth)
    {
        health = Random.Range(minHealth, maxHealth);
    }

    public void SetHealthToZero()
    {
        health = 0;
    }

    public void SetVelocity(int minVelocity, int maxVelocity)
    {
        velocity = Random.Range(minVelocity, maxVelocity);
    }
}
