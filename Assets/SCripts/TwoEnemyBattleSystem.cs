using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleStateTwo { START, PLAYERTURN, ENEMYONETURN, ENEMYTWOTURN, WON, LOST }


public class TwoEnemyBattleSystem : MonoBehaviour
{

    public GameObject[] enemyPrefabs;

    public BattleStateTwo state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerPosition;
    public Transform enemyPositionOne;
    public Transform enemyPositionTwo;

    private Unit playerUnit;
    private Unit enemyUnit;
    private Unit enemyUnitTwo;

    public Text dialogText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public BattleHUD enemyTwoHUD;

    public string explorationScene;

    public GameObject enemyPosition1obj;
    public GameObject enemyPosition2obj;



    // Start is called before the first frame update
    void Start()
    {
        state = BattleStateTwo.START;
        StartCoroutine(SetupBattle());

    }

    public void Update()
    {
        CheckDeath();
        CheckDeath2();
    }

    IEnumerator SetupBattle()
    {

        int enemyChoice1 = Random.Range(0, enemyPrefabs.Length);
        int enemyChoice2 = Random.Range(0, enemyPrefabs.Length);

        GameObject playerGO = Instantiate(playerPrefab, playerPosition);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefabs[enemyChoice1], enemyPositionOne);
        enemyUnit = enemyGO.GetComponent<Unit>();

        
        GameObject enemyGOTwo = Instantiate(enemyPrefabs[enemyChoice2], enemyPositionTwo);
        enemyUnitTwo = enemyGOTwo.GetComponent<Unit>();    

    

        dialogText.text = "A wild " + enemyUnit.unitName + " has attacked";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        enemyTwoHUD.SetHUD(enemyUnitTwo);

        yield return new WaitForSeconds(2f);

        state = BattleStateTwo.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {   
        if(enemyPosition1obj == null && enemyPosition2obj == null)
        {
            state = BattleStateTwo.WON;
            EndBattle();
        }

        else if(enemyPosition1obj  == null)
        {
            enemyUnitTwo.TakeDamage(playerUnit.damage);

            enemyTwoHUD.SetHP(enemyUnitTwo.currentHP);
            dialogText.text = "the attack is successful!";

            yield return new WaitForSeconds(2f);

                state = BattleStateTwo.ENEMYTWOTURN;
                StartCoroutine(EnemyTurnTwo());
        }

        else
        {
            enemyUnit.TakeDamage(playerUnit.damage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogText.text = "the attack is successful!";

            yield return new WaitForSeconds(2f);

                state = BattleStateTwo.ENEMYONETURN;
                StartCoroutine(EnemyTurnOne());
        }

    //    //Damage the enemy
    //    bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

    //    enemyHUD.SetHP(enemyUnit.currentHP);
    //    dialogText.text = "the attack is successful!";

    //    yield return new WaitForSeconds(2f);

    //    //Check if enemy is dead
    //    if (isDead)
    //    {
    //        state = BattleStateTwo.WON;
    //        EndBattle();
    //    }
    //    else
    //    {
    //        state = BattleStateTwo.ENEMYONETURN;
    //        StartCoroutine(EnemyTurnOne());
    //    }
    //    //Change State Based on what happened
    }

    IEnumerator EnemyTurnOne()
    {
        if (enemyPosition1obj == null && enemyPosition2obj == null)
        {
            state = BattleStateTwo.WON;
            EndBattle();
        }

        else if (enemyPosition1obj == null)
        {
            state = BattleStateTwo.ENEMYTWOTURN;
            StartCoroutine(EnemyTurnTwo());
        }

        else
        {
            dialogText.text = enemyUnit.unitName + " 1 attacks";

            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(1f);

            Debug.Log("this is working");

            state = BattleStateTwo.ENEMYTWOTURN;
            StartCoroutine(EnemyTurnTwo());

        }

        //state = BattleStateTwo.ENEMYTWOTURN;
        //EnemyTurnTwo();

        //if (isDead)
        //{
        //    Debug.Log("IsDead is working");
        //    state = BattleStateTwo.LOST;
        //    EndBattle();
        //}
        //else
        //{
        //    Debug.Log("Change State is working");
        //    state = BattleStateTwo.ENEMYTWOTURN;
        //    StartCoroutine(EnemyTurnTwo());
        //    Debug.Log("We made it to the end");
        //}
    }

    IEnumerator EnemyTurnTwo()
    {
        if (enemyPosition1obj == null && enemyPosition2obj == null)
        {
            state = BattleStateTwo.WON;
            EndBattle();
        }
        else
        {

            Debug.Log("EnemyTwoTurn working");

            dialogText.text = enemyUnit.unitName + " 2 attacks";

            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(1f);

            state = BattleStateTwo.PLAYERTURN;
            PlayerTurn();

            //if (isDead)
            //{
            //    state = BattleStateTwo.LOST;
            //    EndBattle();
            //}
            //else
            //{
            //    state = BattleStateTwo.PLAYERTURN;
            //    PlayerTurn();
            //}

        }
    }


    void EndBattle()
    {
        if (state == BattleStateTwo.WON)
        {
            dialogText.text = "You won the Battle!";
            StartCoroutine(SceneSwitchDelay());
        }
        else if (state == BattleStateTwo.LOST)
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
        if (state != BattleStateTwo.PLAYERTURN)
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

    void CheckDeath()
    {
        if(enemyUnit.currentHP <= 0)
        {
            Destroy(enemyPosition1obj);
        }
    }

    void CheckDeath2()
    {
        if(enemyUnitTwo.currentHP <= 0)
        {
            Destroy(enemyPosition2obj);
        }
    }


}