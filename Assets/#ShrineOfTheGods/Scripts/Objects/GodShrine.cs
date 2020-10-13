using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodShrine : MonoBehaviour
{
    //the god it belongs to
    public S_God god;

    public SpriteRenderer shrineImage;

    [Header("Talker")]
    public Talker shrineTalker;
    private S_Conversation shrineConversation;

    private void Start()
    {
        shrineTalker.enabled = false;
        TakeGod(god);
    }

    public void TakeGod(S_God newGod)
    {
        god = newGod;

        transform.name = god.name;
        shrineImage.sprite = god.godShrine;
        shrineConversation = god.shrineConversation;

        shrineTalker.GiveConversation(god.shrineConversation);
    }

}
