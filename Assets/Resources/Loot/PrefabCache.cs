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