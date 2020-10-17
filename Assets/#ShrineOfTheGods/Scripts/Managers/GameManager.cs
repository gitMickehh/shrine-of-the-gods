using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DaySchedule clockworkSchedule;

    [Header("Gods")]
    public S_GodsList gods;

    [Header("References")]
    public S_Int dayNumber;
    public Transform playerTransform;
    public Transform playerStartPosition;

    [Header("Randomness")]
    public S_Bool useCustomSeed;
    public S_Int customSeed;

    [Header("Game Start")]
    public UI_DayAnnouncement startAnnouncement;
    public UI_ResultScreen resultScreen;

    [Header("Transition")]
    public UI_Transition transition;

    private void Start()
    {
        dayNumber.Value = clockworkSchedule.startDay;

        if (useCustomSeed.Value)
        {
            Random.InitState(customSeed.Value);
        }

        startAnnouncement.StartTransition(dayNumber.Value, clockworkSchedule.GetDayMessage(dayNumber.Value), clockworkSchedule.GetDaySprite(dayNumber.Value));
    }

    private void OnDisable()
    {
        gods.ResetList();
    }

    public void StartDay()
    {
        //call here the events of the day and everything
        gods.ActivateGodEffects();
        clockworkSchedule.RaiseDayEvents(dayNumber.Value);
    }

    public void DayEnd()
    {
        resultScreen.gameObject.SetActive(true);
        resultScreen.ShowResult();
    }

    public void NextDay()
    {
        dayNumber.Value++;
        playerTransform.position = playerStartPosition.position;

        startAnnouncement.StartTransition(dayNumber.Value, clockworkSchedule.GetDayMessage(dayNumber.Value), clockworkSchedule.GetDaySprite(dayNumber.Value));
        resultScreen.HideResults();
        resultScreen.gameObject.SetActive(false);
    }

    public void PlayerDiedOfHunger()
    {
        resultScreen.gameObject.SetActive(true);
        resultScreen.PlayerDeath("Player died of hunger");
        //transition.LoadScene(2);
    }

}
