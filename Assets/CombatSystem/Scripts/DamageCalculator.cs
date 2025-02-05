using UnityEngine;

public static class DamageCalculator
{
    public static float CalculateDamage(WeaponData weapon, Resistance resistance, MitigationLevel mitigationLevel, AbilityData ability)
    {
        // Step 1: Calculate base damage with resistance of the target
        float baseDamage = Random.Range(weapon.minDamage, weapon.maxDamage);
        float resistanceValue = resistance.GetResistanceValue(weapon.damageType);
        float damageAfterResistance = baseDamage * (100 - resistanceValue) / 100;

        // Step 2: Apply mitigation - if any, default mitigation level is None or 0%.
        float mitigationValue = Mitigation.GetMitigationValue(mitigationLevel);
        float damageAfterMitigation = damageAfterResistance * (1 - mitigationValue);

        // Step 3: Adjust damage based on AP and Armor of the weapon and the target armor rating.
        float damageAfterAP = AdjustDamageForAP(damageAfterMitigation, weapon.armorPenetration, resistance.armor);

        // Step 4: Apply ability multiplier
        float abilityMultiplier = (ability != null) ? ability.damageMultiplier : 1.0f;
        float finalDamage = damageAfterAP * abilityMultiplier;

        // Round to the nearest whole number
        finalDamage = Mathf.Round(finalDamage);
        return finalDamage;
    }

    private static float AdjustDamageForAP(float damage, APType weaponAP, APType armor)
    {
        float adjustment = 0;

        if (weaponAP == APType.None)
        {
            if (armor == APType.Light) adjustment = -0.25f;
            else if (armor == APType.Medium) adjustment = -0.50f;
            else if (armor == APType.Heavy) adjustment = -0.75f;
        }
        else if (weaponAP == APType.Light)
        {
            if (armor == APType.None) adjustment = 0.25f;
            else if (armor == APType.Medium) adjustment = -0.25f;
            else if (armor == APType.Heavy) adjustment = -0.50f;
        }
        else if (weaponAP == APType.Medium)
        {
            if (armor == APType.None) adjustment = 0.50f;
            else if (armor == APType.Light) adjustment = 0.25f;
            else if (armor == APType.Heavy) adjustment = -0.25f;
        }
        else if (weaponAP == APType.Heavy)
        {
            if (armor == APType.None) adjustment = 0.75f;
            else if (armor == APType.Light) adjustment = 0.50f;
            else if (armor == APType.Medium) adjustment = 0.25f;
        }

        return damage * (1 + adjustment);
    }
}