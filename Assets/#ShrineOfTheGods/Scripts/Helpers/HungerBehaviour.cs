using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HungerBehaviour : MonoBehaviour
{
    public bool active = false;
    public float ratePerSecond = 2f;

    [Header("Reference")]
    public S_Float maxHunger;
    public S_Float hungerValue;
    public S_Float hungerMultiplyer;

    [Header("Events")]
    public UnityEvent onDeath;

    //time
    private float runningTime;

    private void Start()
    {
        hungerValue.Value = maxHunger.Value;
        ResetVariables();
    }

    private void ResetVariables()
    {
        hungerMultiplyer.Value = Mathf.Clamp(hungerMultiplyer.Value, 0.8f, 2f);
        runningTime = 0;
    }

    public void Activate(bool toggle)
    {
        active = toggle;
        ResetVariables();
    }

    private void Update()
    {
        if(active)
        {
            runningTime += Time.deltaTime;
            if(runningTime>= 1)
            {
                AddHunger(-1 * hungerMultiplyer.Value);
                runningTime = 0;
            }
        }
    }

    private void AddHunger(float modificationValue)
    {
        hungerValue.Value += modificationValue;

        if(hungerValue.Value > maxHunger.Value)
        {
            hungerValue.Value = maxHunger.Value;
        }
        else if(hungerValue.Value <= 0)
        {
            //kill here
            onDeath.Invoke();
        }
    }
}
