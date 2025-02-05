using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;
    private Health health;

    void Start()
    {
        health = GetComponentInParent<Health>();
        if (health == null)
        {
            Debug.LogError("Health component not found in parent.");
        }
        if (healthBarFill == null)
        {
            Debug.LogError("HealthBarFill Image not assigned.");
        }
    }

    void Update()
    {
        if (health != null && healthBarFill != null)
        {
            healthBarFill.fillAmount = health.currentHealth / health.maxHealth;
        }
    }
}