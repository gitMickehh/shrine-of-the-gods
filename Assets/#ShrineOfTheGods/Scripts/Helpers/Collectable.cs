using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public S_Inventory playerInventory;
    public bool collected = false;
    public TargetFollower follower;

    public void CollectItem()
    {
        if (playerInventory.Value != null)
            playerInventory.Value.DropItem();

        playerInventory.Value = this;
        collected = true;
        follower.enabled = true;
        follower.StartFollowing(playerInventory.player.transform);
    }

    public void DropItem()
    {
        playerInventory.Value = null;
        collected = false;
        follower.StopFollowing();
        follower.enabled = false;
    }
}
