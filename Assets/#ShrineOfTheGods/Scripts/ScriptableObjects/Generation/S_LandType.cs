using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LandType", menuName = "Shrine Of The Gods/Generation/Land Type")]
public class S_LandType : ScriptableObject
{
    [Range(1,4)]public int rarity = 1;
    public Color landColor;
    public List<S_GenerationElement> spawningTemplates;


}
