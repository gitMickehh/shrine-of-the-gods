using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("Gods")]
    public List<S_God> gods;

    [Header("Spawning Items")]
    public GameObject shrinePrefab;
    public GameObject vegetationPrefab;

    //spawned objects
    private List<GodShrine> shrines;
    private List<Vegetation> plants;

    private GodShrine SpawnShrine(Vector3 position, S_God god)
    {
        var shrineObj = Instantiate(shrinePrefab,position,Quaternion.identity);
        shrineObj.transform.SetParent(transform);

        var shrine = shrineObj.GetComponent<GodShrine>();
        shrine.TakeGod(god);
        return shrine;
    }

    private Vegetation SpawnVegetation(Vector3 position, S_Vegetation vegTemplate) 
    {
        var vegObj = Instantiate(vegetationPrefab, position, Quaternion.identity);
        vegObj.transform.SetParent(transform);

        var veg = vegObj.GetComponent<Vegetation>();

        return veg;
    }
}
