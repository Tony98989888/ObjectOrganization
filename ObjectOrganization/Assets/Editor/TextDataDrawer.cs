using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TextData))]
public class TextDataDrawer : PropertyDrawer
{
    private TextDataList textDataList;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (textDataList == null)
            textDataList = AssetDatabase.LoadAssetAtPath<TextDataList>("Assets/Data/PredefinedText.asset");

        if (textDataList == null)
        {
            EditorGUI.HelpBox(position, "No TextDataList found. Please ensure it is located at the correct path.",
                MessageType.Warning);
            return;
        }

        var selectedTextProp = property.FindPropertyRelative("selectedText");

        EditorGUI.BeginProperty(position, label, property);

        var labelStyle = new GUIStyle(EditorStyles.label)
        {
            wordWrap = true,
            alignment = TextAnchor.UpperLeft,
            padding = new RectOffset(10, 10, 10, 10)
        };

        var boxStyle = new GUIStyle(GUI.skin.box);

        var texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(0.2f, 0.2f, 0.2f, 1.0f));
        texture.Apply();

        boxStyle.normal.background = texture;

        var boxRect = new Rect(position.x, position.y, position.width, position.height);

        GUI.Box(boxRect, GUIContent.none, boxStyle);

        var labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(labelRect, label);

        var textAreaRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width,
            EditorGUIUtility.singleLineHeight * 5);
        EditorGUI.LabelField(textAreaRect, selectedTextProp.stringValue, labelStyle);

        var buttonRect = new Rect(position.x, textAreaRect.y + textAreaRect.height + 4, position.width,
            EditorGUIUtility.singleLineHeight);
        if (GUI.Button(buttonRect, "Select Text")) TextSelectionWindow.Open(selectedTextProp, textDataList);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var totalHeight = EditorGUIUtility.singleLineHeight;
        totalHeight += EditorGUIUtility.singleLineHeight * 5 + 2;
        totalHeight += EditorGUIUtility.singleLineHeight + 4;
        return totalHeight;
    }
}