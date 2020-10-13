using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class S_GenerationElement : ScriptableObject
{
    public GameObject prefab;

    public abstract GameObject SpawnInWorld(Transform spawnPoint);
}
