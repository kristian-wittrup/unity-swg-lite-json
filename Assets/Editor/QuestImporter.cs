using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;

public class QuestImporter : EditorWindow
{
    private string jsonFilePath;

    [MenuItem("Quest System/Import Quests from JSON")]
    public static void ShowWindow()
    {
        GetWindow<QuestImporter>("Quest Importer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Import Quests from JSON", EditorStyles.boldLabel);

        if (GUILayout.Button("Select JSON File"))
        {
            jsonFilePath = EditorUtility.OpenFilePanel("Select JSON File", "", "json");
        }

        if (!string.IsNullOrEmpty(jsonFilePath))
        {
            GUILayout.Label("Selected File: " + jsonFilePath, EditorStyles.wordWrappedLabel);
        }

        if (GUILayout.Button("Import"))
        {
            ImportQuests();
        }
    }

    private void ImportQuests()
    {
        if (string.IsNullOrEmpty(jsonFilePath) || !File.Exists(jsonFilePath))
        {
            Debug.LogError("Invalid JSON file path.");
            return;
        }

        string jsonData = File.ReadAllText(jsonFilePath);
        QuestData[] quests = JsonConvert.DeserializeObject<QuestData[]>(jsonData);

        if (quests == null || quests.Length == 0)
        {
            Debug.LogError("No quests found in the JSON file.");
            return;
        }

        foreach (QuestData quest in quests)
        {
            if (quest == null)
            {
                Debug.LogError("Quest data is null.");
                continue;
            }

            string assetPath = $"Assets/Resources/Quests/{quest.questTitle}.asset";
            AssetDatabase.CreateAsset(quest, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Quests imported successfully.");
    }
}