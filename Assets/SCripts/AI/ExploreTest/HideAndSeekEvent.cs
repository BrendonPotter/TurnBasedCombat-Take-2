using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAndSeekEvent : MonoBehaviour
{
    [SerializeField] GameObject childrenGameObject;
    [SerializeField] GameObject childrenGameObject1;

    [SerializeField] float countDown = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            countDown -= Time.deltaTime;
            childrenGameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            countDown -= Time.deltaTime;
            if (countDown < 0)
            {
                childrenGameObject1.SetActive(true);
            }
        }
    }
}
