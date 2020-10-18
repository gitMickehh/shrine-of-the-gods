using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ResultScreen : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public EventSystem uiEventSystem;

    [Header("Animation")]
    public float fadeTime = 1f;

    [Header("References")]
    public S_GodsList godsList;
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

    [Header("Scene Transition")]
    public UI_Transition transition;

    //here
    private List<UI_GodPanel> godPanels;
    private List<string> hintSentences;
    private bool showingHint;
    private bool showingResults;


    private float currentTime;

    private void Start()
    {
        InitScreen();
    }

    private void OnEnable()
    {
        currentTime = 0;
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
        for (int i = 0; i < godsList.items.Count; i++)
        {
            UI_GodPanel gpanel= Instantiate(godPanel,GodPanelHolder.transform).GetComponent<UI_GodPanel>();

            gpanel.Init(godsList.items[i]);
            godPanels.Add(gpanel);
        }
    }

    private void Update()
    {
        if(showingResults)
        {
            //counting 2 seconds to make sure player doesn't interact before the screen is shown
            currentTime+= Time.deltaTime;
            if (currentTime <= 2f)
                return;

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
        godsList.CalculateGap();

        foreach (UI_GodPanel gPanel in godPanels)
        {
            gPanel.RefreshStats();
        }

        int leadDifference = godsList.GetMaxDifference();
        if(leadDifference >= godsList.leadOverpowerThreshold)
        {
            PlayerDeath(godsList.GetLeadGod() + " overpowerd the other gods.");
        }
    }

    public void ShowResult()
    {
        showingResults = true;
        canvasGroup.DOFade(1, fadeTime);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        uiEventSystem.firstSelectedGameObject = NextDayButton.gameObject;

        UpdateTexts();
        RefreshGods();
    }

    public void PlayerDeath(string messageTop)
    {
        ShowResult();
        upperTitle.text = messageTop;
        nextDayText.text = "Restart";

        NextDayButton.onClick.RemoveAllListeners();
        NextDayButton.onClick.AddListener(()=> transition.LoadScene(1));
    }

    public void HideResults()
    {
        showingResults = false;
        
        canvasGroup.DOFade(2, fadeTime/2);
        canvasGroup.interactable = false;

        HideHint();
        uiEventSystem.firstSelectedGameObject = null;
    }

    public void ShowHint()
    {
        if(!showingHint)
        {
           uiEventSystem.firstSelectedGameObject = BackFromHintButton.gameObject;

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
        showingResults = true;
        uiEventSystem.firstSelectedGameObject = NextDayButton.gameObject;

        HintObject.SetActive(false);
    }

}
