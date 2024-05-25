using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // 要生成的物体的预设
    public int numberOfObjects = 100; // 要生成的物体的数量
    public Vector3 spawnAreaSize = new Vector3(1000, 1000, 1000); // 生成区域的大小

    private ObjectManager objectManager;

    private void Start()
    {
        objectManager = FindObjectOfType<ObjectManager>();

        for (int i = 0; i < numberOfObjects; i++)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        GameObject newObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity);
        if (newObject.GetComponent<SceneObject>() == null)
        {
            newObject.AddComponent<SceneObject>(); // 添加 SceneObject 脚本以注册到 ObjectManager
        }
    }
}