using System;
using System.Collections.Generic;
using UnityEngine;

public class SMSystem : Singleton<SMSystem>
{
    private readonly Dictionary<Guid, SMComponent> m_smComponents = new();
    private Octree m_octtree;

    private void Awake()
    {
        m_octtree = new Octree(new Bounds(Vector3.zero, new Vector3(1000, 1000, 1000)), 0);
    }

    private void OnDrawGizmos()
    {
        if (m_octtree != null) m_octtree.DrawDebug();
    }

    public void RegisterObject(SMComponent component)
    {
        if (!m_smComponents.ContainsKey(component.Id))
        {
            m_smComponents.Add(component.Id, component);
            m_octtree.Insert(component);
        }
    }

    public void DeregisterObject(SMComponent component)
    {
        if (m_smComponents.ContainsKey(component.Id))
        {
            var _component = m_smComponents[component.Id];
            m_octtree.Remove(_component);
            m_smComponents.Remove(component.Id);
        }
    }

    public List<SMComponent> QueryRange(Bounds range)
    {
        return m_octtree.QueryRange(range);
    }
}