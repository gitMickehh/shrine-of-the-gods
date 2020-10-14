using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Talker : MonoBehaviour, IInteractable
{
    public GameObject conversationIcon;
    public S_TextBubble textBubble;
    private int currentConversationNumber;
    public S_Conversation conversationPiece;

    private List<string> currentSentences;
    bool sentenceFinished;
    string currentSentence;

    [Header("Changer")]
    public ConversationChanger changer;

    [Header("Events")]
    public UnityEvent OnInteraction;
    public UnityEvent OnEndSentence;

    public UnityEvent OnEndInteraction;


    private void Start()
    {
        conversationIcon.SetActive(false);
        //currentConversationNumber = conversationPiece.currentConversation;
        sentenceFinished = true;
    }

    public void GiveConversation(S_Conversation conv)
    {
        conversationPiece = conv;
        currentConversationNumber = conversationPiece.currentConversation;
    }

    public void IndicateInteractionStart()
    {
        conversationIcon.SetActive(true);
    }

    public void IndicateInteractionEnd()
    {
        conversationIcon.SetActive(false);
        //textBubble.Value.WalkAway();
    }

    public void Interact()
    {
        CheckConversationChanged();
        Talk();
        OnInteraction.Invoke();
    }

    private void CheckConversationChanged()
    {
        if (changer != null)
        {
            changer.conversation = conversationPiece;
            changer.CheckConversationForChange();
        }
    }

    private void Talk()
    {
        if (currentConversationNumber == conversationPiece.currentConversation)
        {
            //nothing has changed
            if (currentSentences == null || currentSentences.Count == 0)
                currentSentences = conversationPiece.GetConversation(currentConversationNumber).ToList<string>();
        }
        else
        {
            //an event has happened that will change the bark set of the talker
            currentConversationNumber = conversationPiece.currentConversation;
            currentSentences = conversationPiece.GetConversation(currentConversationNumber).ToList<string>();
        }


        if (sentenceFinished)
        {
            if(conversationPiece.inOrder)
                currentSentence = PopNextSentence(currentSentences);
            else
                currentSentence = PopRandomSentence(currentSentences);
            
            textBubble.Value.Type(conversationIcon.transform.position, currentSentence, SentenceFinished);
            sentenceFinished = false;
        }
        else
        {
            textBubble.Value.SkiptToTheEndOfCurrentSentence(currentSentence);
            sentenceFinished = true;
        }
    }

    private void SentenceFinished()
    {
        sentenceFinished = true;
        OnEndSentence.Invoke();
    }

    private string PopNextSentence(List<string> sentences)
    {
        if (sentences.Count == 1)
        {
            string lastSentence = sentences[0];
            currentSentences = conversationPiece.GetConversation(currentConversationNumber).ToList<string>();

            return lastSentence;
        }

        string sentence = sentences[0];
        sentences.Remove(sentence);
        return sentence;
    }

    private string PopRandomSentence(List<string> sentences)
    {
        if (sentences.Count == 1)
        {
            string lastSentence = sentences[0];
            currentSentences = conversationPiece.GetConversation(currentConversationNumber).ToList<string>();
            
            return lastSentence;
        }

        int r = Random.Range(0, sentences.Count);
        string sentence = sentences[r];

        sentences.Remove(sentence);

        return sentence;
    }

}
