using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceOfLand : MonoBehaviour
{
    public S_LandType land;

    public SpriteRenderer landRenderer;

    public List<SpawnPoint> spawnPositions;
    List<SpawnPoint> availableSpawnPositions;
    
    [Header("Extra SpawnPoints")]
    public List<SpawnPoint> extraSpawnPoints;

    List<GameObject> spawnedObjects;

    public void GenerateLand(S_LandType landType)
    {
        land = landType;
        landRenderer.color = land.landColor;
        spawnedObjects = new List<GameObject>();
    }

    public void GenerateLandItems()
    {
        var templatesToSpawn = land.GetTemplatesBasedOnRarity();
        GenerateItems(templatesToSpawn);
    }

    private void GenerateItems(List<S_GenerationElement> templates)
    {
        while(HasAvailableSpawnPoint())
        {
            int r = Random.Range(0,templates.Count);
            var spawnPoint = RandomSpawnPoint();

            var elm = templates[r].SpawnInWorld(spawnPoint);
            AddObjectToSpawnPoint(spawnPoint, elm);
        }
    }

    public bool HasExtraSpace()
    {
        for (int i = 0; i < extraSpawnPoints.Count; i++)
        {
            if (!extraSpawnPoints[i].occupied)
                return true;
        }

        return false;
    }

    public GameObject SpawnExtra(GameObject prefab)
    {
        for (int i = 0; i < extraSpawnPoints.Count; i++)
        {
            if (!extraSpawnPoints[i].occupied)
            {
                var obj = Instantiate(prefab,extraSpawnPoints[i].spawnPos);
                return obj;
            }
        }

        return null;
    }

    public bool AddObjectRandomly(GameObject obj)
    {
        if(!HasAvailableSpawnPoint())
        {
            return false;
        }

        AddObjectToSpawnPoint(RandomSpawnPoint(),obj);

        return true;
    }

    public Vector3 GetAnyPosition()
    {
        int r = Random.Range(0, 2);
        int randomPoint;

        if(r == 0)
        {
            randomPoint = Random.Range(0, spawnPositions.Count);
            return spawnPositions[randomPoint].spawnPos.position;
        }
        else
        {
            randomPoint = Random.Range(0, extraSpawnPoints.Count);
            return extraSpawnPoints[randomPoint].spawnPos.position;
        }
    }

    public bool HasAvailableSpawnPoint()
    {
        availableSpawnPositions = spawnPositions.FindAll(x => !x.occupied).ToList();

        if (availableSpawnPositions == null || availableSpawnPositions.Count == 0)
        {
            return false;
        }

        return true;
    }

    public SpawnPoint RandomSpawnPoint()
    {
        int r = Random.Range(0, availableSpawnPositions.Count);
        return availableSpawnPositions[r];
    }

    public int NextSpawnPointIndex()
    {
        if(!HasAvailableSpawnPoint())
        {
            return -1;
        }

        int r = Random.Range(0, availableSpawnPositions.Count);

        var index = spawnPositions.FindIndex(x => x.spawnPos == availableSpawnPositions[r].spawnPos);
        return index;
    }


    private void AddObjectToSpawnPoint(SpawnPoint spawnPoint, GameObject gObject)
    {
        if (!spawnPoint.occupied)
        {
            gObject.transform.position = spawnPoint.spawnPos.position;
            gObject.transform.SetParent(spawnPoint.spawnPos);
            spawnPoint.Occupy(true);

            spawnedObjects.Add(gObject);
        }
    }

    private void AddObjectToSpawnPoint(int spawnPointIndex, GameObject gObject)
    {
        if(!spawnPositions[spawnPointIndex].occupied)
        {
            gObject.transform.position = spawnPositions[spawnPointIndex].spawnPos.position;
            gObject.transform.SetParent(spawnPositions[spawnPointIndex].spawnPos);
            
            spawnPositions[spawnPointIndex].Occupy(true);

            spawnedObjects.Add(gObject);
        }
    }

    public void RegrowSpawnPoints()
    {
        foreach (SpawnPoint sPoint in spawnPositions)
        {
            if(!sPoint.occupied)
                sPoint.GenerateNewObject();
        }
    }
}
