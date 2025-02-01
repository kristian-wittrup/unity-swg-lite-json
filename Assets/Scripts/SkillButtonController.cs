using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButtonController : MonoBehaviour
{
    // UI elements in the button prefab.
    public TMP_Text skillNameText;
    public Button button;

    // Holds the data for the skill this button represents.
    private Skill skillData;

    // Call this method to initialize the button with a Skill.
    public void Setup(Skill skill)
    {
        skillData = skill;
        if (skillNameText != null)
        {
            skillNameText.text = skill.skillName;
        }
        // Remove any previous listeners and add our own.
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnButtonClicked);
    }

    // Called when the button is clicked.
    void OnButtonClicked()
    {
        Debug.Log("Skill selected: " + skillData.skillName);
        // You can add additional logic here to display details, unlock the skill, etc.
    }
}
