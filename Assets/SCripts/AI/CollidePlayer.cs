using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollidePlayer : MonoBehaviour
{
    [SerializeField] WorldState worldState;

    public int assignedNumber;

    private void CheckingNumber()
    {
        if (assignedNumber == 1)
        {
            worldState.contactEnemy1 = true;
            SceneManager.LoadScene("BattleSceneOneRat");
        }
        if (assignedNumber == 2)
        {
            worldState.contactEnemy2 = true;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //SceneManager.LoadScene("BattleSceneOneRat");
            Debug.Log("Number " + assignedNumber);
            CheckingNumber();
        }
    }
}
