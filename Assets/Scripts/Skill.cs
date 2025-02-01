using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Skill", menuName = "Profession/Skill")]
public class Skill : ScriptableObject
{
    public string skillID;
    public string skillName;
    public string description;
    public List<Stat> grantedStats;
    public List<Ability> grantedAbilities;
}