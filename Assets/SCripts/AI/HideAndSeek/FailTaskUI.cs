using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailTaskUI : MonoBehaviour
{
    [SerializeField] WorldState state;
    [SerializeField] GameObject taskFail;
    [SerializeField] GameObject hideAdnSeekTask;

    [SerializeField] float disableTask = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(state.agreeToPlay && state.failTask)
        {
            hideAdnSeekTask.SetActive(true);
            taskFail.SetActive(true);
            disableTask -= Time.deltaTime;
            if (disableTask <= 0)
            {
                hideAdnSeekTask.SetActive(false);
                disableTask = 0f;
            }
        }
    }
}
