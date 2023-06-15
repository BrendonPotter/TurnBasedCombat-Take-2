using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterManager : MonoBehaviour
{
    private Vector3 previousPosition;
    private bool isMoving;

    public string battleScene1;
    public string battleScene2;

    public GameObject encounterText;

    private void Start()
    {
        // Initialize the previous position to the player's starting position
        previousPosition = transform.position;
    }

    private void Update()
    {
        CheckMoving();

        if (isMoving == true)
        {
            StartCoroutine(EncounterCheck());
        }
    }
    IEnumerator EncounterCheck()
    {
        yield return new WaitForSeconds(2);

        int ecounterCheck = Random.Range(1, 10);

        if(ecounterCheck < 2)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            encounterText.SetActive(true);

            yield return new WaitForSeconds(2);

            SceneManager.LoadScene(battleScene1);
        }

        //if(ecounterCheck >= 3 && ecounterCheck <= 4)
        //{
        //    GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        //    encounterText.SetActive(true);

        //    yield return new WaitForSeconds(2);

        //    SceneManager.LoadScene(battleScene2);
        //}
    }

    void CheckMoving()
    {
        //Check if the player's position has changed since the last frame
        if (transform.position != previousPosition)
        {
            Debug.Log("is Moving");
            isMoving = true;
        }
        else
        {
            Debug.Log("Is not moving");
            isMoving = false;
        }

        //Update the previous position to the current position for the next frame

       previousPosition = transform.position;
    }
}
