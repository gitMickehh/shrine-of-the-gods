using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DaySchedule clockworkSchedule;

    [Header("References")]
    public S_Int dayNumber;

    [Header("Randomness")]
    public bool useCustomSeed;
    public int customSeed;

    [Header("Game Start")]
    public UI_DayAnnouncement startAnnouncement;
    public UI_ResultScreen resultScreen;

    private void Start()
    {
        dayNumber.Value = clockworkSchedule.startDay;

        if (useCustomSeed)
        {
            Random.InitState(customSeed);
        }

        startAnnouncement.StartTransition(dayNumber.Value, clockworkSchedule.GetDayMessage(dayNumber.Value), clockworkSchedule.GetDaySprite(dayNumber.Value));
    }

    public void DayEnd()
    {
        resultScreen.gameObject.SetActive(true);
        resultScreen.ShowResult();
    }

    public void NextDay()
    {
        dayNumber.Value++;

        //call here the events of the day and everything
        //events also that depend on which god is ahead

        startAnnouncement.StartTransition(dayNumber.Value, clockworkSchedule.GetDayMessage(dayNumber.Value), clockworkSchedule.GetDaySprite(dayNumber.Value));
    }

}
