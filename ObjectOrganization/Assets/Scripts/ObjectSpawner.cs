using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public int numberOfObjects = 100;
    public Vector3 spawnAreaSize = new(1000, 1000, 1000);

    private void Start()
    {
        for (var i = 0; i < numberOfObjects; i++) SpawnObject();
    }

    private void SpawnObject()
    {
        var randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        var newObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);
        if (newObject.GetComponent<SMComponent>() == null) newObject.AddComponent<SMComponent>();
    }
}