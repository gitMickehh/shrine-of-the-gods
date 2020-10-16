using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public S_Fire fireTemplate;
    [Space]
    public SpriteRenderer fireRenderer;
    public S_Inventory playerInventory;
    public GameObject particleEffect;

    private int firePower;
    private SpawnPoint mySpawnPoint;
    private bool fireAlive;

    public void SetupFire(S_Fire template, SpawnPoint spawnPoint = null)
    {
        fireTemplate = template;
        mySpawnPoint = spawnPoint;
        ReKindleFire();
    }

    public void BurnItem()
    {
        if(!playerInventory.IsEmpty())
        {
            if(firePower <= 0)
            {
                //can not burny anymore
                KillFire();
                return;
            }

            playerInventory.Value.Burn(particleEffect);
            firePower--;

        }
    }

    public bool IsAlive()
    {
        return fireAlive;
    }

    public void KillFireImmediatly(int fireLevel)
    {
        if (fireLevel >= firePower)
            KillFire();
    }

    public void KillFire()
    {
        fireAlive = false;
        fireRenderer.sprite = null;
        particleEffect.SetActive(false);
    }

    public void PowerupFire(S_Fire newTemplate)
    {
        fireTemplate = newTemplate;
        ReKindleFire();
    }

    public void ReKindleFire()
    {
        fireAlive = true;
        firePower = fireTemplate.firePower;
        fireRenderer.sprite = fireTemplate.fireSprite;

        particleEffect.SetActive(true);
    }
}
