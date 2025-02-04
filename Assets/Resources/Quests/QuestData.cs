using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    public string questTitle;
    public string questDescription;
    public string questGiver;
    public string[] objectives;
    public string rewardItem;
    public string questCompleter; // New field for quest completion objective
}