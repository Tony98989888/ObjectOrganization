using UnityEngine;
using System.Collections.Generic;

public class ObjectManager : MonoBehaviour
{
    private Dictionary<int, SceneObjectData> objectDictionary = new Dictionary<int, SceneObjectData>();
    private Octree octree;

    private void Awake()
    {
        octree = new Octree(new Bounds(Vector3.zero, new Vector3(1000, 1000, 1000)), 0);
    }

    public void RegisterObject(SceneObjectData obj)
    {
        if (!objectDictionary.ContainsKey(obj.ID))
        {
            objectDictionary.Add(obj.ID, obj);
            octree.Insert(obj);
        }
    }

    public void DeregisterObject(int id)
    {
        if (objectDictionary.ContainsKey(id))
        {
            SceneObjectData obj = objectDictionary[id];
            octree.Remove(obj);
            objectDictionary.Remove(id);
        }
    }

    public List<SceneObjectData> QueryRange(Bounds range)
    {
        return octree.QueryRange(range);
    }

    private void OnDrawGizmos()
    {
        if (octree != null)
        {
            octree.DrawDebug();
        }
    }
}