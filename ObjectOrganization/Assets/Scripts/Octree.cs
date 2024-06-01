using System.Collections.Generic;
using UnityEngine;

public class Octree
{
    private const int MAX_OBJECTS_PER_NODE = 8;
    private const int MAX_LEVELS = 5;
    private readonly List<SMComponent> m_components;

    private readonly int m_level;
    private readonly Octree[] m_nodes;
    private Bounds m_bounds;

    public Octree(Bounds bounds, int level)
    {
        m_level = level;
        m_bounds = bounds;
        m_components = new List<SMComponent>();
        m_nodes = new Octree[8];
    }

    public void Insert(SMComponent component)
    {
        if (m_nodes[0] != null)
        {
            var index = GetIndex(component.transform.position);
            if (index != -1)
            {
                m_nodes[index].Insert(component);
                return;
            }
        }

        m_components.Add(component);

        if (m_components.Count > MAX_OBJECTS_PER_NODE && m_level < MAX_LEVELS)
        {
            if (m_nodes[0] == null) Split();

            var i = 0;
            while (i < m_components.Count)
            {
                var index = GetIndex(m_components[i].transform.position);
                if (index != -1)
                {
                    m_nodes[index].Insert(m_components[i]);
                    m_components.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }

    public void Remove(SMComponent component)
    {
        var index = GetIndex(component.transform.position);
        if (index != -1 && m_nodes[0] != null)
            m_nodes[index].Remove(component);
        else
            m_components.Remove(component);
    }

    public List<SMComponent> QueryRange(Bounds bounds)
    {
        var returnObjects = new List<SMComponent>();

        if (!m_bounds.Intersects(bounds)) return returnObjects;

        foreach (var component in m_components)
            if (bounds.Contains(component.transform.position))
                returnObjects.Add(component);

        if (m_nodes[0] != null)
            for (var i = 0; i < 8; i++)
                returnObjects.AddRange(m_nodes[i].QueryRange(bounds));

        return returnObjects;
    }

    private void Split()
    {
        var subWidth = m_bounds.size.x * 0.5f;
        var subHeight = m_bounds.size.y * 0.5f;
        var subDepth = m_bounds.size.z * 0.5f;

        var size = new Vector3(subWidth, subHeight, subDepth);
        var center = m_bounds.center;

        m_nodes[0] = new Octree(new Bounds(center + new Vector3(-subWidth, subHeight, subDepth) * 0.5f, size),
            m_level + 1);
        m_nodes[1] = new Octree(new Bounds(center + new Vector3(subWidth, subHeight, subDepth) * 0.5f, size),
            m_level + 1);
        m_nodes[2] = new Octree(new Bounds(center + new Vector3(-subWidth, subHeight, -subDepth) * 0.5f, size),
            m_level + 1);
        m_nodes[3] = new Octree(new Bounds(center + new Vector3(subWidth, subHeight, -subDepth) * 0.5f, size),
            m_level + 1);
        m_nodes[4] = new Octree(new Bounds(center + new Vector3(-subWidth, -subHeight, subDepth) * 0.5f, size),
            m_level + 1);
        m_nodes[5] = new Octree(new Bounds(center + new Vector3(subWidth, -subHeight, subDepth) * 0.5f, size),
            m_level + 1);
        m_nodes[6] = new Octree(new Bounds(center + new Vector3(-subWidth, -subHeight, -subDepth) * 0.5f, size),
            m_level + 1);
        m_nodes[7] = new Octree(new Bounds(center + new Vector3(subWidth, -subHeight, -subDepth) * 0.5f, size),
            m_level + 1);
    }

    private int GetIndex(Vector3 position)
    {
        var index = -1;
        var center = m_bounds.center;

        var top = position.y > center.y;
        var bottom = !top;
        var front = position.z > center.z;
        var back = !front;
        var left = position.x < center.x;
        var right = !left;

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
        Gizmos.DrawWireCube(m_bounds.center, m_bounds.size);

        if (m_nodes[0] != null)
            for (var i = 0; i < 8; i++)
                m_nodes[i].DrawDebug();
    }
}