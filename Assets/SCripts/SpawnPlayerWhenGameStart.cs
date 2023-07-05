using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerWhenGameStart : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform playerSpawnPoint;

    private void Start()
    {
        player.position = playerSpawnPoint.position;
    }
}
