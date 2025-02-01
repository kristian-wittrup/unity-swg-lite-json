using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Profession/Ability")]
public class Ability : ScriptableObject
{
    public string abilityID;
    public string abilityName;
    public string description;
}