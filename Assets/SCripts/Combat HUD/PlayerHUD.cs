using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public Slider hpSlider;
    public int damage;

    public SaveSystem levelSaving;

    public void SetHUD()
    {
        hpSlider.maxValue = levelSaving.maxHPAmount;
        hpSlider.value = levelSaving.hpAmount;
    }


    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetLevelNum()
    {
        levelText.text = "" + (levelSaving._levelVar);
    }

    //Take damage
    public bool TakeDamage(int dmg)
    {
        levelSaving.hpAmount -= dmg;

        if (levelSaving.hpAmount <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
