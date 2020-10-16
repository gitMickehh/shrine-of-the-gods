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

    public void SetupFire(S_Fire template)
    {
        fireTemplate = template;
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

    public void KillFire()
    {
        particleEffect.SetActive(false);
    }

    public void ReKindleFire()
    {
        firePower = fireTemplate.firePower;
        fireRenderer.sprite = fireTemplate.fireSprite;

        particleEffect.SetActive(true);
    }
}
