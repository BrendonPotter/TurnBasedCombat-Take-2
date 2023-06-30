using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageLevelSetter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SaveSystem mageLevel;
    [SerializeField] SaveSystem hunterLevel;
    void Start() 
    {
        mageLevel._levelVar = hunterLevel._levelVar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
