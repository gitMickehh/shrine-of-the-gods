using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodShrine : MonoBehaviour
{
    public S_God god;
    
    [Header("References")]
    public SpriteRenderer shrineImage;
    public Talker shrineTalker;

    public void TakeGod(S_God newGod)
    {
        god = newGod;

        transform.name = god.name;
        shrineImage.sprite = god.godShrine;
        shrineTalker.conversationPiece = god.shrineConversation;
    }

    public void PayRespects()
    {
        Debug.Log("Paying respects I see :)");
    }
}
