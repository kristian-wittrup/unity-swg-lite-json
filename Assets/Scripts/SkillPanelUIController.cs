using UnityEngine;
using UnityEngine.UI;

public class SkillPanelUIController : MonoBehaviour
{
    // The parent container (with a Grid Layout Group) where the buttons will be added.
    public Transform skillGridParent;
    
    // The prefab for an individual skill button.
    public GameObject skillButtonPrefab;
    
    // Reference to the ProfessionManager that loaded the JSON.
    public ProfessionManager professionManager;

    // The loaded profession data.
    private ProfessionData medicProfession;

    void Start()
    {
        // Retrieve the profession data from the manager.
        medicProfession = professionManager.GetMedicProfession();
        if (medicProfession == null)
        {
            Debug.LogError("Medic profession data is null!");
            return;
        }
        PopulateSkillGrid();
    }

    void PopulateSkillGrid()
    {
        // Clear any existing buttons.
        foreach (Transform child in skillGridParent)
        {
            Destroy(child.gameObject);
        }
        
        // Get the profession name from the JSON.
        string professionName = medicProfession.professionName;
        
        // Define the grid dimensions.
        int rows = 4;
        int cols = 4;
        
        // Loop through and instantiate 16 buttons.
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Instantiate the button as a child of the grid container.
                GameObject newButton = Instantiate(skillButtonPrefab, skillGridParent);
                
                // Get the SkillButtonController component and set the text.
                SkillButtonController buttonController = newButton.GetComponent<SkillButtonController>();
                if (buttonController != null)
                {
                    buttonController.Setup(professionName);
                }
                else
                {
                    
                    Debug.LogError("SkillButtonController not found on the instantiated prefab.");
                }
            }
        }
    }
}
