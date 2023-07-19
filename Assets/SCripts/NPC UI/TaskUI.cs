using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
    [SerializeField] WorldState state;

    [SerializeField] GameObject HideAndSeekTaskBegin;
    // Update is called once per frame
    void Update()
    {
        if(state.agreeToPlay == true)
        {
            HideAndSeekTaskBegin.SetActive(true);
        }
    }
}
