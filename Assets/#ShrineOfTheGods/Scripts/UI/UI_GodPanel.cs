using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GodPanel : MonoBehaviour
{
    public S_God god;

    [Header("UI")]
    public Image godImage;
    public TextMeshProUGUI godPowerText;

    [Header("Colors")]
    public Color strongColor = Color.green;
    public Color weakColor = Color.red;

    public void Init(S_God givenGod)
    {
        god = givenGod;

        RefreshStats();
    }

    public void RefreshStats() 
    {
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
    }
}
