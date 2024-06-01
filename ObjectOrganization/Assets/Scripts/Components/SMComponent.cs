using System;
using UnityEngine;

public class SMComponent : MonoBehaviour
{
    [SerializeField] private SMData m_smData;

    public SMData SMData => m_smData;
    public Guid Id { get; private set; }


    protected virtual void Start()
    {
        Id = Guid.NewGuid();
        // Can add ID to SMData here also
        SMSystem.Instance.RegisterObject(this);
    }

    private void OnDestroy()
    {
        SMSystem.Instance.DeregisterObject(this);
    }
}