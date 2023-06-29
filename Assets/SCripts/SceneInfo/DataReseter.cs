using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReseter : MonoBehaviour
{
    [SerializeField] SaveSystem hunterStats;

    // Start is called before the first frame update
    void OnApplicationQuit()
    {
        ResetData();
    }

    // Update is called once per frame
    void ResetData()
    {
        hunterStats._levelVar = 1;
        hunterStats._earnExpAmount = 0;
        hunterStats._expThreshVar = 100;
        hunterStats.hpAmount = 100;
        hunterStats.maxHPAmount = 100;
        hunterStats.dealDamage = 10;
    }
}
