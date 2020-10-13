using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedDataConversationChanger : ConversationChanger
{
    [System.Serializable]
    public struct SavedDataKey
    {
        [Tooltip("The name of the key saved")] public string savedDataKey;
        public int keyThreshold;
        public bool useName;
        public int conversationNumber;
        public string conversationName;
    }   

    public List<SavedDataKey> keys;

    public override void CheckConversationForChange()
    {
        foreach (SavedDataKey key in keys)
        {
            if(PlayerPrefs.HasKey(key.savedDataKey))
            {
                if(PlayerPrefs.GetInt(key.savedDataKey) >= key.keyThreshold)
                {
                    if(!key.useName)
                        conversation.ChangeCurrentConversation(key.conversationNumber);
                    else
                        conversation.ChangeCurrentConversation(key.conversationName);

                    return;
                }
            }
        }

        conversation.ChangeCurrentConversation(fallbackConversation);

    }

    public void ChangeFallback(int newFallback)
    {
        fallbackConversation = newFallback;
    }
}
