using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailTaskUI : MonoBehaviour
{
    [SerializeField] WorldState state;
    [SerializeField] GameObject taskFail;
    [SerializeField] GameObject hideAdnSeekTask;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(state.agreeToPlay)
        {
            hideAdnSeekTask.SetActive(true);
            if (state.failTask == true)
            {
                taskFail.SetActive(true);
            }
        }
    }
}
