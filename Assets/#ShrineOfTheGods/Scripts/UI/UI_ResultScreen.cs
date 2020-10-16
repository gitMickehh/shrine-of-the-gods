﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class UI_ResultScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    [Header("Animation")]
    public float fadeTime = 1f;

    [Header("References")]
    public List<S_God> gods;
    public S_Int numberOfDays;

    [Header("UI")]
    public TextMeshProUGUI upperTitle;
    public GameObject GodPanelHolder;
    public UI_GodPanel godPanel;

    [Space]
    public S_Conversation hintConversation;
    public GameObject HintObject;
    public TextMeshProUGUI hintText;

    [Space]
    public TextMeshProUGUI nextDayText;

    [Header("Buttons References")]
    public Button NextDayButton;
    public Button HintButton;
    public Button BackFromHintButton;

    //here
    private List<UI_GodPanel> godPanels;
    private List<string> hintSentences;
    private bool showingHint;
    private bool showingResults;

    private void Start()
    {
        InitScreen();
    }

    private void InitScreen()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        showingHint = false;

        hintText.text = NextHint();
        UpdateTexts();

        for (int i = 0; i < GodPanelHolder.transform.childCount; i++)
        {
            Destroy(GodPanelHolder.transform.GetChild(i).gameObject);
        }

        godPanels = new List<UI_GodPanel>();
        for (int i = 0; i < gods.Count; i++)
        {
            UI_GodPanel gpanel= Instantiate(godPanel,GodPanelHolder.transform).GetComponent<UI_GodPanel>();

            gpanel.Init(gods[i]);
            godPanels.Add(gpanel);
        }
    }

    private void Update()
    {
        if(showingResults)
        {
            if(Input.GetButtonDown("Interact"))
            {
                if (!showingHint)
                    NextDayButton.onClick.Invoke();
                else
                    BackFromHintButton.onClick.Invoke();
            }
            else if (Input.GetButtonDown("Eat"))
            {
                HintButton.onClick.Invoke();
            }

        }
    }

    private void UpdateTexts()
    {
        upperTitle.text = "Day " + numberOfDays.Value.ToString("00");
        nextDayText.text = "Day " + (numberOfDays.Value + 1).ToString("00");
    }

    private void RefreshGods()
    {
        foreach (UI_GodPanel gPanel in godPanels)
        {
            gPanel.RefreshStats();
        }
    }

    public void ShowResult()
    {
        showingResults = true;
        canvasGroup.DOFade(1, fadeTime);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        UpdateTexts();
        RefreshGods();
    }

    public void HideResults()
    {
        showingResults = false;
        
        canvasGroup.DOFade(2, fadeTime/2);
        canvasGroup.interactable = false;
        HideHint();
    }

    public void ShowHint()
    {
        if(!showingHint)
        {
           showingHint = true;
           HintObject.SetActive(true);
        }
        else
        {
            hintText.text = NextHint();
        }

    }

    private string NextHint()
    {
        if (hintSentences == null)
        {
            hintSentences = hintConversation.GetConversation().ToList();
        }
        else if (hintSentences.Count == 0)
        {
            hintSentences = hintConversation.GetConversation().ToList();
        }

        int r = Random.Range(0,hintSentences.Count);
        string sentence = hintSentences[r];
        hintSentences.RemoveAt(r);
        return sentence;
    }

    public void HideHint()
    {
        showingHint = false;
        HintObject.SetActive(false);
    }

}