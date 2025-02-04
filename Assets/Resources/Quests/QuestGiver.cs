using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestData questToGive; // Make sure this field is public
    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            Interact();
        }
    }

    void Interact()
    {
        QuestManager questManager = Object.FindFirstObjectByType<QuestManager>();
        if (questManager != null && questToGive != null)
        {
            if (questManager.currentQuest == questToGive)
            {
                if (questManager.currentQuest != null && questManager.currentQuest.questGiver == gameObject.name)
                {
                    questManager.CompleteObjective("Talked to QuestGiver");
                }
                else
                {
                    Debug.Log("Quest Completed: " + questToGive.questTitle);
                }
            }
            else
            {
                questManager.StartQuest(questToGive);
                Debug.Log("Quest Accepted: " + questToGive.questTitle);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered NPC range. Press E to talk to NPC.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited NPC range.");
        }
    }
}