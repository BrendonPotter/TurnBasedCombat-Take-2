using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST }


public class BattleSystem : MonoBehaviour
{

    public GameObject[] enemyPrefabs;

    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerPosition;
    public Transform enemyPosition;

    public Text dialogText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public string explorationScene;


    private Unit playerUnit;
    private Unit enemyUnit;


    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {

        int enemyChoice1 = Random.Range(0, enemyPrefabs.Length);

        GameObject playerGO = Instantiate(playerPrefab, playerPosition);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefabs[enemyChoice1], enemyPosition);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogText.text = "A wild " + enemyUnit.unitName + " has attacked";

        playerHUD.SetHUD(playerUnit);
        playerHUD.SetLevelNum();
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        //Damage the enemy
        bool isDead =  enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogText.text = "the attack is successful!";

        yield return new WaitForSeconds(2f);

        //Check if enemy is dead
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        //Change State Based on what happened
    }

    IEnumerator EnemyTurn()
    {
        dialogText.text = enemyUnit.unitName + "attacks";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }


    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogText.text = "You won the Battle!";
            StartCoroutine(SceneSwitchDelay());
        }
        else if(state == BattleState.LOST)
        {
            dialogText.text = "you have died";
            StartCoroutine(SceneSwitchDelay());
        }
    }

    void PlayerTurn()
    {
        dialogText.text = "Choose an action";
    }

    

    public void OnAttackButton()
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    //public void OnDefendButton()
    //{
    //    if (state != BattleState.PLAYERTURN)
    //    {
    //        return;
    //    }

    //    StartCoroutine(PlayerDefends());
    //}

    IEnumerator SceneSwitchDelay()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(explorationScene);
    }

}


