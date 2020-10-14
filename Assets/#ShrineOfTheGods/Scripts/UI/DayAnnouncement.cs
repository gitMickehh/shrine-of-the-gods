using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class DayAnnouncement : MonoBehaviour
{
    public int dayNumber;

    [Header("UI")]
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI dayTitleText;
    public Image dayImage;

    [Header("Animation")]
    public float animationTime = 2f;
    public float holdTime = 1f;

    [Header("Events")]
    public GameEvent OnAnnouncementFinish;

    private void Start()
    {
        StartTransition();
    }

    public void StartTransition()
    {
        Animate();
    }

    void Animate()
    {
        canvasGroup.alpha = 0;

        //Sequence s = DOTween.Sequence();

        //s.Join(canvasGroup.DOFade(1,animationTime));
        //can't find a delay
        //s.Append(canvasGroup.DOFade(0,animationTime));

        canvasGroup.DOFade(1, animationTime).OnComplete(()=>StartCoroutine(HoldScreen()));
    }

    IEnumerator HoldScreen()
    {
        yield return new WaitForSeconds(holdTime);
        canvasGroup.alpha = 1;
        canvasGroup.DOFade(0, animationTime).OnComplete(()=>OnAnnouncementFinish.Raise());
    }
    
}
