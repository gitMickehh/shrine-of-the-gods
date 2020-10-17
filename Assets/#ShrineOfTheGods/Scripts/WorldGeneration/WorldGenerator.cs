using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("Gods")]
    public S_GodsList gods;
    private List<S_God> godsPrivate;

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

    [Header("Effects In World")]
    public GameObject rain_VFX_Prefab;
    private List<GameObject> allRainVFX;
    [Space]
    public GameObject scripturePrefab;
    [Space]
    public GameObject MummyPrefab;

    //spawned objects
    private List<PieceOfLand> lands;
    private List<GodShrine> shrines;
    private List<GameObject> thothScrpitures;
    private List<Mummy> mummiesSpawned;

    private void Start()
    {
        GenerateWorld();
    }

    public void GenerateWorld()
    {
        //in the beginning was the word (John 1)
        gods.ClearPowers();

        godsPrivate = gods.items.ToList();
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
                 
                    land.GenerateLand(selectedType);
                    lands.Add(land);
                }
            }
        }
        
        SpawnShrinesInLand();
        GenerateLandsItems();
    }

    private void SpawnShrinesInLand()
    {
        int rStart = Random.Range(0, Mathf.FloorToInt(lands.Count / 4.0f));
        PlaceRandomShrine(rStart);

        int shrineDistance = 1;
        int maxShrineDistance = Random.Range(2, Mathf.FloorToInt((worldSize.x * worldSize.y) / 3));

        for (int i = rStart + 1; i < lands.Count; i++)
        {
            if (godsPrivate.Count <= 0)
                return;

            if (shrineDistance >= maxShrineDistance)
            {
                //must spawn god now
                PlaceRandomShrine(i);
                shrineDistance = 1;
            }
            else
            {
                float randomChanceToSpawnGod = Random.Range(0,1.0f);
                if(randomChanceToSpawnGod <= 0.45f)
                {
                    //spawn god :)
                    PlaceRandomShrine(i);
                    shrineDistance = 1;
                }
                else
                {
                    shrineDistance++;
                }
            }
        }
    }

    private void PlaceRandomShrine(int landIndex)
    {
        int randomGodIndex = Random.Range(0, godsPrivate.Count);
        lands[landIndex].AddObjectRandomly(SpawnShrine(godsPrivate[randomGodIndex]));
        godsPrivate.RemoveAt(randomGodIndex);
    }

    private void GenerateLandsItems()
    {
        for (int i = 0; i < lands.Count; i++)
        {
            lands[i].GenerateLandItems();
        }
    }

    public GameObject SpawnShrine(S_God god)
    {
        var shrineObj = Instantiate(shrinePrefab, transform);

        var shrine = shrineObj.GetComponentInChildren<GodShrine>();
        shrine.TakeGod(god);
        return shrineObj;
    }

    private Vector3 GetCellPosition(int x, int y)
    {
        return new Vector3((x - middleCell.x) * cellSize.x, (y - middleCell.y) * cellSize.y, 0);
    }

    public void RegrowSpawnPoints()
    {
        foreach (PieceOfLand land in lands)
        {
            land.RegrowSpawnPoints();
        }
    }

    private void CalculateMiddle()
    {
        middleCell.x = Mathf.FloorToInt(worldSize.x / 2);
        middleCell.y = Mathf.FloorToInt(worldSize.y / 2);
    }

    public void Rain(bool rainOn)
    {
        if(rainOn)
        {
            if (allRainVFX == null)
                allRainVFX = new List<GameObject>();
            else
            {
                foreach (GameObject rain in allRainVFX)
                {
                    Destroy(rain);
                }
                allRainVFX.Clear();
            }

            for (int i = 0; i < worldSize.x; i++)
            {
                for (int j = 0; j < worldSize.y; j++)
                {
                    var rainObj = Instantiate(rain_VFX_Prefab, GetCellPosition(i, j), Quaternion.identity);
                    rainObj.transform.SetParent(transform);
                    allRainVFX.Add(rainObj);
                }
            }
        }
        else
        {
            if (allRainVFX == null)
                return;
            else
            {
                foreach (GameObject rain in allRainVFX)
                {
                    Destroy(rain);
                }
            }
        }
    }

    public void SpawnMummies(int numberOfMummies)
    {
        if(mummiesSpawned==null)
        {
            mummiesSpawned = new List<Mummy>();

            for (int i = 0; i < numberOfMummies; i++)
            {
                int r = Random.Range(0,lands.Count);
                var mummyPos = lands[r].GetAnyPosition();
                var mummy = Instantiate(MummyPrefab,mummyPos,Quaternion.identity);
                mummy.transform.SetParent(transform);

                mummiesSpawned.Add(mummy.GetComponent<Mummy>());
            }
        }
    }

    public void KillAllMummies()
    {
        if(mummiesSpawned != null)
        {
            foreach (Mummy mummy in mummiesSpawned)
            {
                mummy.KillMummy();
            }

            mummiesSpawned.Clear();
            mummiesSpawned = null;
        }
    }

    private GameObject SpawnExtra(GameObject spawnPrefab)
    {
        int randomLand = Random.Range(0,lands.Count);

        var obj = lands[randomLand].SpawnExtra(spawnPrefab);

        return obj;
    }

    public void SpawnOrModifyThothScriptures(S_ConversationsList shrineLines)
    {
        if(thothScrpitures == null)
        {
            thothScrpitures = SpawnThothShrines(shrineLines.items.Count);
        }
        else if(thothScrpitures.Count < shrineLines.items.Count)
        {
            foreach (GameObject oldShrine in thothScrpitures)
            {
                Destroy(oldShrine);
            }

            thothScrpitures = SpawnThothShrines(shrineLines.items.Count);
        }

        for (int i = 0; i < shrineLines.items.Count; i++)
        {
            thothScrpitures[i].GetComponent<Talker>().conversationPiece = shrineLines.items[i];
        }
    }

    private List<GameObject> SpawnThothShrines(int numberOfShrines)
    {
        List<GameObject> scriptureSpawned = new List<GameObject>();

        for (int i = 0; i < numberOfShrines; i++)
        {
            scriptureSpawned.Add(SpawnExtra(scripturePrefab));
        }

        return scriptureSpawned;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
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
