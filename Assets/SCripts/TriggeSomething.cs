using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeSomething : MonoBehaviour
{
    [SerializeField] EncounterManager encounterSystem;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            encounterSystem.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        encounterSystem.enabled = false;
    }
}
