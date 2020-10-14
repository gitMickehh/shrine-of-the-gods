using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControllerDetector : MonoBehaviour
{
    bool controller;

    public UnityEvent OnController;
    public UnityEvent OnKeyboard;

    private void Start() 
    {
        controller = CheckForConroller();
        
        if(controller)
            OnController.Invoke();
        else
            OnKeyboard.Invoke();
    }

    private void Update() 
    {
        if(CheckForConroller() != controller)
        {
            controller = !controller;
        
            if(controller)
                OnController.Invoke();
            else
                OnKeyboard.Invoke();
        }
    }

    
    public bool CheckForConroller()
    {
        if (Input.GetJoystickNames().Length == 0)
        {
            //keyboard
            return false;
        }
        else
        {
            //controller
            return true;
        }

    }
}
