using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    private Leveling level;

    //UI for the EXP Gap
    [SerializeField] private Image experienceBarImage;
    [SerializeField] private TextMeshProUGUI levelText;
    // Start is called before the first frame update
    void Awake()
    {
        level= GetComponent<Leveling>();
    }

    private void Update()
    {
        SetExperienceBarSize();
        SetLevelNumber();
    }

    //Constant update the EXP barslide in UI
    public void SetExperienceBarSize()
    {
        float fillAmount = (float)level.earnExpAmount._earnExpAmount / (float)level.expThreshSave._expThreshVar;
        experienceBarImage.fillAmount = fillAmount;
    }

    public void SetLevelNumber()
    {
        levelText.text = "Level: " + (level.levelSave.Value);
    }
}
