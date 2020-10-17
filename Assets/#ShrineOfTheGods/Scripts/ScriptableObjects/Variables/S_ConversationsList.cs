using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New conversations list", menuName = "Shrine Of The Gods/Lists/Conversations")]
public class S_ConversationsList : S_AbstractList<S_Conversation>
{
    public int current = 0;
}
