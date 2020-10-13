using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LandType { 
    Desert,
    Grass,
    Village
}

public class PieceOfLand : MonoBehaviour
{
    public LandType land;

    public GameObject horizontalWall;
    public GameObject verticalWall;

    public void GenerateLand()
    {

    }
}
