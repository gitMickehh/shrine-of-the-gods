using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GodShrine : MonoBehaviour
{
    public S_God god;
    
    [Header("References")]
    public SpriteRenderer shrineImage;
    public Talker shrineTalker;
    public S_Inventory playerInventory;
    public Transform sacraficePosition;

    private List<Collectable> sacrifices;

    public UnityEvent OnGiveSacrifice;

    public void TakeGod(S_God newGod)
    {
        god = newGod;

        transform.name = god.name;
        shrineImage.sprite = god.godShrine;
        shrineTalker.conversationPiece = god.shrineConversation;

        sacrifices = new List<Collectable>();
    }

    public void PayRespects()
    {

        if(playerInventory.Value != null)
        {

            OnGiveSacrifice.Invoke();

            sacrifices.Add(playerInventory.Value);
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

    public void ClearSacrifices()
    {
        if (sacrifices == null)
            return;

        foreach (Collectable item in sacrifices)
        {
            Destroy(item.gameObject);
        }
    }
}
