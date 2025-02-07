using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public QuestData questToGive;
    private QuestManager questManager;
    private bool playerInRange;

    void Start()
    {
        questManager = Object.FindFirstObjectByType<QuestManager>();
        if (questManager == null)
        {
            Debug.LogError("QuestManager not found in the scene.");
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (questManager.currentQuest == questToGive)
            {
                if (questManager.GetCompletedObjectivesCount() == questToGive.objectives.Length)
                {
                    Debug.Log("Quest Completed: " + questToGive.questTitle);
                    questManager.CompleteQuest();
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