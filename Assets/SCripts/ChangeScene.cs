using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] int sceneNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (sceneNumber == 2)
        {
            //Change scene to burning village
        }
        if (sceneNumber == 3)
        {
            //Change scene to burndown village
        }
    }
}
