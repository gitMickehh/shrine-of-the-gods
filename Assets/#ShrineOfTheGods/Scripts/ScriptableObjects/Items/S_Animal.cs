using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animal", menuName = "Shrine Of The Gods/Generation/Animal")]
public class S_Animal : S_GenerationElement
{

    public Sprite sprite;
    public bool interactable;

    public override GameObject SpawnInWorld(SpawnPoint spawnPoint)
    {
        var animalObj = Instantiate(prefab, spawnPoint.spawnPos);

        var anim = animalObj.GetComponentInChildren<Animal>();
        anim.InitAnimal(this, spawnPoint);

        return animalObj;
    }
}
