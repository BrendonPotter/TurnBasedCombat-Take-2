using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leveling : MonoBehaviour
{
    public int experience;
    public int experienceThreshHold;

    [SerializeField] private int level = 1;
    public SaveSystem levelSave;
    public SaveSystem expThreshSave;


    public void Start()
    {
        experienceThreshHold = 100;
    }

    public void Update()
    {
        //Click to gain XP
        GainEXP();
        GainEvenMoreEXP();

        experienceThreshHold = expThreshSave._expThreshVar;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        if(experience >= expThreshSave._expThreshVar)
        {
            //If gain enough amount of XP, level up. Increase the XP gap by certain amount
            levelSave.Value++;
            experience -= expThreshSave._expThreshVar;
            IncreaseExperienceThreshHold();
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddExperience(50);
            Debug.Log("Gain EXP");
        }
    }

    public void GainEvenMoreEXP()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddExperience(500);
            Debug.Log("Gain even MORE EXP");
        }
    }
}
