using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HideAndSeekEvent : MonoBehaviour
{
    [SerializeField] WorldState state;

    [SerializeField] GameObject childrenGameObject;
    [SerializeField] GameObject childrenGameObject1;
    [SerializeField] GameObject HideAndSeekTaskBegin;


    [SerializeField] float countDown = 10f;
    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] TextMeshProUGUI completeTask;


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
                countDown = 0;
                countDownText.enabled= false;
            }
        }
    }

    private void Update()
    {
        int remainTimeInt = Mathf.CeilToInt(countDown);
        countDownText.text = remainTimeInt.ToString();
        if (state.agreeToPlay == true)
        {
            HideAndSeekTaskBegin.SetActive(true);
        }

        if(state.founded >= 2)
        {
            state.successTask = true;
            completeTask.enabled= true;
        }
    }
}
