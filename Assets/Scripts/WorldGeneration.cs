using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class WorldGeneration: MonoBehaviour
{
    [SerializeField] Dictionary<GameObject, float> PrefabsCollisions;
    public List<GameObject> PrefabsDecor;
    
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
            var index = Random.Range(0, PrefabsDecor.Count);
            Debug.Log(index);

            GameObject prefab = PrefabsDecor[index];
            var yPos = prefab.transform.position.y;
            Debug.Log(yPos);

            Vector3 spawnPos = new Vector3(
                Random.Range(StartingPoint.x, StartingPoint.x + SpawnArea.x),
                yPos,
                Random.Range(StartingPoint.y, StartingPoint.y + SpawnArea.y)
                );


            //prefab: GameObject, to clone
            //spawn pos: Vector3, position to put
            //
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
