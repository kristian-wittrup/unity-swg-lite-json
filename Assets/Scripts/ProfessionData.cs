using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public string statType;
    public int value;
}

[Serializable]
public class Ability
{
    public string abilityID;
    public string abilityName;
    public string description;
}

[Serializable]
public class Skill
{
    public string skillID;
    public string skillName;
    public string description;
    public List<Stat> grantedStats;
    public List<Ability> grantedAbilities;
}

[Serializable]
public class ProfessionData
{
    public string professionName;
    // Using a List of Lists to represent the grid.
    public List<List<Skill>> skillGrid;
}
