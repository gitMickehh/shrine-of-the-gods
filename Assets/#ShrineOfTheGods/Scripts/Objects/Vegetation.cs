using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vegetation : MonoBehaviour
{
    public S_Vegetation plantTemplate;
    public SpriteRenderer plantRenderer;
    public Collectable myCollectable;
    private bool interactable;

    [ContextMenu("Refine Look")]
    public void RefineLook()
    {
        transform.name = plantTemplate.name;
        plantRenderer.sprite = plantTemplate.sprite;
        interactable = plantTemplate.interactable;
        
        if(interactable)
            myCollectable.myElement = plantTemplate;
    }

    public void SetVegetation(S_Vegetation veg)
    {
        transform.name = veg.name;

        plantTemplate = veg;
        plantRenderer.sprite = plantTemplate.sprite;
        interactable = plantTemplate.interactable;

        if(interactable)
            myCollectable.myElement = plantTemplate;
    }

}
