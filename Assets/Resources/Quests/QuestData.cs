using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    public string questTitle;
    public string questDescription;
    public string questGiver;
    public string[] objectives;
    public string questRewardLootGroupName; // Field for the loot group name
    public string questCompleter; // Field for quest completion objective
}