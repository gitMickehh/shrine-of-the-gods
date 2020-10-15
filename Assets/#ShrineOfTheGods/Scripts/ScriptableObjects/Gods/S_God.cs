using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    [Header("Sacrifice")]
    public List<S_GenerationElement> empoweringItems = new List<S_GenerationElement>();
    public List<S_GenerationElement> weakeningItems = new List<S_GenerationElement>();

    public int GiveItem(Collectable item)
    {
        S_GenerationElement found = empoweringItems.Find(x => x.name == item.myElement.name);
        if (found != null)
        {
            currentPower.Value++;
            return 1;
        }

        found = empoweringItems.Find(x => x.name == item.myElement.name);
        if (found != null)
        {
            currentPower.Value++;
            return -1;
        }

        //not found
        return 0;
    }
}
