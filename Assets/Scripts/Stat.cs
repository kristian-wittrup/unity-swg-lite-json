using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "Profession/Stat")]
public class Stat : ScriptableObject
{
    public string statType;
    public int value;
}