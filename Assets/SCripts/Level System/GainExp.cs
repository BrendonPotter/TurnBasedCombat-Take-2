using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GainExp : MonoBehaviour
{
    [SerializeField] private Leveling leveling;
    [SerializeField] private LevelUI levelWindow;

    // Update is called once per frame
    void Update()
    {
        levelWindow.SetLevelSystem(leveling);
        if (Input.GetKeyDown(KeyCode.G))
        {
            leveling.AddExperience(50);
            Debug.Log("Gain EXP");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            leveling.AddExperience(500);
            Debug.Log("Gain even MORE EXP");
        }
        else
        {
            Debug.Log("The Hell?");
        }
    }
}
