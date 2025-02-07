using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public string questRewardLootGroupName; // Name of the loot group in JSON
    public QuestData currentQuest;
    private List<string> completedObjectives = new List<string>();

    void Start()
    {
        if (currentQuest != null)
        {
            Debug.Log("Quest Started: " + currentQuest.questTitle);
        }
    }

    public void StartQuest(QuestData quest)
    {
        currentQuest = quest;
        completedObjectives.Clear();
        Debug.Log("Quest Started: " + currentQuest.questTitle);
    }

    public void CompleteObjective(string objective)
    {
        if (currentQuest != null && !completedObjectives.Contains(objective))
        {
            completedObjectives.Add(objective);
            Debug.Log("Objective Completed: " + objective);

            if (completedObjectives.Count == currentQuest.objectives.Length)
            {
                CompleteQuest();
            }
        }
    }

    public void CompleteQuest()
    {
        if (string.IsNullOrEmpty(questRewardLootGroupName))
        {
            Debug.LogError("questRewardLootGroupName is not set.");
            return;
        }

        // Add reward logic
        // Find the loot group by name and get a random item from it, yielding a list of rewards, or none
        LootGroup questRewardLootGroup = FindLootGroupByName(questRewardLootGroupName);
        if (questRewardLootGroup != null)
        {
            // Get a list of rewards
            List<LootItem> rewards = LootManager.GetRandomLoot(questRewardLootGroup);
            // Check if there are any rewards
            if (rewards != null && rewards.Count > 0)
            {
                foreach (var reward in rewards)
                {
                //  Instantiate(reward.itemPrefab, transform.position, Quaternion.identity); // Ask Arthur about next step, not sure how to implement this
                    Debug.Log($"Rewarded player with: {reward.itemPrefabName}");
                }
            }
            else
            {
                Debug.Log("No reward items selected.");
            }
        }


        // Find the loot group by name and get a random item from it, always yielding a reward
    /*     LootGroup questRewardLootGroup = FindLootGroupByName(questRewardLootGroupName);
        if (questRewardLootGroup != null)
        {
            LootItem reward = LootManager.GetRandomLoot(questRewardLootGroup);
            if (reward != null)
            {
//              Instantiate(reward.itemPrefab, transform.position, Quaternion.identity);
                Debug.Log($"Rewarded player with: {reward.itemPrefabName}");
            }
            else
            {
                Debug.Log("No reward item selected.");
            }
        } */
        else
        {
            // add a method to handle the case where the loot group is not found, and tell the player that the quest is completed but there is no reward, sucker!
            Debug.LogError($"Loot group '{questRewardLootGroupName}' not found.");
        }
    }

    public int GetCompletedObjectivesCount()
    {
        return completedObjectives.Count;
    }

    private LootGroup FindLootGroupByName(string groupName)
    {
        LootManager lootManager = Object.FindFirstObjectByType<LootManager>();
        if (lootManager == null)
        {
            Debug.LogError("LootManager not found in the scene.");
            return null;
        }

        foreach (var lootGroup in lootManager.lootGroups)
        {
            if (lootGroup.groupName == groupName)
            {
                return lootGroup;
            }
        }
        return null;
    }
}