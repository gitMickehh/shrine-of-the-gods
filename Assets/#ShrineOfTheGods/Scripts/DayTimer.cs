using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimer : MonoBehaviour
{
    bool running = false;
    public float neutralDayTime = 60f;
    [SerializeField][Range(0.8f,1.2f)]private float dayTimeMultiplier = 1f;

    private float currentDayTime;
    public S_Float currentTime;

    [Header("Events")]
    public GameEvent dayStart;
    public GameEvent dayEnd;

    private void Start()
    {
        StartTimer(1);
    }

    private void StartTimer(float multiplyer)
    {
        dayTimeMultiplier = Mathf.Clamp(multiplyer,0.8f,1.2f);
        currentDayTime = neutralDayTime * dayTimeMultiplier;
        currentTime.Value = 0;
        running = true;

        dayStart.Raise();
    }

    private void EndTimer()
    {
        currentTime.Value = 0;
        running = false;

        //fire an event so you can calculate the stuff and all
        dayEnd.Raise();
    }

    private void Update()
    {
        if(running)
        {
            currentTime.Value += Time.deltaTime;
            if(currentTime.Value >= currentDayTime)
            {
                EndTimer();
            }
        }
    }
}
