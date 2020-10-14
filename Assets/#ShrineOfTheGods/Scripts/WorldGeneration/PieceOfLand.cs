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

    List<GameObject> spawnedObjects;

    public void GenerateLand(S_LandType landType, GameObject shrine = null)
    {
        land = landType;
        landRenderer.color = land.landColor;
        spawnedObjects = new List<GameObject>();

        if(shrine != null)
        {
            //place shrine here :)
            AddObjectToSpawnPoint(NextSpawnPointIndex(),shrine);
        }

        //Randomly Generate items here
        var templatesToSpawn = land.GetTemplatesBasedOnRarity();
        GenerateItems(templatesToSpawn);
    }

    private void GenerateItems(List<S_GenerationElement> templates)
    {

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

    public void AddObjectToSpawnPoint(int spawnPointIndex, GameObject gObject)
    {
        if(!spawnPositions[spawnPointIndex].occupied)
        {
            gObject.transform.position = spawnPositions[spawnPointIndex].spawnPos.position;
            gObject.transform.SetParent(spawnPositions[spawnPointIndex].spawnPos);

            spawnedObjects.Add(gObject);
        }
    }
}
