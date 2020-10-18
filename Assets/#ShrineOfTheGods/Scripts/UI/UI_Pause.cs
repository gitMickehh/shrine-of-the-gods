using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
{
    private bool pauseIsOn = false;

    public UI_Transition transition;

    public GameObject firstButtonSelected;
    private GameObject lastButtonSelected;

    public EventSystem uiEventSystem;
    public UnityEvent OnGamePause;
    public UnityEvent OnGameUpause;

    private void Update()
    {
        if(Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel"))
        {
            //toggle pause
            TogglePause();
        }
    }

    private void TogglePause()
    {
        pauseIsOn = !pauseIsOn;

        if (pauseIsOn)
        {
            lastButtonSelected = uiEventSystem.firstSelectedGameObject;
            firstButtonSelected.GetComponent<Button>().Select();
            uiEventSystem.SetSelectedGameObject(firstButtonSelected);

            OnGamePause.Invoke();
        }
        else 
        {
            uiEventSystem.SetSelectedGameObject(lastButtonSelected);
            OnGameUpause.Invoke();
        }
    }

    public void ContinueButton()
    {
        pauseIsOn = true;
        TogglePause();
    }

    public void HomeButton()
    {
        transition.LoadScene(0);
    }
}
