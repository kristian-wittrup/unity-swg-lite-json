using System.Collections.Generic;
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

