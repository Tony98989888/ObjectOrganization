using System.Collections.Generic;
using UnityEngine;

public class SMOComponents : MonoBehaviour
{
    private Bounds m_detectBounds;
    private string m_instanceName;

    private bool m_LOSActive;
    [SerializeField] private float perceptionRadius;

    public List<SMComponent> DetectNearbySceneObjects(Bounds detectRange)
    {
        return SMSystem.Instance.QueryRange(detectRange);
    }
}