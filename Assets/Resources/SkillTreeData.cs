using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "New Skill Tree", menuName = "Profession/SkillTreeData")]
public class SkillTreeData : ScriptableObject
{
    public List<Skill> skills;
}