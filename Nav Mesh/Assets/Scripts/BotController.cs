
using UnityEngine;

public class BotController:MonoBehaviour
{
    private readonly int minDamage = 1;
    private readonly int maxDamage = 6;

    private readonly int minHealth = 10;
    private readonly int maxHealth = 20;

    private readonly int minVelocity = 2;
    private readonly int maxVelocity = 5;

    public readonly BotInfo botInfo = new BotInfo();
    private readonly BotMaterialStorage botMaterialStorage = new BotMaterialStorage();

    
    public void CreateBot()
    {
        botInfo.SetDamage(minDamage,maxDamage);
        botInfo.SetHealth(minHealth,maxHealth);
        botInfo.SetVelocity(minVelocity,maxVelocity);
        SetMaterial(botMaterialStorage.GetRandom());
    }

    void SetMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }
}
