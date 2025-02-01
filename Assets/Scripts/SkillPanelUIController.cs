using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelUIController : MonoBehaviour
{
    // The parent container in your SkillPanel (e.g., a Grid Layout Group) where skill buttons will be added.
    public Transform skillGridParent;
    
    // The prefab for an individual skill button.
    public GameObject skillButtonPrefab;
    
    // Reference to the ProfessionManager that has loaded your Medic JSON data.
    public ProfessionManager professionManager;

    // The loaded Medic profession data.
    private ProfessionData medicProfession;

    void Start()
    {
        // Retrieve the Medic data from your ProfessionManager.
        medicProfession = professionManager.GetMedicProfession();
        if (medicProfession == null)
        {
            Debug.LogError("Medic profession data is null!");
            return;
        }
        PopulateSkillGrid();
    }

    // This method creates a button for each skill in the 4x4 grid.
    void PopulateSkillGrid()
    {
        // Clear any existing buttons.
        foreach (Transform child in skillGridParent)
        {
            Destroy(child.gameObject);
        }
            
        // Loop through each column (the JSON structure uses columns as the outer array)
        for (int col = 0; col < medicProfession.skillGrid.Count; col++)
        {
            List<Skill> columnSkills = medicProfession.skillGrid[col];
            // Loop through each skill in the column (each column should have 4 skills).
            for (int row = 0; row < columnSkills.Count; row++)
            {
                Skill skill = columnSkills[row];
                // Instantiate a new skill button as a child of the skillGridParent.
                GameObject newButton = Instantiate(skillButtonPrefab, skillGridParent);
                // Get the SkillButtonController component to initialize the button.
                SkillButtonController buttonController = newButton.GetComponent<SkillButtonController>();
                if (buttonController != null)
                {
                    Debug.Log("Setting up skill: " + skill.skillName);
                    //buttonController.Setup(skill); // Gives console error when active - not sure why. Maybe because JSDOn is not parsed correctly?
                }
            }
        }
    }
}
