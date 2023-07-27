using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSpawnManager : MonoBehaviour
{

    private const string PlayerPositionKey = "PlayerPosition";
    [SerializeField] Vector3 startPosition;

    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = transform; // Assuming this script is attached to the player object
    }

    private void Update()
    {
        SavePlayerPosition();

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P has been pressed");
            transform.position = startPosition;
        }
    }

    private void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat(PlayerPositionKey + "_x", playerTransform.position.x);
        PlayerPrefs.SetFloat(PlayerPositionKey + "_y", playerTransform.position.y);
        PlayerPrefs.SetFloat(PlayerPositionKey + "_z", playerTransform.position.z);
        PlayerPrefs.Save();
    }

    private void LoadPlayerPosition()
    {
        if (PlayerPrefs.HasKey(PlayerPositionKey + "_x"))
        {
            float x = PlayerPrefs.GetFloat(PlayerPositionKey + "_x");
            float y = PlayerPrefs.GetFloat(PlayerPositionKey + "_y");
            float z = PlayerPrefs.GetFloat(PlayerPositionKey + "_z");
            playerTransform.position = new Vector3(x, y, z);
        }
    }

    private void Start()
    {
        LoadPlayerPosition();
    }
}