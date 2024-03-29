﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Shrine Of The Gods/Inventory")]
public class S_Inventory : S_RuntimeObject<Collectable>
{
    public PlayerControl player;

    public bool IsEmpty()
    {
        return (Value == null);
    }
}
