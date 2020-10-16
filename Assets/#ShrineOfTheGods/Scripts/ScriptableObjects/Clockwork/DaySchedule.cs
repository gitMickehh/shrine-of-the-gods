using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[CreateAssetMenu(fileName = "New Day Schedule", menuName = "Shrine Of The Gods/Day Schedule")]
public class DaySchedule : ScriptableObject
{
    public int startDay = 1;

    [Tooltip("Message of the day if the day is not here or doesn't have a custom message")]
    public string fallbackMessage;
    public Sprite fallbackSprite;

    public List<SOG_Day> schedule = new List<SOG_Day>();

    public string GetDayMessage(int dayNumber)
    {
        SOG_Day day = schedule.Find(x => x.dayNumber == dayNumber);
        
        if (day == null)
            return fallbackMessage;
        else if (string.IsNullOrEmpty(day.name))
            return fallbackMessage;
        else
            return day.name;
    }

    public Sprite GetDaySprite(int dayNumber)
    {
        SOG_Day day = schedule.Find(x => x.dayNumber == dayNumber);

        if (day == null)
            return fallbackSprite;
        else if (day.dayCustomSprite == null)
            return fallbackSprite;
        else
            return day.dayCustomSprite;
    }

    [ContextMenu("Order List")]
    public void OrderSchedule()
    {
        schedule = schedule.OrderBy(x=> x.dayNumber).ToList();
    }

}
