using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionBehaviour : MonoBehaviour, IInteractable
{
    public GameObject interactionIndicator;

    public UnityEvent onInteract;

    private void Start()
    {
        interactionIndicator.SetActive(false);
    }

    public void IndicateInteractionStart()
    {
        interactionIndicator.SetActive(true);
    }

    public void IndicateInteractionEnd()
    {
        interactionIndicator.SetActive(false);
    }


    public void Interact()
    {
        onInteract.Invoke();
    }
}
