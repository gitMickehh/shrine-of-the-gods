using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectTrigger2D : MonoBehaviour
{
    [Tooltip("If the string is empty, anything will trigger this")]public string triggerTagged;

    public UnityEvent enter;
    public UnityEvent exit;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(string.IsNullOrEmpty(triggerTagged))
        {
            //fire here for anything
            enter.Invoke();
        }
        else if(other.CompareTag(triggerTagged))
        {
            //fire here
            enter.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
      if(string.IsNullOrEmpty(triggerTagged))
        {
            //fire here for anything
            exit.Invoke();
        }
        else if(other.CompareTag(triggerTagged))
        {
            //fire here
            exit.Invoke();
        }
    }
}
