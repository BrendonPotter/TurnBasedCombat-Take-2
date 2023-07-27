using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] WorldState state;
    [SerializeField] string nextScene;

    private void Awake()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (state.successTask == false && state.agreeToPlay == true)
            {
                state.failTask = true;

            }

            SceneManager.LoadScene(nextScene); 
        }
    }
}
