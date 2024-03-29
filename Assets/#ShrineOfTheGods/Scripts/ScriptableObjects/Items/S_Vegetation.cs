﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Vegetation", menuName = "Shrine Of The Gods/Generation/Vegetation")]
public class S_Vegetation : S_GenerationElement
{
    public bool interactable;
    public bool canRot = true;
    public Sprite sprite;
    public Sprite rottenSprite;

    public override GameObject SpawnInWorld(SpawnPoint spawnPoint)
    {
        var vegObj = Instantiate(prefab, spawnPoint.spawnPos);

        var veg = vegObj.GetComponent<Vegetation>();
        veg.SetVegetation(this, spawnPoint);

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
