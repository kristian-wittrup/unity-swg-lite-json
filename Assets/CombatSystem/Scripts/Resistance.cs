using UnityEngine;

[System.Serializable]
public class Resistance
{
    public float energyResistance;
    public float electricityResistance;
    public float stunResistance;
    public float heatResistance;
    public float coldResistance;
    public APType armor; // Level of armor : None, Light, Medium, Heavy

    public float GetResistanceValue(DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Energy:
                return energyResistance;
            case DamageType.Electricity:
                return electricityResistance;
            case DamageType.Stun:
                return stunResistance;
            case DamageType.Heat:
                return heatResistance;
            case DamageType.Cold:
                return coldResistance;
            default:
                return 0;
        }
    }
}