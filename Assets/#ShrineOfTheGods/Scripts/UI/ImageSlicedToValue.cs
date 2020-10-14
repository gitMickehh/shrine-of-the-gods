using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSlicedToValue : MonoBehaviour
{
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
        slicedImage.fillAmount = (totalValueFloat - value.Value)/ totalValueFloat;
    }
}
