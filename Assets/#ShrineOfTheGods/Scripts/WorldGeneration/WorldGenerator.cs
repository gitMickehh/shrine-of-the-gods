using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        GenerateLands();
    }

    private List<S_LandType> GetLandsBasedOnRarity()
    {
        List<S_LandType> output = new List<S_LandType>();

        for (int i = 0; i < landTypes.Count; i++)
        {
            for (int j = 0; j < landTypes[i].rarity; j++)
            {
                output.Add(landTypes[i]);
            }
        }

        return output;
    }

    private void GenerateLands()
    {
        List<S_LandType> types = GetLandsBasedOnRarity();

        for (int i = 0; i < worldSize.x; i++)
        {
            for (int j = 0; j < worldSize.y; j++)
            {
                if (!(middleCell.x == i && middleCell.y == j))
                {
                    int r = Random.Range(0, landTemplates.Count);
                    var landObj = Instantiate(landTemplates[r], GetCellPosition(i, j), Quaternion.identity);
                    landObj.transform.SetParent(transform);

                    var land = landObj.GetComponent<PieceOfLand>();

                    S_LandType selectedType;

                    if(types.Count <= 0)
                    {
                        types = GetLandsBasedOnRarity();
                        int rType = Random.Range(0, types.Count);
                        selectedType = types[rType];

                        
                        types.RemoveAt(rType);
                    }
                    else if(types.Count == 1)
                    {
                        selectedType = types[0];
                        types.RemoveAt(0);
                    }
                    else
                    {
                        int rType = Random.Range(0, types.Count);
                        selectedType = types[rType];
                        types.RemoveAt(rType);
                    }

                    float randomGodCheck = Random.Range(0,1.0f);
                    if(randomGodCheck <= 0.4f && gods.Count > 0)
                    {
                        int randomGodIndex;

                        if (gods.Count == 1)
                        {
                            randomGodIndex = 0;
                        }
                        else
                        {
                            randomGodIndex = Random.Range(0, gods.Count);
                        }

                        land.GenerateLand(selectedType, SpawnShrine(transform, gods[randomGodIndex]).gameObject);
                        gods.RemoveAt(randomGodIndex);
                    }
                    else
                    {
                        land.GenerateLand(selectedType);
                    }

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
