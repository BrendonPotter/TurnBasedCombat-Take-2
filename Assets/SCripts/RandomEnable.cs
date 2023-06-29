using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnable : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objectsToEnable;

    private void Start()
    {
        if (objectsToEnable != null && objectsToEnable.Length > 0)
        {
            int randomIndex = Random.Range(0, objectsToEnable.Length);
            GameObject selectedObject = objectsToEnable[randomIndex];
            selectedObject.SetActive(true);
        }
        else
        {
            Debug.LogError("No objects assigned to objectsToEnable array!");
        }
    }
}
