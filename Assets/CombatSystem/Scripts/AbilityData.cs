using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "CombatSystem/Ability")]
public class AbilityData : ScriptableObject
{
    public string abilityName;
    public float damageMultiplier = 1.0f; // Default multiplier is 1 (no change)
}