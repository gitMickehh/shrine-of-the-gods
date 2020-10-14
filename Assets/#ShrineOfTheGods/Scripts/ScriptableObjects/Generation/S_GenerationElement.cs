using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_GenerationElement : ScriptableObject
{
    public GameObject prefab;
    [Range(1,4)]public int rarity = 1;

    public abstract GameObject SpawnInWorld(Transform spawnPoint);
}
