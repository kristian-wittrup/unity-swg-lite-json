using UnityEngine;

public class DamageTest : MonoBehaviour
{
    public CombatManager combatManager;
    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T key pressed, applying damage.");
            ApplyDamage();
        }
    }

    void ApplyDamage()
    {
        if (combatManager != null)
        {
            combatManager.ApplyDamage();
        }
        else
        {
            Debug.LogError("CombatManager is not assigned.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered range. Press T to apply damage.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited range.");
        }
    }
}