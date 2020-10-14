using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LandType", menuName = "Shrine Of The Gods/Generation/Land Type")]
public class S_LandType : ScriptableObject
{
    [Range(1,4)]public int rarity = 1;
    public Color landColor;
    public List<S_GenerationElement> spawningTemplates;

    public List<S_GenerationElement> GetTemplatesBasedOnRarity()
    {
        List<S_GenerationElement> output = new List<S_GenerationElement>();

        for (int i = 0; i < spawningTemplates.Count; i++)
        {
            for (int j = 0; j < spawningTemplates[i].rarity; j++)
            {
                output.Add(spawningTemplates[i]);
            }
        }

        return output;
    }

}
