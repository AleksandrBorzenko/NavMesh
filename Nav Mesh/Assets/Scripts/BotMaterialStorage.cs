using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMaterialStorage:IBotMaterial
{
    private List<Material> materials = new List<Material>(4);

    public void AddMaterials()
    {
        for (int i = 0; i < materials.Capacity; i++)
        {
            materials.Add(Resources.Load<Material>("Materials/BotMaterial" + i.ToString()));
        }
    }

    public Material GetRandom()
    {
        return materials[Random.Range(0, materials.Capacity)];
    }
}
