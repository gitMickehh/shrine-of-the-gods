using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTimer : MonoBehaviour
{
    bool running = false;
    public float neutralDayTime = 60f;
    public S_Float maxDayTime;
    [SerializeField][Range(0.8f,1.2f)]private float dayTimeMultiplier = 1f;

    private float currentDayTime;
    public S_Float currentTime;

    [Header("Variables")]
    public S_Float timeMultiplyer;

    [Header("Events")]
    public GameEvent dayEnd;

    private void Start()
    {
        ResetValues();
    }

    public void StartTimer()
    {
        dayTimeMultiplier = Mathf.Clamp(timeMultiplyer.Value, 0.8f,1.2f);
        currentDayTime = neutralDayTime * dayTimeMultiplier;
        maxDayTime.Value = currentDayTime;

        currentTime.Value = 0;
        running = true;

        //dayStart.Raise();
    }

    private void ResetValues()
    {
        currentTime.Value = 0;
        running = false;
    }

    private void EndTimer()
    {
        ResetValues();

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

    public void PauseTimer(bool pauseOn)
    {
        running = !pauseOn;
    }
}
