using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSaver : MonoBehaviour
{
    private const string PlayerPositionKey = "PlayerPosition";

    // Update is called once per frame
    void Update()
    {
        SavePosition();
    }

    public void SavePosition()
    {
        PlayerPrefs.SetFloat(PlayerPositionKey + "_x", transform.position.x);
        PlayerPrefs.SetFloat(PlayerPositionKey + "_y", transform.position.y);
        PlayerPrefs.SetFloat(PlayerPositionKey + "_z", transform.position.z);
        PlayerPrefs.Save();
        Debug.Log("Saved Position" + PlayerPositionKey);
    }
}
