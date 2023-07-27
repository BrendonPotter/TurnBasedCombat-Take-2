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
    [SerializeField] float disableTask = 5f;

    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] GameObject waitText;
    [SerializeField] GameObject completeTask;

    [SerializeField] bool completed;
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
            waitText.SetActive(true);
            if (countDown < 0)
            {
                childrenGameObject1.SetActive(true);
                waitText.SetActive(false);
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

        if(state.founded == 2)
        {
            state.successTask = true;
            completed= true;
        }

        if (completed == true)
        {
            completeTask.SetActive(true);
            disableTask -= Time.deltaTime;
            if(disableTask <= 0)
            {
                HideAndSeekTaskBegin.SetActive(false);
                disableTask = 0f;
            }
        }
    }
}
