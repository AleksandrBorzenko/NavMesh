
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour, ITarget<Bot>
{
    private readonly int minDamage = 1;
    private readonly int maxDamage = 6;

    private readonly int minHealth = 10;
    private readonly int maxHealth = 20;

    private readonly int minVelocity = 2;
    private readonly int maxVelocity = 5;

    public readonly BotInfo botInfo = new BotInfo();

    public Bot targetSign => this;

    public Vector3 position => transform.position;

    private BotTargetSearcher botTargetSearcher = new BotTargetSearcher();

    void Start()
    {
        SetBotData();
        botTargetSearcher.GetNearestTarget(transform.position);
    }
    
    public void SetBotData()
    {
        botInfo.SetDamage(minDamage,maxDamage);
        botInfo.SetHealth(minHealth,maxHealth);
        botInfo.SetVelocity(minVelocity,maxVelocity);
    }

    public void SetMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }

    
}
