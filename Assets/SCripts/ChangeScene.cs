using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] int sceneNumber;
    [SerializeField] SceneLoader sceneNameLoad;

    private void Awake()
    {
        sceneNameLoad= GetComponent<SceneLoader>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (sceneNumber == 2)
            {
                //Change scene to burning village
                sceneNameLoad.LoadSceneByName("ExploreTest");
            }
            if (sceneNumber == 3)
            {
                //Change scene to burndown village
                sceneNameLoad.LoadSceneByName("BurnDownVillage");
            }
        }
    }
}
