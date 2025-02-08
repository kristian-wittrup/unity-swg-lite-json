using System.Collections.Generic;

[System.Serializable]
public class LootGroup
{
    public string groupName;
    public List<LootItem> lootItems;
}

[System.Serializable]
public class LootGroupList
{
    public List<LootGroup> lootGroups;
}