using UnityEngine;

public interface IBot
{
    int damage { get; }
    int health { get; }
    int velocity { get; }
    void SetDamage(int minDamage, int maxDamage);
    void SetHealth(int minHealth, int maxHealth);
    void SetVelocity(int minVelocity, int maxVelocity);
    void Attack();
    void SetMaterial(Material material);
}
