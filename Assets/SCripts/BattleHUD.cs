using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;

    public SaveSystem levelSaving;

    void Awake()
    {
        levelSaving = GetComponent<SaveSystem>();
    }

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }


    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetLevelNum()
    {
        levelText.text = "Level: " + (levelSaving.Value);
    }

}
