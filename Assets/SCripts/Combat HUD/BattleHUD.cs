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

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = (unit.currentHP/unit.maxHP) * 100;
        levelText.text = unit.unitLevel.ToString();
    }

    public void SetHP(int currentHP)
    {
        // AG NOTE: This won't do anything now! We want to call SetHP(Unit unit) instead. :)
        hpSlider.value = currentHP;
    }

    //public void SetHP(Unit unit)
    //{
    //    hpSlider.value = (unit.currentHP / unit.maxHP) * 100;
    //}
}
