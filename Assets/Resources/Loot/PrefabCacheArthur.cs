// asyncload attempt - Might be a bit overboard, but i wanted to try it out + chance of loading faster. 
// Cons: Might be a bit more complex than needed and need to be careful with loading order.

/** note
*  The allPrefabsForCache array in the Unity inspector is there for convenience, but it is not required for the automatic loading process. 
*  The LoadPrefabs method will read the JSON file, load the prefabs specified in the JSON, and populate the prefabCache dictionary.
*/

using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Collections;

namespace com.arthur.isthebest.cache
{
    public class PrefabCacheArthur : MonoBehaviour
    {
        public static PrefabCacheArthur Instance;

        [Tooltip("This is a list JUST for being able to pass the references in editor. Then i'll populate the dictionary with them, and never use it again.")]
        [SerializeField] GameObject[] allPrefabsForCache;

        private readonly Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            StartCoroutine(LoadPrefabs());
        }

        private IEnumerator LoadPrefabs()
        {
            // Load and parse the JSON data
            string filePath = Path.Combine(Application.dataPath, "Resources/Loot/lootGroups.json");
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                List<LootGroup> lootGroupList = JsonConvert.DeserializeObject<List<LootGroup>>(jsonData);

                // Populate the dictionary with the contents of the JSON data
                foreach (var lootGroup in lootGroupList)
                {
                    foreach (var lootItem in lootGroup.lootItems)
                    {
                        ResourceRequest request = Resources.LoadAsync<GameObject>($"Loot/Weapons/{lootItem.itemPrefabName}");
                        yield return request;

                        GameObject prefab = request.asset as GameObject;
                        if (prefab != null)
                        {
                            prefabCache.Add(lootItem.itemPrefabName, prefab);
                        }
                        else
                        {
                            Debug.LogWarning($"Prefab not found for item: {lootItem.itemPrefabName}");
                        }
                    }
                }
            }
            else
            {
                Debug.LogError($"JSON file not found at path: {filePath}");
            }
        }

        public GameObject GetPrefabBasedOnName(string itemPrefabName)
        {
            if (!prefabCache.ContainsKey(itemPrefabName))
            {
                Debug.Log("You didn't provide the prefab to the cache, but i'm going to try to load it");

                string prefabPath = $"Loot/Weapons/{itemPrefabName}";
                Debug.Log($"Loading prefab from path: {prefabPath}");
                GameObject prefab = Resources.Load<GameObject>(prefabPath);
                if (prefab != null)
                {
                    prefabCache.Add(itemPrefabName, prefab);
                    return prefab;
                }
                else
                {
                    Debug.LogError($"Prefab not found at path: {prefabPath}");
                    return null;
                }
            }

            return prefabCache[itemPrefabName];
        }
    }
}


// Working version of the PrefabCacheArthur script. This script is meant to be used as a Singleton, and it will cache all prefabs in the Resources/Loot/Weapons folder. It will also attempt to load prefabs that are not in the cache, but this should be avoided if possible due to performance reasons. The script is designed to be used in conjunction with the LootManager script, which will use the PrefabCacheArthur to load prefabs based on their names.
/* using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace com.arthur.isthebest.cache
{
    public class PrefabCacheArthur : MonoBehaviour
    {
        public static PrefabCacheArthur Instance;

        [Tooltip("This is a list JUST for being able to pass the references in editor. Then i'll populate the dictionary with them, and never use it again.")]
        [SerializeField] GameObject[] allPrefabsForCache;

        private readonly Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            // Load and parse the JSON data
            string filePath = Path.Combine(Application.dataPath, "Resources/Loot/lootGroups.json");
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                LootGroupList lootGroupList = JsonConvert.DeserializeObject<LootGroupList>(jsonData);

                // Populate the dictionary with the contents of the JSON data
                foreach (var lootGroup in lootGroupList.lootGroups)
                {
                    foreach (var lootItem in lootGroup.lootItems)
                    {
                        GameObject prefab = Resources.Load<GameObject>($"Loot/Weapons/{lootItem.itemPrefabName}");
                        if (prefab != null)
                        {
                            prefabCache.Add(lootItem.itemPrefabName, prefab);
                        }
                        else
                        {
                            Debug.LogWarning($"Prefab not found for item: {lootItem.itemPrefabName}");
                        }
                    }
                }
            }
            else
            {
                Debug.LogError($"JSON file not found at path: {filePath}");
            }
        }

        public GameObject GetPrefabBasedOnName(string itemPrefabName)
        {
            if (!prefabCache.ContainsKey(itemPrefabName))
            {
                Debug.Log("You didn't provide the prefab to the cache, but i'm going to try to load it");

                string prefabPath = $"Loot/Weapons/{itemPrefabName}";
                Debug.Log($"Loading prefab from path: {prefabPath}");
                GameObject prefab = Resources.Load<GameObject>(prefabPath);
                if (prefab != null)
                {
                    prefabCache.Add(itemPrefabName, prefab);
                    return prefab;
                }
                else
                {
                    Debug.LogError($"Prefab not found at path: {prefabPath}");
                    return null;
                }
            }

            return prefabCache[itemPrefabName];
        }
    }
} */

/* using System.Collections.Generic;
using UnityEngine;


namespace com.arthur.isthebest.cache
{
    /// <summary>
    /// Meant to be an example taking your <see cref="PrefabCache"/> and adapting it without resources.load!
    /// Not trying to step on your feet, but trying to explain through code
    /// </summary>
    public class PrefabCacheArthur : MonoBehaviour
    {
        // Just a little example in case you want it... If you want to easily access this as a Singleton
        public static PrefabCacheArthur Instance; // You can now from anywhere say "PrefabCacheArthur.Instance.GetPrefabBasedOnName(); without static the whole class. Notice the set in Awake()

        [Tooltip("This is a list JUST for being able to pass the references in editor. Then i'll populate the dictionary with them, and never use it again.")]
        [SerializeField] GameObject[] allPrefabsForCache;

        private readonly Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            // Here i'm going to just populate the dictionary with the contents of the LIST!
            // This will only happen once, on game start.
            foreach (var prefab in allPrefabsForCache)
            {
                prefabCache.Add(prefab.name, prefab);
            }
        }

        /// <summary>
        /// Stole your code, but removing the resources.load parts!
        /// </summary>
        public GameObject GetPrefabBasedOnName(string itemPrefabName)  // Name of the prefab, use in LootManager
        {
            if (!prefabCache.ContainsKey(itemPrefabName))
            {
                // I suppose i'm going to leave this... kinda a cool workaround. a 'last ditch' effort to say "hey you didn't give it to the cache, but i'm going to fetch it!"
                // Thinking about this more, your way is actually a cool 'lazy initialization' way to populate the database... Resources.Load is expensive though, so i do think its probably
                // Still better to pre populate. But your way was 100% fine in a smaller game. Cool.
                Debug.Log("You didn't provide the prefab to the cache, but i'm going to try to load it");

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

}

 */