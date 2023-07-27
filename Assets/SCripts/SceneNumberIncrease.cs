using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNumberIncrease : MonoBehaviour
{
    [SerializeField] WorldState state;

    // Start is called before the first frame update
    void Start()
    {
        state.sceneNumber = 1; 
    }

}
