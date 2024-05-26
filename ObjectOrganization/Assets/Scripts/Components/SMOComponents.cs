using System.Collections.Generic;
using UnityEngine;

public class SMOComponents : SceneObject
{
    private string instanceName;

    [SerializeField] private float perceptionRadius;

    private bool needLOS;

    private Bounds detectBounds;

    public List<SceneObjectData> DetectNearbySceneObjects(Bounds detectRange)
    {
        return manager.QueryRange(detectRange);
    }
}