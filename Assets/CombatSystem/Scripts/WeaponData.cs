using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "CombatSystem/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float minDamage;
    public float maxDamage;
    public DamageType damageType;
    public APType armorPenetration; // Weapons AP type
}

public enum DamageType
{
    Energy,
    Electricity,
    Stun,
    Heat,
    Cold
}

public enum APType
{
    None,
    Light,
    Medium,
    Heavy
}