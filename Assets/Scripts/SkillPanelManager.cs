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
        // Load all ProfessionData ScriptableObjects
        professions = new List<ProfessionData>(Resources.LoadAll<ProfessionData>(""));

        // Create profession buttons
        foreach (var profession in professions)
        {
            Button button = Instantiate(professionButtonPrefab, professionListContainer);
            button.GetComponentInChildren<TMP_Text>().text = profession.professionName;
            button.onClick.AddListener(() => OnProfessionButtonClicked(profession));
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
        PopulateSkillGrid(profession.skillGrid);
    }

    void PopulateSkillGrid(List<List<Skill>> skillGrid)
    {
        // Clear existing skill buttons
        foreach (Transform child in skillGridContainer)
        {
            Destroy(child.gameObject);
        }

        // Create new skill buttons
        for (int i = 0; i < skillGrid.Count; i++)
        {
            for (int j = 0; j < skillGrid[i].Count; j++)
            {
                Skill skill = skillGrid[i][j];
                Button button = Instantiate(skillButtonPrefab, skillGridContainer);
                button.GetComponentInChildren<TMP_Text>().text = skill.skillName;
                // Add additional logic to handle skill button clicks if needed
            }
        }
    }
}