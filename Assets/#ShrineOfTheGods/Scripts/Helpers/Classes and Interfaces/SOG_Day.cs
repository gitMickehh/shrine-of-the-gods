using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SOG_Day
{
    public string name;
    public int dayNumber;
    public int customDayMultiplyer = 1;
    public Sprite dayCustomSprite;
    public List<GameEvent> customEvents = new List<GameEvent>();

    public void RaiseDayEvents()
    {
        if (customEvents.Count == 0)
            return;

        foreach (GameEvent gameEvent in customEvents)
        {
            gameEvent.Raise();
        }
    }

}
