using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckCheckpoint : MonoBehaviour
{

    public SaveSystem checkpoint;

    //Player Spawn Points
    [SerializeField] GameObject hunterSpawnSingle;
    [SerializeField] GameObject hunterSpawnDuo;
    [SerializeField] GameObject mageSpawnDuo;
    

    // Start is called before the first frame update
    public void CheckPlayerNumber()
    {
        if (checkpoint.checkPoint == 1)
        {
            hunterSpawnDuo.SetActive(true);
            mageSpawnDuo.SetActive(true);        
            Debug.Log("It checked");

        }
        else
        {
            hunterSpawnSingle.SetActive(true);
            Debug.Log("It didnt check");
        }
    }


}
