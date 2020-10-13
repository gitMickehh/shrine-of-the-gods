using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Converastion", menuName = "Basic/Conversation/Conversation")]
public class S_Conversation : ScriptableObject
{

    public int currentConversation = 0;
    public bool loopConverastion = true;
    
    [System.Serializable]public struct Conversation
    {
        public string name; //just for the writer's reference
        public List<string> sentences;
    }

    // [System.Serializable]public struct InventoryConversation
    // {

    // }

    [Space]
    
    public string CharacterName;

    public List<Conversation> conversations = new List<Conversation>();


    public void ChangeCurrentConversation(int covnersation)
    {
        currentConversation = covnersation;
    }

    public void ChangeCurrentConversation(string covnersationTitle)
    {
        int num = conversations.FindIndex(x=> x.name == covnersationTitle);
        currentConversation = num;
    }

    public List<string> GetConversation()
    {
        return conversations[currentConversation].sentences;
    }

    public List<string> GetConversation(int conversationNumber)
    {
        if(conversationNumber >= conversations.Count)
        {
            return conversations[0].sentences;
        }

        return conversations[conversationNumber].sentences;
    }

}
