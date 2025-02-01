using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Profession", menuName = "Profession/ProfessionData")]
public class ProfessionData : ScriptableObject
{
    public string professionName;
    public List<SkillTreeData> skillTrees;
    // public List<List<Skill>> skillGrid;
}