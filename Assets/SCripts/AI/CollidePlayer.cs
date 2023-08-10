using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollidePlayer : MonoBehaviour
{
    [SerializeField] WorldState worldState;
    [SerializeField] GameObject thisGameObject;

    public int assignedNumber;
    private void Awake()
    {
        if (worldState.contactEnemy1 == true && assignedNumber == 1)
        {
            Destroy(thisGameObject);
        }
        if (worldState.contactEnemy2 == true && assignedNumber == 2)
        {
            Destroy(thisGameObject);
        }
        if (worldState.contactEnemy3 == true && assignedNumber == 3)
        {
            Destroy(thisGameObject);
        }
        if (worldState.contactEnemy4 == true && assignedNumber == 4)
        {
            Destroy(thisGameObject);
        }
        if (worldState.contactEnemy5 == true && assignedNumber == 5)
        {
            Destroy(thisGameObject);
        }
    }

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
            //SceneManager.LoadScene("BattleSceneTwoRat");
            SceneManager.LoadScene("BattleSceneOneRat");
        }
        if (assignedNumber == 3)
        {
            worldState.contactEnemy3 = true;
            //SceneManager.LoadScene("BattleSceneTwoRat");
            SceneManager.LoadScene("BattleSceneOneRat");
        }
        if (assignedNumber == 4)
        {
            worldState.contactEnemy4 = true;
            //SceneManager.LoadScene("BattleSceneTwoRat");
            SceneManager.LoadScene("BattleSceneOneRat");
        }
        if (assignedNumber == 5)
        {
            worldState.contactEnemy5 = true;
            //SceneManager.LoadScene("BattleSceneTwoRat");
            SceneManager.LoadScene("BattleSceneOneRat");
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Number " + assignedNumber);
            CheckingNumber();
        }
    }
}
