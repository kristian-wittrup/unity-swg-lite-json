using UnityEngine;

public class QuestCompleter : MonoBehaviour
{
    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CompleteObjective();
        }
    }

    void CompleteObjective()
    {
        QuestManager questManager = Object.FindFirstObjectByType<QuestManager>();
        if (questManager != null && questManager.currentQuest != null)
        {
            Debug.Log("Objective Completed: Talked to QuestCompleter");
            questManager.CompleteObjective("Talked to QuestCompleter");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered QuestCompleter range. Press E to interact.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited QuestCompleter range.");
        }
    }
}