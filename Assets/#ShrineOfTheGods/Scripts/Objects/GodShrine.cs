using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodShrine : MonoBehaviour
{
    public S_God god;
    
    [Header("References")]
    public SpriteRenderer shrineImage;
    public Talker shrineTalker;
    public S_Inventory playerInventory;
    public Transform sacraficePosition;

    public void TakeGod(S_God newGod)
    {
        god = newGod;

        transform.name = god.name;
        shrineImage.sprite = god.godShrine;
        shrineTalker.conversationPiece = god.shrineConversation;
    }

    public void PayRespects()
    {

        if(playerInventory.Value != null)
        {
            int retValue = god.GiveItem(playerInventory.Value);

            playerInventory.Value.transform.position = sacraficePosition.position;
            playerInventory.Value.transform.SetParent(sacraficePosition);
            //negative impact on god
            //VFX and SFX
            playerInventory.Value.SacrificeItem(retValue);

            return;
        }

        Debug.Log("No items");
    }
}
