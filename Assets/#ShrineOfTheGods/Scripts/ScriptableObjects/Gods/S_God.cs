using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class SacrificeSetting
{
    public S_GenerationElement itemSacrificed;
    public bool burnedBonus;
    public bool rottenBonus;
}

[CreateAssetMenu(fileName = "New god", menuName = "Shrine Of The Gods/god")]
public class S_God : ScriptableObject
{
    [TextArea]public string godDescription;

    [Header("Look")]
    public Sprite godImage;
    public Sprite godShrine;

    [Header("Conversations")]
    public S_Conversation shrineConversation;
    public S_Conversation godConversation;

    [Header("Stats")]
    public S_Int currentPower;

    [Header("Events")]
    public GameEvent godDefault;
    public GameEvent godStrong;
    public GameEvent godWeak;

    [Header("Sacrifice")]
    public List<SacrificeSetting> empoweringItems = new List<SacrificeSetting>();
    public List<SacrificeSetting> weakeningItems = new List<SacrificeSetting>();

    public int GiveItem(Collectable item)
    {
        SacrificeSetting found = empoweringItems.Find(x => x.itemSacrificed.name == item.myElement.name);
        if (found != null)
        {
            if (item.burned == found.burnedBonus)
                currentPower.Value += 2;
            else
                currentPower.Value++;

            if (item.rotten == found.rottenBonus)
                currentPower.Value += 2;
            else
                currentPower.Value++;
            
            return 1;
        }

        found = empoweringItems.Find(x => x.itemSacrificed.name == item.myElement.name);
        if (found != null)
        {
            if (item.burned == found.burnedBonus)
                currentPower.Value -= 2;
            else
                currentPower.Value--;

            if (item.rotten == found.rottenBonus)
                currentPower.Value -= 2;
            else
                currentPower.Value--;
            
            return -1;
        }

        //not found
        return 0;
    }

}
