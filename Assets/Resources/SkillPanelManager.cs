using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SkillPanelManager : MonoBehaviour
{
    public GameObject skillPanel;
    public MonoBehaviour playerController;
    public Transform professionListContainer; // Container for profession buttons
    public Transform skillGridContainer; // Container for skill buttons
    public Button professionButtonPrefab; // Prefab for profession buttons
    public Button skillButtonPrefab; // Prefab for skill buttons
    public TMP_Text professionTitle;

    private bool isPanelActive = false;
    private List<ProfessionData> professions;

    void Start()
    {
        // Load all ProfessionData ScriptableObjects from the specified path
        professions = new List<ProfessionData>(Resources.LoadAll<ProfessionData>("ScriptableObjects"));
        Debug.Log("Loaded " + professions.Count + " professions.");

        // Create profession buttons
        foreach (var profession in professions)
        {
            Button button = Instantiate(professionButtonPrefab, professionListContainer);
            button.GetComponentInChildren<TMP_Text>().text = profession.professionName;
            button.onClick.AddListener(() => OnProfessionButtonClicked(profession));
            Debug.Log("Created button for profession: " + profession.professionName);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ToggleSkillPanel();
        }
    }

    void ToggleSkillPanel()
    {
        isPanelActive = !isPanelActive;
        skillPanel.SetActive(isPanelActive);
        playerController.enabled = !isPanelActive;

        if (isPanelActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnProfessionButtonClicked(ProfessionData profession)
    {
        professionTitle.text = profession.professionName;
        Debug.Log("Selected profession: " + profession.professionName);
        PopulateSkillGrid(profession.skillTrees);
    }

    void PopulateSkillGrid(List<SkillTreeData> skillTrees)
    {
        // Clear existing skill buttons
        foreach (Transform child in skillGridContainer)
        {
            Destroy(child.gameObject);
        }

        // Create new skill buttons
        foreach (var skillTree in skillTrees)
        {
            foreach (var skill in skillTree.skills)
            {
                Button button = Instantiate(skillButtonPrefab, skillGridContainer);
                button.GetComponentInChildren<TMP_Text>().text = skill.skillName;
                Debug.Log("Created button for skill: " + skill.skillName);
                // Add additional logic to handle skill button clicks if needed
            }
        }
    }
}