using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    public string battleScene1;
    public string battleScene2;

    public GameObject encounterText; 

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(EncounterCheck());
    }


    IEnumerator EncounterCheck()
    {
        yield return new WaitForSeconds(5);

        int ecounterCheck = Random.Range(1, 10);

        if(ecounterCheck < 2)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            encounterText.SetActive(true);

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene(battleScene1);
        }

        if(ecounterCheck >= 3 && ecounterCheck <= 4)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            encounterText.SetActive(true);

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene(battleScene2);
        }
    }
}
