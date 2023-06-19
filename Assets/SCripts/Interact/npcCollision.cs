using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcCollision : MonoBehaviour
{
    [SerializeField] Canvas canvy;

    private void Start()
    {
        canvy.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           canvy.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canvy.enabled = false;
        }
    }
}
