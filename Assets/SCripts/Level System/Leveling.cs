using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leveling : MonoBehaviour
{
    public SaveSystem levelSave;
    public SaveSystem expThreshSave;
    public SaveSystem earnExpAmount;


    public void Start()
    {
        
    }

    public void Update()
    {
        //Click to gain XP
        GainEvenMoreEXP();
    }

    public void AddExperience(int amount)
    {
        earnExpAmount._earnExpAmount += amount;
        if(earnExpAmount._earnExpAmount >= expThreshSave._expThreshVar)
        {
            //If gain enough amount of XP, level up. Increase the XP gap by certain amount
            levelSave.Value++;
            earnExpAmount._earnExpAmount -= expThreshSave._expThreshVar;
            IncreaseExperienceThreshHold();
            Debug.Log("Level UP!!!!");
            return;
        }
    }

    public void IncreaseExperienceThreshHold()
    {
        expThreshSave._expThreshVar += 100;
    }

    //Earn XP
    public void GainEXP()
    {
        AddExperience(100);
        Debug.Log("Gain 100 EXP");
    }


    //This method use for experiment only...
    public void GainEvenMoreEXP()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddExperience(100);
            Debug.Log("Gain 100 EXP CHEATS");
        }
    }
}
