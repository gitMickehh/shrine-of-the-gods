using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class UI_DayAnnouncement : MonoBehaviour
{
    private int dayNumber;

    [Header("UI")]
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI dayMessageText;
    public Image UI_DayImage;

    [Header("Header UI")]
    public TextMeshProUGUI topDisplayDayText;

    [Header("Animation")]
    public float animationTime = 2f;
    public float holdTime = 1f;

    [Header("Events")]
    public GameEvent OnAnnouncementFinish;

    public void StartTransition(int numberOfDay, string messageToSay, Sprite dayImage = null)
    {
        SetupScreen(numberOfDay, messageToSay, dayImage);
        Animate();
    }

    void SetupScreen(int numberOfDay, string messageToSay, Sprite dayImage)
    {
        dayNumber = numberOfDay;
        dayText.text = "Day " + numberOfDay.ToString("00");
        topDisplayDayText.text = dayText.text;
        dayMessageText.text = messageToSay;

        if (dayImage == null)
            UI_DayImage.gameObject.SetActive(false);
        else
        {
            UI_DayImage.gameObject.SetActive(true);
            UI_DayImage.sprite = dayImage;
        }
    }

    void Animate()
    {
        if(dayNumber != 1)
            canvasGroup.alpha = 0;

        canvasGroup.DOFade(1, animationTime).OnComplete(()=>StartCoroutine(HoldScreen()));
    }

    IEnumerator HoldScreen()
    {
        yield return new WaitForSeconds(holdTime);
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, animationTime).OnComplete(()=>OnAnnouncementFinish.Raise());
    }
    
}
