using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    public string myScene;
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

        if(ecounterCheck < 3)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            encounterText.SetActive(true);

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene(myScene);
        }
    }
}
