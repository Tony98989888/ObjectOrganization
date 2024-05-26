using UnityEditor;
using UnityEngine;

public class TextSelectionWindow : EditorWindow
{
    private static SerializedProperty property;
    private static TextDataList textDataList;
    private Vector2 scrollPosition;
    private GUIStyle buttonStyle;
    private string searchText = string.Empty;

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
            wordWrap = true,
            alignment = TextAnchor.MiddleLeft,
            fontSize = 18,
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

        // 搜索栏
        EditorGUILayout.BeginHorizontal("box");
        EditorGUILayout.LabelField("Search:", GUILayout.Width(50));
        searchText = EditorGUILayout.TextField(searchText);
        EditorGUILayout.EndHorizontal();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        foreach (var text in textDataList.texts)
        {
            if (string.IsNullOrEmpty(searchText) || text.ToLower().Contains(searchText.ToLower()))
            {
                // 使用一个框来显示每个项目
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
        }

        EditorGUILayout.EndScrollView();
    }
}