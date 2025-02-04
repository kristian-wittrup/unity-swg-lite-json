using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public QuestData currentQuest;
    private HashSet<string> completedObjectives = new HashSet<string>();

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

    void CompleteQuest()
    {
        Debug.Log("Quest Completed: " + currentQuest.questTitle);
        // Add reward logic here
    }
}