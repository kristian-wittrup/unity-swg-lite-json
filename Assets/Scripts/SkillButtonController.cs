using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButtonController : MonoBehaviour
{
    // The TMP_Text that displays the skill name.
    public TMP_Text skillNameText;
    
    // The Button component.
    public Button button;

    // Setup method that assigns the given text (profession name) to the TMP_Text.
    public void Setup(string text)
    {
        if (skillNameText != null)
        {
            skillNameText.text = text;
        }
        else
        {
            Debug.LogError("TMP_Text component not assigned in SkillButtonController.");
        }
    }
}
