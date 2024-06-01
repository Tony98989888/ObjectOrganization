using UnityEngine;

[CreateAssetMenu(fileName = "TextDataList", menuName = "ScriptableObjects/TextDataList", order = 2)]
public class TextDataList : ScriptableObject
{
    [TextArea] public string[] texts;
}