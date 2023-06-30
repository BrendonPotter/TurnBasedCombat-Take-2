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

    [SerializeField] bool isLevelUp;
    [SerializeField] GameObject levelUpCanvas;

    //Unlock Level
    [SerializeField] GameObject meteorUnlock;
    [SerializeField] GameObject trippleArrowUnlock;
    [SerializeField] GameObject lightingStrikeUnlock;
    [SerializeField] GameObject volleyUnlock;


    public void Start()
    {
        meteorUnlock.SetActive(false);
        trippleArrowUnlock.SetActive(false);
    }

    public void Update()
    {
        //Click to gain XP
        GainEvenMoreEXP();
        CheckLevel();
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
            isLevelUp= true;
            if(isLevelUp)
            {
                levelSave.hpAmount += 100;
                levelSave.maxHPAmount += 100;
                levelSave.dealDamage += 20;
                levelUpCanvas.SetActive(true);
                ToggleBoolean();
            }
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

    public void GainDoubleEXP()
    {
        AddExperience(200);
        Debug.Log("Gain 200 EXP");
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

    IEnumerator ToggleBoolean()
    {
        yield return new WaitForSeconds(0.25f); // Wait for 2 seconds
        isLevelUp = false;
        levelUpCanvas.SetActive(false);
    }

    private void CheckLevel()
    {
        //Hunter abilities
        if (levelSave._levelVar >= 5)
        {
            volleyUnlock.SetActive(true);
        }
        if (levelSave._levelVar >= 10)
        {
            //Change to tripple arrow
            trippleArrowUnlock.SetActive(true);
        }
        //Mage abilities
        if(levelSave._levelVar >= 5)
        {
            meteorUnlock.SetActive(true);
        }
        if(levelSave._levelVar >= 10)
        {
            lightingStrikeUnlock.SetActive(true);
        }
    }
}
