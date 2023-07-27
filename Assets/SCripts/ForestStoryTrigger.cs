using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestStoryTrigger : MonoBehaviour
{
    public GameObject targetGameObject;
    public MonoBehaviour targetScript;
    [SerializeField] GameObject mage;
    [SerializeField] GameObject storyCollider;

    private void Update()
    {
        if (targetGameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                targetGameObject.SetActive(false);
                targetScript.enabled = true;
                storyCollider.SetActive(false);

            }
        }
    }

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

            if(mage != null)
            {
                mage.SetActive(true);

            }
        }
    }


}
