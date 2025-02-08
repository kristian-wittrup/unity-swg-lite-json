
 using System.Collections.Generic;
using UnityEngine;

public static class PrefabCache
{
    private static Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>(); 

    public static GameObject GetPrefabBasedOnName(string itemPrefabName)  // Name of the prefab, use in LootManager
    {
        if (!prefabCache.ContainsKey(itemPrefabName))
        {
            //  the path to the prefab based on the itemPrefabName
            string prefabPath = $"Loot/Weapons/{itemPrefabName}"; 
            Debug.Log($"Loading prefab from path: {prefabPath}");
            GameObject prefab = Resources.Load<GameObject>(prefabPath); // Should optimize this to load only once. 
            if (prefab != null)
            {
                prefabCache[itemPrefabName] = prefab;
                Debug.Log($"Prefab '{itemPrefabName}' loaded and cached successfully.");
            }
            else
            {
                Debug.LogError($"Failed to load prefab from path: {prefabPath}");
            }
        }

        if (prefabCache.ContainsKey(itemPrefabName))
        {
            return prefabCache[itemPrefabName];
        }

        Debug.LogError($"Prefab for name '{itemPrefabName}' not found in cache.");
        return null;
    }
} 

/* using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class PrefabCache : MonoBehaviour
{
    public static PrefabCache Instance; // Singleton instance

    [Tooltip("This is a list JUST for being able to pass the references in editor. Then I'll populate the dictionary with them, and never use it again.")]
    [SerializeField] GameObject[] allPrefabsForCache;

    private readonly Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure this GameObject persists across scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        // Populate the dictionary with the contents of the list
        foreach (var prefab in allPrefabsForCache)
        {
            prefabCache.Add(prefab.name, prefab);
        }

        // Load additional prefabs from JSON file
        LoadAllPrefabs(Path.Combine(Application.dataPath, "Resources/Loot", "lootgroups.json"));
    }

    public void LoadAllPrefabs(string jsonFilePath)
    {
        Debug.Log($"Loading all prefabs from JSON file at path: {jsonFilePath}");

        if (!File.Exists(jsonFilePath))
        {
            Debug.LogError($"JSON file not found at path: {jsonFilePath}");
            return;
        }

        string json = File.ReadAllText(jsonFilePath);
        var lootGroups = JsonConvert.DeserializeObject<List<LootGroup>>(json);

        foreach (var lootGroup in lootGroups)
        {
            foreach (var lootItem in lootGroup.lootItems)
            {
                if (!string.IsNullOrEmpty(lootItem.itemPrefabName) && !prefabCache.ContainsKey(lootItem.itemPrefabName))
                {
                    string prefabPath = $"Loot/Weapons/{lootItem.itemPrefabName}";
                    Debug.Log($"Loading prefab from path: {prefabPath}");
                    GameObject prefab = Resources.Load<GameObject>(prefabPath);
                    if (prefab != null)
                    {
                        prefabCache[lootItem.itemPrefabName] = prefab;
                        Debug.Log($"Prefab '{lootItem.itemPrefabName}' loaded and cached successfully.");
                    }
                    else
                    {
                        Debug.LogError($"Failed to load prefab from path: {prefabPath}");
                    }
                }
            }
        }

        // Log the contents of the cache for debugging purposes
        Debug.Log("Prefabs loaded into cache:");
        foreach (var key in prefabCache.Keys)
        {
            Debug.Log($"Cached prefab: {key}");
        }
    }

    public GameObject GetPrefabBasedOnName(string itemPrefabName)
    {
        if (prefabCache.ContainsKey(itemPrefabName))
        {
            return prefabCache[itemPrefabName];
        }

        Debug.LogError($"Prefab for name '{itemPrefabName}' not found in cache.");
        return null;
    }
} */

