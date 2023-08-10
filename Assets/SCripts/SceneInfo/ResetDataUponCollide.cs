using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDataUponCollide : MonoBehaviour
{
    [SerializeField] WorldState resetState;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            resetState.firstTimeEnter = 0;
        }
    }
}
