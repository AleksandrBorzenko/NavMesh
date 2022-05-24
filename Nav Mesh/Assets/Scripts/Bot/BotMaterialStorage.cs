using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
/// <summary>
/// Storage for the materials to the bot MeshRenderer
/// </summary>
public class BotMaterialStorage:IBotMaterial
{
    private List<Material> materials = new List<Material>(4);
    /// <summary>
    /// Loading materials from resources
    /// </summary>
    public void AddMaterials()
    {
        for (int i = 0; i < materials.Capacity; i++)
        {
            materials.Add(Resources.Load<Material>("Materials/BotMaterial" + (i+1).ToString()));
        }
    }
    /// <summary>
    /// Return material from the storage by its index. If index >= materials.Capacity the random material will be returned
    /// </summary>
    /// <param name="index">Index in storage</param>
    /// <returns>Material</returns>
    public Material GetMaterialByIndex(int index)
    {
        if(index<materials.Capacity)
            return materials[index];
        else
        {
            return GetRandom();
        }
    }
    /// <summary>
    /// Just get random material from storage
    /// </summary>
    /// <returns>Random material</returns>
    public Material GetRandom()
    {
        return materials[Random.Range(0, materials.Capacity)];
    }
}
