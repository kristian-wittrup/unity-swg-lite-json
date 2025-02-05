using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public WeaponData weapon;
    public Resistance resistance;
    public Health targetHealth;
    public MitigationLevel mitigationLevel; // Grabbin this from the enum in the other script for mitigation level
    public AbilityData selectedAbility; // For future Skill System implementation - Wallawocko system

    public void ApplyDamage()
    {
        float damage = DamageCalculator.CalculateDamage(weapon, resistance, mitigationLevel, selectedAbility);
        targetHealth.AdjustHealth(-damage);
        Debug.Log($"Applied {damage} damage to the target.");
    }
}