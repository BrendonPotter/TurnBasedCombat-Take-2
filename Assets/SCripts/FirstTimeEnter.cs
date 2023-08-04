using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeEnter : MonoBehaviour
{

    public GameObject playerPrefab;
    public Vector3 spawnPosition;

    [SerializeField] WorldState firstTimeEntering;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        yield return null;

        // AG: This script will run the code below the FIRST TIME the Forest Level is entered.
        if (firstTimeEntering.firstTimeEnter == 0)
        {
            ResetPlayerPrefs();
            GameObject newPlayerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

            firstTimeEntering.firstTimeEnter = 1;
            Debug.Log("It added one to firstTimeEnter");

            // AG: Save the newPlayerInstance position to the PlayerPrefs
            newPlayerInstance.GetComponent<PlayerPrefsSaver>().SavePosition();
        }

    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs have been reset.");
    }

}
