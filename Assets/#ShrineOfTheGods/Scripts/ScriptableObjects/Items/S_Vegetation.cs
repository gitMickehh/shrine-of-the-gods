using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vegetation", menuName = "Shrine Of The Gods/Generation/Vegetation")]
public class S_Vegetation : S_GenerationElement
{
    public bool interactable;
    public Sprite sprite;
    public Sprite rottenSprite;

    public override GameObject SpawnInWorld()
    {
        var vegObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        var veg = vegObj.GetComponent<Vegetation>();
        veg.SetVegetation(this);

        return vegObj;
    }

    //public Vegetation SpawnInWorld(Transform spawnPoint)
    //{
    //    var vegObj = Instantiate(prefab, spawnPoint);
    //    vegObj.name = name;

    //    var veg = vegObj.GetComponent<Vegetation>();
    //    veg.SetVegetation(this);

    //    return veg;
    //}
}
