using UnityEngine;
/// <summary>
/// Contains methods that determine tbe material of bot
/// </summary>
public interface IBotMaterial
{
    Material GetRandom();
    Material GetMaterialByIndex(int index);
}
