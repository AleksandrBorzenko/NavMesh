using UnityEngine;

public interface IBot
{
    int damage { get; }
    int health { get; }
    int velocity { get; }
    Material material { get; set; }
    void SetDamage();
    void SetHealth();
    void SetVelocity();
    void Attack();
}
