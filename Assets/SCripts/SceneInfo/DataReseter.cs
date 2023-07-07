using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataReseter : MonoBehaviour
{
    [SerializeField] SaveSystem hunterStats;
    [SerializeField] WorldState worldState;

    //[SerializeField] Transform playerPosition;
    //[SerializeField] Transform spawnPosition;



    // Start is called before the first frame update
    void OnApplicationQuit()
    {
        ResetData();
        //ResetMovemtent();
        ResetWorldState();
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

    //void ResetMovemtent()
    //{
    //    playerPosition.position = transform.position;
    //}

    void ResetWorldState()
    {
        worldState.obtainKey = false;

        worldState.contactEnemy1 = false;
        worldState.winVsEnemy1 = false;
        worldState.contactEnemy2 = false;
        worldState.winVsEnemy2 = false;
    }
}
