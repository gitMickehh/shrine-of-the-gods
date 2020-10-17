using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI_GodPanel : MonoBehaviour
{
    public S_God god;
    public S_GodsList godsList;

    [Header("UI")]
    public Image godImage;
    public TextMeshProUGUI godName;
    public TextMeshProUGUI godPowerText;

    [Header("Colors")]
    public Color strongColor = Color.green;
    public Color weakColor = Color.red;

    [Header("Animation")]
    public float animationTime = 0.3f;
    public Ease animationEasing = Ease.InBounce;

    public void Init(S_God givenGod)
    {
        god = givenGod;
        transform.name = god.name + "_Frame";
        RefreshStats();
    }

    public void RefreshStats() 
    {
        godName.text = god.name;
        godImage.sprite = god.godImage;
        godPowerText.text = god.currentPower.Value.ToString();
        
        if(god.currentPower.Value == 0)
        {
            godPowerText.color = Color.white;
        }
        else if(god.currentPower.Value > 0)
        {
            godPowerText.color = strongColor;
        }
        else
        {
            godPowerText.color = weakColor;
        }

        int ranking = godsList.GetGodRanking(god);

        float newScale = (godsList.items.Count - ranking)/(float)godsList.items.Count;
        //Debug.Log("god: " + god.name + ", rank: " + ranking + ", new scale: "+ newScale);
        newScale = Mathf.Clamp(newScale,0.5f,1.0f);

        godImage.rectTransform.DOScale(newScale, animationTime).SetEase(animationEasing);
    }
}
