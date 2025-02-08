/* using UnityEngine;

[System.Serializable]
public class LootItem
{
    public string itemPrefabName;
    //public string itemPrefabPath; // Path to the prefab in Resources
    public float dropChance;
    [System.NonSerialized]
    public GameObject itemPrefab; // Loaded at runtime
} */



using UnityEngine;

[System.Serializable]
public class LootItem
{
    public string itemPrefabName;
    public float dropChance;
    [System.NonSerialized]
    public GameObject itemPrefab; // Loaded at runtime
}