using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtorialCanvas : MonoBehaviour
{
    [SerializeField] GameObject turtorialGameObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            turtorialGameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            turtorialGameObject.SetActive(false);
        }
    }
}
