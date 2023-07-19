using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReseter : MonoBehaviour
{
    [SerializeField] SaveSystem hunterStats;
    [SerializeField] WorldState worldState;
    [SerializeField] Inventory inventory;

    void OnApplicationQuit()
    {
        ResetData();
        ResetWorldState();
        RemoveAllItem();
    }

    void ResetData()
    {
        hunterStats._levelVar = 1;
        hunterStats._earnExpAmount = 0;
        hunterStats._expThreshVar = 100;
        hunterStats.hpAmount = 100;
        hunterStats.maxHPAmount = 100;
        hunterStats.dealDamage = 10;
    }

    void RemoveAllItem()
    {
        inventory.Items.Clear();
    }
    void ResetWorldState()
    {
        worldState.obtainKey = false;
        worldState.agreeToPlay= false;
        worldState.successTask = false;
        worldState.failTask = false;
        worldState.founded = 0;

        worldState.contactEnemy1 = false;
        worldState.winVsEnemy1 = false;
        worldState.contactEnemy2 = false;
        worldState.winVsEnemy2 = false;

    }
}
