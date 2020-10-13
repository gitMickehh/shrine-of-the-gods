using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public struct SpawnPoint {
    public Transform spawnPos;
    public bool occupied;
}

public class WorldGenerator : MonoBehaviour
{
    [Header("Gods")]
    public List<S_God> gods;

    [Header("Land  Types")]
    public List<S_LandType> landTypes;

    [Header("Land Templates")]
    public List<GameObject> landTemplates;

    [Header("Prefabs")]
    public GameObject shrinePrefab;

    [Header("World Settings")]
    public Vector2Int cellSize;
    public Vector2Int worldSize;
    private Vector2Int middleCell;


    //spawned objects
    private List<PieceOfLand> lands;
    private List<GodShrine> shrines;

    private void Start()
    {
        GenerateWorld();
    }

    public void GenerateWorld()
    {
        //in the beginning was the word (John 1)
        lands = new List<PieceOfLand>();
        CalculateMiddle();

        for (int i = 0; i < worldSize.x; i++)
        {
            for (int j = 0; j < worldSize.y; j++)
            {
                if(!(middleCell.x == i && middleCell.y == j))
                {
                    int r = Random.Range(0, landTemplates.Count);
                    var landObj = Instantiate(landTemplates[r], GetCellPosition(i, j), Quaternion.identity);

                    var land = landObj.GetComponent<PieceOfLand>();
                    int rType = Random.Range(0, landTypes.Count);
                    land.GenerateLand(landTypes[rType]);

                    lands.Add(land);
                }    
            }
        }
    }

    public GodShrine SpawnShrine(Transform spawnPoint, S_God god)
    {
        var shrineObj = Instantiate(shrinePrefab, spawnPoint);

        var shrine = shrineObj.GetComponent<GodShrine>();
        shrine.TakeGod(god);
        return shrine;
    }

    private Vector3 GetCellPosition(int x, int y)
    {
        return new Vector3((x - middleCell.x) * cellSize.x, (y - middleCell.y) * cellSize.y, 0);
    }

    private void CalculateMiddle()
    {
        middleCell.x = Mathf.FloorToInt(worldSize.x / 2);
        middleCell.y = Mathf.FloorToInt(worldSize.y / 2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        CalculateMiddle();

        for (int i = 0; i < worldSize.x; i++)
        {
            for (int j = 0; j < worldSize.y; j++)
            {
                Gizmos.DrawWireCube(GetCellPosition(i,j), new Vector3(cellSize.x,cellSize.y,0));
            }
        }
    }

}
