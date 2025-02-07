using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class LootManager : MonoBehaviour
{
    public List<LootGroup> lootGroups;

    void Start()
    {
        LoadLootGroupsFromJSON("lootgroups.json");
    }

#if UNITY_EDITOR
    // Load loot groups in editor mode
    [UnityEditor.InitializeOnLoadMethod] // docs ive used if it breaks:: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/InitializeOnLoadMethodAttribute.html, https://docs.unity3d.com/Manual/RunningEditorCodeOnLaunch.html
    private static void LoadLootGroupsInEditor()
    {
        // Should be almopst a copy/paste for the LoadLootGroupsFromJSON method: remember to switch the path to filePath. 
        string filePath = Path.Combine(Application.dataPath, "Resources/Loot", "lootgroups.json");
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath); 
            var lootGroups = JsonConvert.DeserializeObject<List<LootGroup>>(json); // Did JsonUtility first, but switched to Newtonsoft.Json after errors with serialization, again, the docs ive used: https://www.newtonsoft.com/json/help/html/SerializingJSON.htm

            foreach (var lootGroup in lootGroups) 
            {
                foreach (var lootItem in lootGroup.lootItems)
                {
                    if (!string.IsNullOrEmpty(lootItem.itemPrefabName)) // Remember to change property name if json changes!!!!
                    {
                        GameObject prefab = PrefabCache.GetPrefabBasedOnName(lootItem.itemPrefabName); // Arthur idea to move the prefab to PreFabCache to avoid loading the same prefab multiple times
                        if (prefab != null)
                        {
                            lootItem.itemPrefab = prefab; 
                        }
                    }
                }
            }

            // Store the looaded loot groups in a static field for editor scripts to access
            EditorLootGroups = lootGroups; 
            Debug.Log("Loot groups loaded successfully in editor.");
        }
        else
        {
            Debug.LogError($"JSON file not found at path: {filePath}");
        }
    }

    public static List<LootGroup> EditorLootGroups { get; private set; }
#endif

    public void LoadLootGroupsFromJSON(string fileName)
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/Loot", fileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            lootGroups = JsonConvert.DeserializeObject<List<LootGroup>>(json);

            foreach (var lootGroup in lootGroups)
            {
                foreach (var lootItem in lootGroup.lootItems)
                {
                    if (!string.IsNullOrEmpty(lootItem.itemPrefabName))
                    {
                        GameObject prefab = PrefabCache.GetPrefabBasedOnName(lootItem.itemPrefabName);
                        if (prefab != null)
                        {
                            lootItem.itemPrefab = prefab;
                        }
                    }
                }
            }
            Debug.Log("Loot groups loaded successfully.");
        }
        else
        {
            Debug.LogError($"JSON file not found at path: {filePath}");
        }
    }



    // Individual roll for each item in the loot group, you can get zero, one or multiple items
    public static List<LootItem> GetRandomLoot(LootGroup lootGroup)
    {
        List<LootItem> droppedItems = new List<LootItem>();

        foreach (var item in lootGroup.lootItems)
        {
            float randomValue = Random.Range(0, 100); // Get a random value between 0 and 100
            if (randomValue <= item.dropChance)
            {
                droppedItems.Add(item); // Add the item to the dropped items list if the random value is less than or equal to the drop chance
            }
        }

        return droppedItems; // Return the list of dropped items
    }

    // This method is used to get a random loot item from a loot group, but it will always yield a reward between all the items in the loot group
    /* public static LootItem GetRandomLoot(LootGroup lootGroup)
    {
        float totalChance = 0f;
        foreach (var item in lootGroup.lootItems)
        {
            totalChance += item.dropChance; // Sum up all drop chances
        }

        if (totalChance == 0)
        {
            return null; // No items can be dropped
        }

        float randomValue = Random.Range(0, totalChance); // Get a random value between 0 and the total drop chance
        float cumulativeChance = 0f; // Used to compare with the random value, cumulative chance increases with each item.

        foreach (var item in lootGroup.lootItems)
        {
            cumulativeChance += item.dropChance; // Increase the cumulative chance
            if (randomValue <= cumulativeChance)
            {
                return item;
            }
        }

        return null; // In case no item is selected, which should not happen if probabilities are set correctly
    } */
 

}