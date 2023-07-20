using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectWithDelay : MonoBehaviour
{
    [SerializeField] float delay = 10f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            delay -= Time.deltaTime;
            if(delay < 0)
            {
                delay = 0;
                Destroy(gameObject);
            }
        }
    }
}
