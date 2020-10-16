using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnPoint
{

    public Transform spawnPos;
    public bool occupied;
    public S_GenerationElement template;

    public void Occupy(bool toggle)
    {
        occupied = toggle;
    }
}
