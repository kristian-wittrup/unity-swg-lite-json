using UnityEngine;

public class SkillPanelManager : MonoBehaviour
{
    // Assign your Skill Panel GameObject (the UI panel that pops up) in the Inspector.
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

    // Toggles the skill panel's visibility and disables/enables player controls.
    private void ToggleSkillPanel()
    {
        isPanelActive = !isPanelActive;

        // Activate or deactivate the skill panel.
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
