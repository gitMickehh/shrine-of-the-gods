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
        if(interactionIndicator != null)
            interactionIndicator.SetActive(false);
    }

    public void IndicateInteractionStart()
    {
        if(interactionIndicator != null)
            interactionIndicator.SetActive(true);
    }

    public void IndicateInteractionEnd()
    {
        if(interactionIndicator != null)
            interactionIndicator.SetActive(false);
    }


    public void Interact()
    {
        onInteract.Invoke();
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }
}
