using System;
using UnityEngine;

public class SceneObjectData
{
    public int ID { get; private set; }
    public Vector3 Position { get; private set; }
    public bool IsActive { get; private set; }

    public SceneObjectData(int id, Vector3 position, bool isActive)
    {
        ID = id;
        Position = position;
        IsActive = isActive;
    }

    public void UpdatePosition(Vector3 newPosition)
    {
        Position = newPosition;
    }

    public void SetActive(bool active)
    {
        IsActive = active;
    }
}