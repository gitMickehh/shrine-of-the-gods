using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ImageSlicedToValue : MonoBehaviour
{
    public bool reversed;

    public Image slicedImage;
    public S_Float value;
    public S_Float totalValue;
    private float totalValueFloat;

    public void StartTimer()
    {
        totalValueFloat = totalValue.Value;
    }

    private void Update()
    {
        if(!reversed)
            slicedImage.fillAmount = (totalValueFloat - value.Value)/ totalValueFloat;
        else
            slicedImage.fillAmount = value.Value / totalValueFloat;
    }
}
