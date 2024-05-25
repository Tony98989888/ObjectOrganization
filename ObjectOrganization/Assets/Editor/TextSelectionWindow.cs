using UnityEditor;
using UnityEngine;

public class TextSelectionWindow : EditorWindow
{
    private static SerializedProperty property;
    private static TextDataList textDataList;
    private Vector2 scrollPosition;
    private GUIStyle buttonStyle;

    public static void Open(SerializedProperty prop, TextDataList list)
    {
        property = prop;
        textDataList = list;
        TextSelectionWindow window = GetWindow<TextSelectionWindow>("Select Text");
        window.minSize = new Vector2(300, 400);
        window.Show();
    }

    private void OnEnable()
    {
        buttonStyle = new GUIStyle(GUI.skin.button)
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 12,
            normal = { textColor = Color.cyan },
            hover = { textColor = Color.magenta }
        };
    }

    private void OnGUI()
    {
        if (textDataList == null || property == null)
        {
            EditorGUILayout.HelpBox("TextDataList or Property is not set.", MessageType.Error);
            return;
        }

        EditorGUILayout.LabelField("Select a text", EditorStyles.boldLabel);
        
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        foreach (var text in textDataList.texts)
        {
            EditorGUILayout.BeginVertical("box");
            if (GUILayout.Button(text, buttonStyle))
            {
                property.stringValue = text;
                property.serializedObject.ApplyModifiedProperties();
                Close();
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }
        
        EditorGUILayout.EndScrollView();
    }
}