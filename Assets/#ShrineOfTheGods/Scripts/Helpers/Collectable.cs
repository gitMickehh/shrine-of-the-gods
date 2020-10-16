using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public S_GenerationElement myElement;
    public S_Inventory playerInventory;
    public bool collected = false;
    public TargetFollower follower;
    public Rigidbody2D rb;

    [Header("Event")]
    public UnityEvent OnCollect;
    public UnityEvent OnSacrafice;
    public UnityEvent OnBurn;

    private GameObject fireObject;
    [HideInInspector] public bool burned;

    public void CollectItem()
    {
        if (playerInventory.Value != null)
            playerInventory.Value.DropItem();

        playerInventory.Value = this;
        collected = true;
        follower.enabled = true;
        follower.StartFollowing(playerInventory.player.transform);

        rb.simulated = false;

        OnCollect.Invoke();
    }

    public void SacrificeItem(int sacrificeResult)
    {
        playerInventory.Value = null;
        collected = false;
        follower.StopFollowing();
        follower.enabled = false;

        OnSacrafice.Invoke();
    }

    public void DropItem()
    {
        playerInventory.Value = null;
        collected = false;
        follower.StopFollowing();
        follower.enabled = false;

        rb.simulated = true;
    }

    public void Burn(GameObject particleVFX)
    {
        if (burned)
            return;

        OnBurn.Invoke();
        burned = true;
        fireObject = Instantiate(particleVFX, transform);
    }
}
