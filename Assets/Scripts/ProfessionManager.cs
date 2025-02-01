using UnityEngine;

public class ProfessionManager : MonoBehaviour
{
    // Option 1: Assign the JSON file via the Inspector.
    public TextAsset medicSkillDataJson;

    // This will hold our loaded Medic profession data.
    private ProfessionData medicProfession;

    void Awake()
    {
        // If not assigned in the Inspector, try to load it from Resources.
        if (medicSkillDataJson == null)
        {
            medicSkillDataJson = Resources.Load<TextAsset>("MedicSkillData");
        }

        if (medicSkillDataJson != null)
        {
            // Parse the JSON into our ProfessionData class.
            medicProfession = JsonUtility.FromJson<ProfessionData>(medicSkillDataJson.text);
            Debug.Log("Loaded Profession: " + medicProfession.professionName);
        }
        else
        {
            Debug.LogError("MedicSkillData.json not found in Resources!");
        }
    }

    // Public method to access the Medic Profession data.
    public ProfessionData GetMedicProfession()
    {
        return medicProfession;
    }
}
