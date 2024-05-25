using UnityEngine;

public class SceneObject : MonoBehaviour
{
    private SceneObjectData data;
    private ObjectManager manager;

    private void Start()
    {
        manager = FindObjectOfType<ObjectManager>();
        data = new SceneObjectData(GetInstanceID(), transform.position, true);
        manager.RegisterObject(data);
    }

    private void OnDestroy()
    {
        if (manager != null)
        {
            manager.DeregisterObject(data.ID);
        }
    }

    private void Update()
    {
        data.UpdatePosition(transform.position);
    }
}