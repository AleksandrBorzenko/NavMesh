using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour, IBot
{
    private int _damage;
    private int _health;
    private int _velocity;

    public int damage => _damage;

    public int health => _health;

    public int velocity => _velocity;

    public Material material { set => throw new System.NotImplementedException(); }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void SetDamage()
    {
        throw new System.NotImplementedException();
    }

    public void SetHealth()
    {
        throw new System.NotImplementedException();
    }

    public void SetVelocity()
    {
        throw new System.NotImplementedException();
    }
}
