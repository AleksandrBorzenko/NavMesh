using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInfo : IBotInfo
{
    private int _damage;
    private int _health;
    private int _velocity;


    public int damage => _damage;

    public int health => _health;

    public int velocity => _velocity;

    public void SetDamage(int minDamage, int maxDamage)
    {
        _damage = Random.Range(minDamage, maxDamage);
    }

    public void SetHealth(int minHealth, int maxHealth)
    {
        _health = Random.Range(minHealth, maxHealth);
    }

    public void SetVelocity(int minVelocity, int maxVelocity)
    {
        _velocity = Random.Range(minVelocity, maxVelocity);
    }
}
