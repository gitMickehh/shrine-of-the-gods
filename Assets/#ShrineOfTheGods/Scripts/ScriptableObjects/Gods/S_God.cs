using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New god", menuName = "Shrine Of The Gods/god")]
public class S_God : ScriptableObject
{
    [TextArea]public string godDescription;

    public Sprite godImage;
    public Sprite godShrine;

    public S_Conversation shrineConversation;
    public S_Conversation godConversation;
}
