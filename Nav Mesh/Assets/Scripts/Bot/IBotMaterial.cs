using UnityEngine;

public interface IBotMaterial
{
    Material GetRandom();
    Material GetMaterialByIndex(int index);
}
