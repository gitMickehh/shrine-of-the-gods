using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConversationChanger : MonoBehaviour
{
    public S_Conversation conversation;
    public int fallbackConversation;
    
    public abstract void CheckConversationForChange();
}
