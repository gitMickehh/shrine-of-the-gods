using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vegetation : MonoBehaviour, IInteractable
{
    public S_Vegetation plantTemplate;
    public SpriteRenderer plantRenderer;
    private bool interactable;

    public void SetVegetation(S_Vegetation veg)
    {
        transform.name = veg.name;

        plantTemplate = veg;
        plantRenderer.sprite = plantTemplate.sprite;
        interactable = veg.interactable;
    }

    public void IndicateInteractionEnd()
    {
        if (!interactable)
            return;
    }

    public void IndicateInteractionStart()
    {
        if (!interactable)
            return;
    }

    public void Interact()
    {
        if (!interactable)
            return;
    }
}
