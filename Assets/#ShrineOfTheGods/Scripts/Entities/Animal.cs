using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public S_Animal animalTemplate;
    public SpriteRenderer animalRendedrer;
    public Collectable myCollectable;
    private bool interactable;
    private SpawnPoint mySpawnPoint;

    [ContextMenu("Refine Look")]
    public void RefineLook()
    {
        transform.name = animalTemplate.name;
        animalRendedrer.sprite = animalTemplate.sprite;
        interactable = animalTemplate.interactable;

        if (interactable)
            myCollectable.myElement = animalTemplate;
    }

    public void InitAnimal(S_Animal anim, SpawnPoint spawnPoint = null)
    {
        transform.name = anim.name;

        animalTemplate = anim;
        animalRendedrer.sprite = animalTemplate.sprite;
        interactable = animalTemplate.interactable;

        mySpawnPoint = spawnPoint;

        if (interactable)
            myCollectable.myElement = animalTemplate;
    }

    public void AnimalLeft()
    {
        mySpawnPoint.occupied = false;
    }
}
