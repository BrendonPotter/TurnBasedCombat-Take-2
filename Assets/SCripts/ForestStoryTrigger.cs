using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestStoryTrigger : MonoBehaviour
{
    public GameObject targetGameObject;
    [SerializeField] GameObject mage;
    [SerializeField] GameObject storyCollider;

    //Temporary disable random encounter battle after the player spot the bandit hideout
    [SerializeField] GameObject disableRandomEncounter;


    //Village Scene Manager
    [SerializeField] GameObject normalVillage;
    [SerializeField] GameObject burningVillage;

    private void Update()
    {
        if (targetGameObject.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                targetGameObject.SetActive(false);
                storyCollider.SetActive(false);
                this.enabled = false;

                if (mage != null)
                {
                    mage.SetActive(true);

                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the box collider
        if (other.CompareTag("Player"))
        {

            normalVillage.SetActive(false);
            burningVillage.SetActive(true);

            disableRandomEncounter.SetActive(false);

            // Enable the target game object
            if (targetGameObject != null)
            {
                targetGameObject.SetActive(true);
            }
        }
    }


}
