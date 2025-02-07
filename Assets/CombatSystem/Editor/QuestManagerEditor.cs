using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        QuestManager questManager = (QuestManager)target;

        if (questManager != null)
        {
// #if UNITY_EDITOR, this is the code that will be executed only in the editor. 
// https://docs.unity3d.com/6000.0/Documentation/Manual/platform-dependent-compilation.html           
#if UNITY_EDITOR
            List<LootGroup> lootGroups = LootManager.EditorLootGroups; 
            if (lootGroups != null)
            {            
                List<string> lootGroupNames = new List<string>(); 
                foreach (var lootGroup in lootGroups)
                {
                    // Add the loot group name to the list of names, so we can display it in the dropdown
                    lootGroupNames.Add(lootGroup.groupName); 
                }

                if (lootGroupNames.Count > 0)
                {
                    // Find the index of the currently selected loot group name
                    int selectedIndex = Mathf.Max(0, lootGroupNames.IndexOf(questManager.questRewardLootGroupName));
                    // Display a dropdown with the loot group names
                    selectedIndex = EditorGUILayout.Popup("Quest Reward Loot Group", selectedIndex, lootGroupNames.ToArray());
                    // Set the selected loot group name as the quest reward loot group name
                    questManager.questRewardLootGroupName = lootGroupNames[selectedIndex];
                }
                else
                {
                        // Display a warning if no loot groups are found
                    EditorGUILayout.HelpBox("No loot groups found.", MessageType.Warning);
                }
            }
            else
            {   
                EditorGUILayout.HelpBox("LootManager not found in the scene.", MessageType.Warning);
            }
#endif
        }
    }
}