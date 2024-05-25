using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSMData", menuName = "ScriptableObjects/SMData", order = 1)]
public class SMData : ScriptableObject
{
    public string InstanceName;
    public List<FreeFormInfo> FreeFormInformation;
    public List<PredefinedInfo> PredefinedInformation;
    public float IneractionRange;
}

public enum SMObjectType
{
    NORMAL,
    SPECIAL
}


[Serializable]
public class FreeFormInfo
{
    public SMObjectType Type;
    public string Value;
    public string ShortDescription;
    [TextArea] public string LongDescription;
    public int PriorityOverride;
}

[Serializable]
public class PredefinedInfo
{
    public SMObjectType Type;
    public string Value;
    public TextData ShortDescription;
    [TextArea] public string LongDescription;
}