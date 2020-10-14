using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vegetation : MonoBehaviour
{
    public S_Vegetation plantTemplate;
    public SpriteRenderer plantRenderer;
    private bool interactable;

    [ContextMenu("Refine Look")]
    public void RefineLook()
    {
        transform.name = plantTemplate.name;
        plantRenderer.sprite = plantTemplate.sprite;
        interactable = plantTemplate.interactable;
    }

    public void SetVegetation(S_Vegetation veg)
    {
        transform.name = veg.name;

        plantTemplate = veg;
        plantRenderer.sprite = plantTemplate.sprite;
        interactable = veg.interactable;
    }

}
