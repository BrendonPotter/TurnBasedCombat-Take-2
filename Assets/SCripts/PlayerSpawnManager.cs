using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private const string PlayerPositionKey = "PlayerPosition";
    //[SerializeField] Vector3 startPosition;


    [SerializeField] WorldState FirstTimeEnter;

    //private void Awake()
    //{
    //    playerTransform = transform; // Assuming this script is attached to the player object
    //}

    private void Start()
    {
        if(FirstTimeEnter.firstTimeEnter == 0)
        {
            return;
        }

        if (FirstTimeEnter.firstTimeEnter == 1)
        {
            LoadPlayerPosition();
        }

    }

    //private void Update()
    //{
    //    SavePlayerPosition();

    //    //if (Input.GetKeyDown(KeyCode.P))
    //    //{
    //    //    Debug.Log("P has been pressed");
    //    //    transform.position = startPosition;
    //    //}
    //}

    //public void SavePlayerPosition()
    //{
    //    PlayerPrefs.SetFloat(PlayerPositionKey + "_x", transform.position.x);
    //    PlayerPrefs.SetFloat(PlayerPositionKey + "_y", transform.position.y);
    //    PlayerPrefs.SetFloat(PlayerPositionKey + "_z", transform.position.z);
    //    PlayerPrefs.Save();
    //    Debug.Log("Saved Position" + PlayerPositionKey);
    //}

    private void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey(PlayerPositionKey + "_x"))
        {
            Debug.LogWarning("LOADING PLAYER POSITION");
            float x = PlayerPrefs.GetFloat(PlayerPositionKey + "_x");
            float y = PlayerPrefs.GetFloat(PlayerPositionKey + "_y");
            float z = PlayerPrefs.GetFloat(PlayerPositionKey + "_z");
            Vector3 loadedPosition = new Vector3(x, y, z);

            
            SpawnPlayerAtPosition(loadedPosition);
            Debug.Log("Loaded Position:" + loadedPosition);
        }
    }

    private void SpawnPlayerAtPosition(Vector3 spawnPosition)
    {
        if (playerPrefab != null)
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

            spawnedPlayer.transform.parent = transform;
        }
        else
        {
            Debug.Log("Player Prefab not assigned");
        }
    }

}