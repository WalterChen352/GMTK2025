using UnityEngine;
using System.Collections.Generic;

public class WorldGeneration: MonoBehaviour
{
    public List<GameObject> prefabs;
    public Vector2 SpawnArea = new Vector2(50f, 50f);
    public Vector2 ScaleRange = new Vector2(0.8f, 1.2f);
    public Vector2 StartingPoint;
    public int MinObjects;
    public int MaxObjects;
    private int numObjects;
    public int MinBerries;
    public int MaxBerries;
    private void Start()
    {
        numObjects = Random.Range(MinObjects, MaxObjects);
        Debug.Log($"Generating {numObjects} objects");
        GenerateWorld();
    }


    public void GenerateWorld()
    {
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 spawnPos = new Vector3(
                Random.Range(StartingPoint.x, StartingPoint.x + SpawnArea.x),
                0,
                Random.Range(StartingPoint.y, StartingPoint.y + SpawnArea.y)
                );

            GameObject prefab = prefabs[Random.Range(0, prefabs.Count)];


            GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
            if (prefab.name == "Bush")
            {
                BushState bushState = obj.GetComponent<BushState>();
                var numBerries = Random.Range(MinBerries, MaxBerries);
                Debug.Log($"bush initialized with {numBerries} berries");
                bushState.Initialize(numBerries);

            }

            float scale = Random.Range(ScaleRange.x, ScaleRange.y);
            obj.transform.localScale = Vector3.one * scale;
        }
    }
}
