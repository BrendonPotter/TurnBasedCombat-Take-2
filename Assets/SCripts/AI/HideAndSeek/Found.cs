using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Found : MonoBehaviour
{
    [SerializeField] bool isFounded;
    [SerializeField] WorldState isFound;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isFounded)
            {
                isFounded = true;
                isFound.founded++;
            }
        }
    }
}
