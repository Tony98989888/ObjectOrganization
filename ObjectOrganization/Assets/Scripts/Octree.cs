using System.Collections.Generic;
using UnityEngine;

public class Octree
{
    private const int MAX_OBJECTS = 8;
    private const int MAX_LEVELS = 5;

    private int level;
    private List<SceneObjectData> objects;
    private Bounds bounds;
    private Octree[] nodes;

    public Octree(Bounds bounds, int level)
    {
        this.level = level;
        this.bounds = bounds;
        objects = new List<SceneObjectData>();
        nodes = new Octree[8];
    }

    public void Insert(SceneObjectData obj)
    {
        if (nodes[0] != null)
        {
            int index = GetIndex(obj.Position);
            if (index != -1)
            {
                nodes[index].Insert(obj);
                return;
            }
        }

        objects.Add(obj);

        if (objects.Count > MAX_OBJECTS && level < MAX_LEVELS)
        {
            if (nodes[0] == null)
            {
                Split();
            }

            int i = 0;
            while (i < objects.Count)
            {
                int index = GetIndex(objects[i].Position);
                if (index != -1)
                {
                    nodes[index].Insert(objects[i]);
                    objects.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }

    public void Remove(SceneObjectData obj)
    {
        int index = GetIndex(obj.Position);
        if (index != -1 && nodes[0] != null)
        {
            nodes[index].Remove(obj);
        }
        else
        {
            objects.Remove(obj);
        }
    }

    public List<SceneObjectData> QueryRange(Bounds range)
    {
        List<SceneObjectData> returnObjects = new List<SceneObjectData>();

        if (!bounds.Intersects(range))
        {
            return returnObjects;
        }

        foreach (var obj in objects)
        {
            if (range.Contains(obj.Position))
            {
                returnObjects.Add(obj);
            }
        }

        if (nodes[0] != null)
        {
            for (int i = 0; i < 8; i++)
            {
                returnObjects.AddRange(nodes[i].QueryRange(range));
            }
        }

        return returnObjects;
    }

    private void Split()
    {
        float subWidth = bounds.size.x / 2f;
        float subHeight = bounds.size.y / 2f;
        float subDepth = bounds.size.z / 2f;

        Vector3 size = new Vector3(subWidth, subHeight, subDepth);
        Vector3 center = bounds.center;

        nodes[0] = new Octree(new Bounds(center + new Vector3(-subWidth, subHeight, subDepth) / 2, size), level + 1);
        nodes[1] = new Octree(new Bounds(center + new Vector3(subWidth, subHeight, subDepth) / 2, size), level + 1);
        nodes[2] = new Octree(new Bounds(center + new Vector3(-subWidth, subHeight, -subDepth) / 2, size), level + 1);
        nodes[3] = new Octree(new Bounds(center + new Vector3(subWidth, subHeight, -subDepth) / 2, size), level + 1);
        nodes[4] = new Octree(new Bounds(center + new Vector3(-subWidth, -subHeight, subDepth) / 2, size), level + 1);
        nodes[5] = new Octree(new Bounds(center + new Vector3(subWidth, -subHeight, subDepth) / 2, size), level + 1);
        nodes[6] = new Octree(new Bounds(center + new Vector3(-subWidth, -subHeight, -subDepth) / 2, size), level + 1);
        nodes[7] = new Octree(new Bounds(center + new Vector3(subWidth, -subHeight, -subDepth) / 2, size), level + 1);
    }

    private int GetIndex(Vector3 position)
    {
        int index = -1;
        Vector3 center = bounds.center;

        bool top = position.y > center.y;
        bool bottom = !top;
        bool front = position.z > center.z;
        bool back = !front;
        bool left = position.x < center.x;
        bool right = !left;

        if (top)
        {
            if (front)
            {
                if (left) index = 0;
                else if (right) index = 1;
            }
            else if (back)
            {
                if (left) index = 2;
                else if (right) index = 3;
            }
        }
        else if (bottom)
        {
            if (front)
            {
                if (left) index = 4;
                else if (right) index = 5;
            }
            else if (back)
            {
                if (left) index = 6;
                else if (right) index = 7;
            }
        }

        return index;
    }

    public void DrawDebug()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(bounds.center, bounds.size);

        if (nodes[0] != null)
        {
            for (int i = 0; i < 8; i++)
            {
                nodes[i].DrawDebug();
            }
        }
    }
}