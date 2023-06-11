using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    private Leveling levelSystem;

    private void Awake()
    {

    }

    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "Level: " + (levelNumber + 1);
    }

    public void SetLevelSystem(Leveling levelSystem)
    {
        this.levelSystem = levelSystem;

        SetLevelNumber(levelSystem.GetLevelNumber());
        levelSystem.OnLevelChanged += Leveling_OnLevelChanged;
    }

    private void Leveling_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystem.GetLevelNumber());
    }
}
