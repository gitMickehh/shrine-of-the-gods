using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vegetation : MonoBehaviour
{
    public S_Vegetation plantTemplate;
    public SpriteRenderer plantRenderer;
    public Collectable myCollectable;
    private bool interactable;
    private SpawnPoint myPoint;

    [ContextMenu("Refine Look")]
    public void RefineLook()
    {
        transform.name = plantTemplate.name;
        plantRenderer.sprite = plantTemplate.sprite;
        interactable = plantTemplate.interactable;
        
        if(interactable)
            myCollectable.myElement = plantTemplate;
    }

    public void SetVegetation(S_Vegetation veg, SpawnPoint spawnPoint = null)
    {
        transform.name = veg.name;

        plantTemplate = veg;
        plantRenderer.sprite = plantTemplate.sprite;
        interactable = plantTemplate.interactable;

        myPoint = spawnPoint;

        if (interactable)
            myCollectable.myElement = plantTemplate;
    }

    public void VegetationPicked()
    {
        myPoint.occupied = false;
    }

}
