using UnityEngine;

public class SceneObject : MonoBehaviour
{
    private SceneObjectData data;
    protected SMSystem manager;

    protected virtual void Start()
    {
        manager = FindObjectOfType<SMSystem>();
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

    protected virtual void Update()
    {
        data.UpdatePosition(transform.position);
    }
}