using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TextData))]
public class TextDataDrawer : PropertyDrawer
{
    private TextDataList textDataList;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (textDataList == null)
        {
            textDataList = AssetDatabase.LoadAssetAtPath<TextDataList>("Assets/Data/PredefinedText.asset");
        }

        if (textDataList == null)
        {
            EditorGUI.HelpBox(position, "No TextDataList found. Please ensure it is located at the correct path.", MessageType.Warning);
            return;
        }

        SerializedProperty selectedTextProp = property.FindPropertyRelative("selectedText");

        EditorGUI.BeginProperty(position, label, property);

        Rect labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(labelRect, label);

        Rect buttonRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUIUtility.singleLineHeight);

        if (GUI.Button(buttonRect, selectedTextProp.stringValue))
        {
            TextSelectionWindow.Open(selectedTextProp, textDataList);
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2 + 4;
    }
}