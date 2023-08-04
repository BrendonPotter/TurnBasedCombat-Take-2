using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtorialHUD : MonoBehaviour
{
    [SerializeField] GameObject turtorialCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            turtorialCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            turtorialCanvas.SetActive(false);
        }
    }
}
