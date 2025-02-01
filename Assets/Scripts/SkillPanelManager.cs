using UnityEngine;

public class SkillPanelManager : MonoBehaviour
{
    // Assign Skill Panel GameObject in the Inspector.
    public GameObject skillPanel;
    
    // Assign the player's movement/camera controller script (or any component that handles player input)
    public MonoBehaviour playerController;
    
    // Tracks whether the skill panel is currently active.
    private bool isPanelActive = false;

    void Update()
    {
        // Listen for the "K" key press.
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleSkillPanel();
        }
    }

    // Toggles the skill panels visibility and disables/enabes player control.
    private void ToggleSkillPanel()
    {
        isPanelActive = !isPanelActive;

        // Activate or deactivate the skill panel based on the toggle state.
        if (skillPanel != null)
        {
            skillPanel.SetActive(isPanelActive);
        }

        // Disable player controller when the panel is active, enable it when not.
        if (playerController != null)
        {
            playerController.enabled = !isPanelActive;
        }

        // Manage cursor visibility and lock state for UI interaction.
        Cursor.visible = isPanelActive;
        Cursor.lockState = isPanelActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
