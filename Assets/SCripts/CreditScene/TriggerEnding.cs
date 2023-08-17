using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnding : MonoBehaviour
{
    public bool badEnding;
    [SerializeField] WorldState endingState;

    private void Update()
    {
        if(badEnding == true)
        {
            endingState.badEnding = true;
            endingState.goodEnding = false;
        }
        else
        {
            endingState.goodEnding = true;
            endingState.badEnding = false;
        }
    }
}
