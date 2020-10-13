using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextBubble : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public GameObject bubble;

    [Header("Settings")]
    [Tooltip("character")] [Range(15,45.0f)]public float typingSpeed = 20.0f;
    public float timeBeforeDisappearing = 5.0f;

    //typing freedom
    Coroutine currentCoroutine;
    Coroutine waitingToClose;

    [Header("Global Reference")]
    public S_TextBubble myReferece;

    private void OnEnable() {
        myReferece.Value = this;
    }
    private void OnDisable() {
        myReferece.Value = null;
    }

    private void Start() 
    {
        HideBubble();
    }

    public void WalkAway()
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        if(waitingToClose != null)
        {
            StopCoroutine(waitingToClose);
        }

        Countdown(timeBeforeDisappearing/2.0f);
    }

    public void Type(Vector3 bubblePosition,string sentence, Action OnSentenceFinish)
    {
        bubble.SetActive(true);
        bubble.transform.position = bubblePosition;

        myText.text = "";
            
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            
            if(waitingToClose != null)
                StopCoroutine(waitingToClose);
        }
            
        currentCoroutine = StartCoroutine(Typing(sentence, OnSentenceFinish));
    }

    private IEnumerator Typing(string sentence, Action OnSentenceFinish)
    {
        var typingSpeedInSeconds = 1.0f / typingSpeed;

        foreach (char letter in sentence)
        {
            myText.text += letter;
            yield return new WaitForSeconds(typingSpeedInSeconds);
        }

        Countdown(timeBeforeDisappearing);
        OnSentenceFinish.Invoke();
    }

    private void TypeSentenceNow(string sentence)
    {
        myText.text = sentence;

        Countdown(timeBeforeDisappearing);
    }

    private void Countdown(float timeToClose)
    {
        if(waitingToClose != null)
            StopCoroutine(waitingToClose);

        waitingToClose = StartCoroutine(CountdownToHide(timeToClose));
    }

    public void SkiptToTheEndOfCurrentSentence(string sentence)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            TypeSentenceNow(sentence);
        }
    }

    private IEnumerator CountdownToHide(float timeToClose)
    {
        yield return new WaitForSeconds(timeToClose);
        HideBubble();
    }

    private void HideBubble()
    {
        myText.text = "";
        bubble.SetActive(false);
    }

}
