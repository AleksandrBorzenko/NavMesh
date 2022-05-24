using UnityEngine;

public interface IBotInfo
{
    int damage { get; }
    int health { get; }
    int velocity { get; }
    bool isAlive { get; set; }
    void SetDamage(int minDamage, int maxDamage);
    void SetHealth(int minHealth, int maxHealth);
    void SetVelocity(int minVelocity, int maxVelocity);
    void DecreaseHealth(int amount);
    void SetHealthToZero();
}
