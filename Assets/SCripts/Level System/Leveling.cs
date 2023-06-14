using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leveling : MonoBehaviour
{
    public event EventHandler OnLevelChanged;
    public int experience;
    public int experienceThreshHold;

    //UI for the EXP Gap
    [SerializeField] private int level;
    [SerializeField] private Image experienceBarImage;


    public void Start()
    {
        level = 0;
        experience = 0;
        experienceThreshHold = 100;
    }

    public void Update()
    {
        SetExperienceBarSize();
    }

    public void AddExperience(int amount)
    {
        experience+= amount;
        if(experience >= experienceThreshHold)
        {
            //If gain enough amount of XP, level up. Increase the XP gap by certain amount
            level++;
            experience -= experienceThreshHold;
            OnLevelChanged(this, EventArgs.Empty);
            IncreaseExperienceThreshHold();
        }
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public float IncreaseExperienceThreshHold()
    {
        return experienceThreshHold += 500;
    }

    //Constant update the EXP bar
    public void SetExperienceBarSize()
    {
        float fillAmount = (float)experience / (float)experienceThreshHold;
        experienceBarImage.fillAmount = fillAmount;
    }
}
