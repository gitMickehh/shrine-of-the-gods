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
    public UnityEvent OnRot;

    [Header("FIRE")]
    private GameObject fireObject;
    [HideInInspector] public bool burned;

    [Header("ROT")]
    public GameObject rotVFX;
    private GameObject rotObject;
    [HideInInspector] public bool rotten;

    public void CollectItem()
    {
        if (playerInventory.Value != null)
            playerInventory.Value.DropItem();

        playerInventory.Value = this;
        collected = true;
        follower.enabled = true;
        follower.StartFollowing(playerInventory.player.transform);

        //rb.simulated = true;

        //i make this false because if i keep it simulated, it will be the closest thing to the player
        //to interact with :/
        rb.simulated = false;

        OnCollect.Invoke();
    }

    public void SacrificeItem(int sacrificeResult)
    {
        playerInventory.Value = null;
        collected = false;
        follower.StopFollowing();
        follower.enabled = false;
        rb.simulated = false;

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

    public void EatItem()
    {
        Destroy(gameObject);
    }

    public bool Burn(GameObject particleVFX)
    {
        if (burned)
            return false;

        OnBurn.Invoke();
        burned = true;
        fireObject = Instantiate(particleVFX, transform);
        return true;
    }

    public bool Rot(GameObject particleVFX)
    {
            if (rotten)
                return false;

            OnRot.Invoke();
            rotten = true;
            rotObject = Instantiate(particleVFX, transform);
            return true;
    }

    public void Rot()
    {
            if (rotten)
                return;

            OnRot.Invoke();
            rotten = true;
            rotObject = Instantiate(rotVFX, transform);
    }

    public void StopRot()
    {
            if (!rotten)
                return;

            rotten = false;
            Destroy(rotObject);
    }

}
