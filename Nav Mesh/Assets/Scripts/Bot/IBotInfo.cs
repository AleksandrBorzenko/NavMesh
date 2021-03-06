using UnityEngine;
/// <summary>
/// Determines the info will be set to the bot
/// </summary>
public interface IBotInfo
{
    string name { get; set; }
    int damage { get; }
    int health { get; }
    int velocity { get; }
    int score { get; }
    bool isAlive { get; set; }
    void SetDamage(int minDamage, int maxDamage);
    void SetHealth(int minHealth, int maxHealth);
    void SetVelocity(int minVelocity, int maxVelocity);
    void DecreaseHealth(int amount);
    void SetHealthToZero();
    void IncreaseScore();
    void IncreaseDamage(int amount);
    void SetScoreToZero();
    void SetName(string name);
}
