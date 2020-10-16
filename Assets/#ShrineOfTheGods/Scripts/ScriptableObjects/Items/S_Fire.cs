using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fire", menuName = "Shrine Of The Gods/Generation/Fire")]
public class S_Fire : S_GenerationElement
{
    public Sprite fireSprite;
    [Tooltip("The number of times to burn from this from before it runs out")]
    public int firePower = 2;

    public override GameObject SpawnInWorld(SpawnPoint spawnPoint)
    {
        var fireObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);

        var fir = fireObj.GetComponent<Fire>();
        fir.SetupFire(this, spawnPoint);

        return fireObj;
    }
}
