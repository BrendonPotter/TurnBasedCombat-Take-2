using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskUI : MonoBehaviour
{
    [SerializeField] WorldState state;

    [SerializeField] GameObject taskBegin;
    // Update is called once per frame
    void Update()
    {
        //hide and seek task
        if(state.agreeToPlay == true)
        {
            taskBegin.SetActive(true);
        }
    }
}
