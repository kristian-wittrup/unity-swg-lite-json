using UnityEngine;
using System.Collections.Generic;

public class ProfessionManager : MonoBehaviour
{
    public TextAsset medicSkillDataJson;
    private ProfessionData medicProfession;

    void Awake()
    {
        // If the JSON file isn't manually assigned, try to load it from the Resources folder.
        if (medicSkillDataJson == null)
        {
            medicSkillDataJson = Resources.Load<TextAsset>("MedicSkillData");
        }

        if (medicSkillDataJson != null)
        {
            // Parse the JSON into our ProfessionData object.
            medicProfession = JsonUtility.FromJson<ProfessionData>(medicSkillDataJson.text);
            
            if (medicProfession == null)
            {
                Debug.LogError("medicProfession is null!");
            }
            else
            {
                Debug.Log("Loaded Profession Name: " + medicProfession.professionName);
            }

            if (medicProfession != null && medicProfession.skillGrid != null)
            {
                Debug.LogError("skillGrid is null!");
            }
            else if (medicProfession != null && medicProfession.skillGrid != null)
            {
                Debug.Log("SkillGrid column count: " + medicProfession.skillGrid.Count);

                // Log the number of skills in each column.
                for (int i = 0; i < medicProfession.skillGrid.Count; i++)
                {
                    Debug.Log("Column " + i + " has " + medicProfession.skillGrid[i].Count + " skills.");
                }
            }
        }
        else
        {
            Debug.LogError("MedicSkillData.json not found in Resources!");
        }
    }

    public ProfessionData GetMedicProfession()
    {
        return medicProfession;
    }
}
