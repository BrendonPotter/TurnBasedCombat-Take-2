using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeEnterChange : MonoBehaviour
{
    [SerializeField] WorldState firstTimeEnterChange;

    // Start is called before the first frame update
    void Start()
    {
        firstTimeEnterChange.firstTimeEnter = 0; 
    }


}
