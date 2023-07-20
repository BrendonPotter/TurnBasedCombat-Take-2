using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTheEvent : MonoBehaviour
{
    [SerializeField] GameObject enableTheEvent;
    [SerializeField] WorldState checkingWorldState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkingWorldState.agreeToPlay)
        {
            enableTheEvent.SetActive(true);
        }
    }
}
