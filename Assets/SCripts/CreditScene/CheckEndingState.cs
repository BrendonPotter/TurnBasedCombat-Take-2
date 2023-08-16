using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndingState : MonoBehaviour
{
    [SerializeField] WorldState worldState;

    [SerializeField] GameObject goodCanvas;
    [SerializeField] GameObject badCanvas;

    // Update is called once per frame
    void Update()
    {
        if(worldState.badEnding == true)
        {
            badCanvas.SetActive(true);
        }
        else if (worldState.goodEnding == true)
        {
            goodCanvas.SetActive(true);
        }
    }
}
