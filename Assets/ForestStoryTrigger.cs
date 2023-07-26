using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestStoryTrigger : MonoBehaviour
{
    public GameObject targetGameObject;
    public MonoBehaviour targetScript;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the box collider
        if (other.CompareTag("Player"))
        {
            // Disable the target script
            if (targetScript != null)
                targetScript.enabled = false;

            // Enable the target game object
            if (targetGameObject != null)
                targetGameObject.SetActive(true);
        }
    }
}
