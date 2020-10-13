using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{

    void IndicateInteractionStart();
    void IndicateInteractionEnd();
    void Interact();
}

