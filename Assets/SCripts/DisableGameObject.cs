using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    [SerializeField] GameObject encounterManager;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            encounterManager.SetActive(true);
            Debug.Log("Enter Danger Zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            encounterManager.SetActive(false);
        }
    }
}
