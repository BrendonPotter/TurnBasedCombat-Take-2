using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum BattleStateTwo { START, PLAYERTURN, ENEMYONETURN, ENEMYTWOTURN, WON, LOST }


public class FailTwoEnemyBattleSystem : MonoBehaviour
{

    public GameObject[] enemyPrefabs;

    public BattleStateTwo state;


    public GameObject fireballPrefab;
    public GameObject arrowPrefab;
    public GameObject meteorPrefab;
    public GameObject cloudPrefab;
    public GameObject lightningStrikePrefab;
    public GameObject healingParticleSystemPrefab;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerPosition;
    public Transform enemyPositionOne;
    public Transform enemyPositionTwo;

    private Unit enemyUnit;
    private Unit enemyUnitTwo;

    public Text dialogText;

    public PlayerHUD playerHUD;
    public BattleHUD enemyHUD;
    public BattleHUD enemyTwoHUD;

    public string explorationScene;

    public GameObject enemyPosition1obj;
    public GameObject enemyPosition2obj;


    //Other script reference
    public Leveling earnEXP;
    public SaveSystem playerUnit;

    public GameObject attackFleePanel;
    public GameObject abilityChoicePanel;

    public Button meteorShower;
    public Button lightingStrike;
    public Button fireBall;
    public Button tripleArrow;
    public Button healing;

    public TextMeshProUGUI lightingStrikeCDText;
    public TextMeshProUGUI meteorShowerCDText;
    public TextMeshProUGUI trippleArrowCDText;
    public TextMeshProUGUI fireBallCDText;
    public TextMeshProUGUI healingCDText;

    public int lightingStrikeCD;
    public int meteorShowerCD;
    public int trippleArrowCD;
    public int fireBallCD;
    public int healingCD;


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

        GameObject enemyGO = Instantiate(enemyPrefabs[enemyChoice1], enemyPositionOne);
        enemyUnit = enemyGO.GetComponent<Unit>();

        
        GameObject enemyGOTwo = Instantiate(enemyPrefabs[enemyChoice2], enemyPositionTwo);
        enemyUnitTwo = enemyGOTwo.GetComponent<Unit>();    

    

        dialogText.text = "Two enemy has appear!!!";

        playerHUD.SetHUD();
        playerHUD.SetLevelNum();

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
            enemyUnitTwo.TakeDamage(enemyUnitTwo.damage);

            enemyTwoHUD.SetHP(enemyUnitTwo.currentHP);
            dialogText.text = "the attack is successful!";

            yield return new WaitForSeconds(2f);

            state = BattleStateTwo.ENEMYTWOTURN;
            StartCoroutine(EnemyTurnTwo());
        }

        else
        {
            enemyUnit.TakeDamage(enemyUnitTwo.damage);

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

    IEnumerator PlayerAttackFireball()
    {

        if (enemyPosition1obj == null && enemyPosition2obj == null)
        {
            state = BattleStateTwo.WON;
            EndBattle();
        }
        
        else if(enemyPosition1obj == null)
        {
            // Spawn a fireball prefab
            GameObject fireballGO = Instantiate(fireballPrefab, playerPosition.position, Quaternion.identity);
            Fireball fireball = fireballGO.GetComponent<Fireball>();
            fireball.SetTarget(enemyPositionTwo.position);

            // Wait for the fireball to reach the enemy
            yield return new WaitForSeconds(fireball.travelTime);

            // Damage the enemy
           enemyUnitTwo.TakeDamage(enemyUnitTwo.damage);

            enemyTwoHUD.SetHP(enemyUnit.currentHP);
            dialogText.text = "You cast a fireball!";

            // Destroy the fireball
            Destroy(fireballGO);

            yield return new WaitForSeconds(2f);

            state = BattleStateTwo.ENEMYTWOTURN;
            StartCoroutine(EnemyTurnTwo());

        }
        else
        {
            // Spawn a fireball prefab
            GameObject fireballGO = Instantiate(fireballPrefab, playerPosition.position, Quaternion.identity);
            Fireball fireball = fireballGO.GetComponent<Fireball>();
            fireball.SetTarget(enemyPositionOne.position);

            // Wait for the fireball to reach the enemy
            yield return new WaitForSeconds(fireball.travelTime);

            enemyUnit.TakeDamage(playerUnit.dealDamage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogText.text = "You cast a fireball!";

            // Destroy the fireball
            Destroy(fireballGO);

            yield return new WaitForSeconds(2f);

            state = BattleStateTwo.ENEMYONETURN;
            StartCoroutine(EnemyTurnOne());
        }
        //// Spawn a fireball prefab
        //GameObject fireballGO = Instantiate(fireballPrefab, playerPosition.position, Quaternion.identity);
        //Fireball fireball = fireballGO.GetComponent<Fireball>();
        //fireball.SetTarget(enemyPositionOne.position);

        //// Wait for the fireball to reach the enemy
        //yield return new WaitForSeconds(fireball.travelTime);

        //// Damage the enemy
        //bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        //enemyHUD.SetHP(enemyUnit.currentHP);
        //dialogText.text = "You cast a fireball!";

        //// Destroy the fireball
        //Destroy(fireballGO);

        //yield return new WaitForSeconds(2f);

        //Check if enemy is dead
        //if (isDead)
        //    {
        //        state = BattleStateTwo.WON;
        //        EndBattle();
        //    }
        //    else
        //    {
        //        state = BattleStateTwo.ENEMYONETURN;
        //        StartCoroutine(EnemyTurnOne());
        //    }
    }

    IEnumerator PlayerAttackTripleArrow()
    {
        if (enemyPosition1obj == null && enemyPosition2obj == null)
        {
            state = BattleStateTwo.WON;
            EndBattle();
        }

        else if(enemyPosition1obj == null)
        {
            for (int i = 0; i < 3; i++)
            {
                // Spawn an arrow prefab
                GameObject arrowGO = Instantiate(arrowPrefab, playerPosition.position, Quaternion.identity);
                Arrow arrow = arrowGO.GetComponent<Arrow>();
                arrow.SetTarget(enemyPositionTwo.position);

                // Wait for the arrow to reach the enemy
                yield return new WaitForSeconds(arrow.travelTime);

                // Damage the enemy
                enemyUnitTwo.TakeDamage(enemyUnitTwo.damage);

                enemyTwoHUD.SetHP(enemyUnit.currentHP);
                dialogText.text = "You shot an arrow!";

                // Destroy the arrow
                Destroy(arrowGO);

                yield return new WaitForSeconds(0.5f);
            }

            state = BattleStateTwo.ENEMYTWOTURN;
            StartCoroutine(EnemyTurnTwo());
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                // Spawn an arrow prefab
                GameObject arrowGO = Instantiate(arrowPrefab, playerPosition.position, Quaternion.identity);
                Arrow arrow = arrowGO.GetComponent<Arrow>();
                arrow.SetTarget(enemyPositionOne.position);

                // Wait for the arrow to reach the enemy
                yield return new WaitForSeconds(arrow.travelTime);

                // Damage the enemy
                bool isDead = enemyUnit.TakeDamage(enemyUnitTwo.damage);

                enemyHUD.SetHP(enemyUnit.currentHP);
                dialogText.text = "You shot an arrow!";

                // Destroy the arrow
                Destroy(arrowGO);

                yield return new WaitForSeconds(0.5f);
            }

            state = BattleStateTwo.ENEMYONETURN;
            StartCoroutine(EnemyTurnOne());
        }
        //for (int i = 0; i < 3; i++)
        //{
        //    // Spawn an arrow prefab
        //    GameObject arrowGO = Instantiate(arrowPrefab, playerPosition.position, Quaternion.identity);
        //    Arrow arrow = arrowGO.GetComponent<Arrow>();
        //    arrow.SetTarget(enemyPositionOne.position);

        //    // Wait for the arrow to reach the enemy
        //    yield return new WaitForSeconds(arrow.travelTime);

        //    // Damage the enemy
        //    bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        //    enemyHUD.SetHP(enemyUnit.currentHP);
        //    dialogText.text = "You shot an arrow!";

        //    // Destroy the arrow
        //    Destroy(arrowGO);

        //    yield return new WaitForSeconds(0.5f);
        //}

        // Check if enemy is dead
    }

    IEnumerator PlayerAttackMeteorShower()
    {
        if (enemyPosition1obj == null && enemyPosition2obj == null)
        {
            state = BattleStateTwo.WON;
            EndBattle();
        }
        else if(enemyPosition1obj == null)
        {
            for (int i = 0; i < 5; i++)
            {
                // Calculate the position for the meteor
                Vector3 meteorPosition = GetMeteorPosition(i);

                // Spawn a meteor prefab
                GameObject meteorGO = Instantiate(meteorPrefab, meteorPosition, Quaternion.identity);
                Meteor meteor = meteorGO.GetComponent<Meteor>();
                meteor.SetTarget(enemyPositionTwo.position);

                yield return null;
            }

            // Wait for all meteors to reach the enemy
            yield return new WaitForSeconds(Meteor.travelTime);

            // Damage the enemy
            enemyUnitTwo.TakeDamage(enemyUnitTwo.damage);

            enemyTwoHUD.SetHP(enemyUnitTwo.currentHP);
            dialogText.text = "You summoned meteors!";

            state = BattleStateTwo.ENEMYTWOTURN;
            StartCoroutine(EnemyTurnTwo());
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                // Calculate the position for the meteor
                Vector3 meteorPosition = GetMeteorPosition(i);

                // Spawn a meteor prefab
                GameObject meteorGO = Instantiate(meteorPrefab, meteorPosition, Quaternion.identity);
                Meteor meteor = meteorGO.GetComponent<Meteor>();
                meteor.SetTarget(enemyPositionOne.position);

                yield return null;
            }

            // Wait for all meteors to reach the enemy
            yield return new WaitForSeconds(Meteor.travelTime);

            // Damage the enemy
            enemyUnit.TakeDamage(enemyUnitTwo.damage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogText.text = "You summoned meteors!";

            state = BattleStateTwo.ENEMYONETURN;
            StartCoroutine(EnemyTurnOne());

        }
        //for (int i = 0; i < 5; i++)
        //{
        //    // Calculate the position for the meteor
        //    Vector3 meteorPosition = GetMeteorPosition(i);

        //    // Spawn a meteor prefab
        //    GameObject meteorGO = Instantiate(meteorPrefab, meteorPosition, Quaternion.identity);
        //    Meteor meteor = meteorGO.GetComponent<Meteor>();
        //    meteor.SetTarget(enemyPositionOne.position);

        //    yield return null;
        //}

        //// Wait for all meteors to reach the enemy
        //yield return new WaitForSeconds(Meteor.travelTime);

        //// Damage the enemy
        //enemyUnitTwo.TakeDamage(playerUnit.damage);

        //enemyTwoHUD.SetHP(enemyUnit.currentHP);
        //dialogText.text = "You summoned meteors!";



        //// Check if enemy is dead
        //if (isDead)
        //{
        //    state = BattleStateTwo.WON;
        //    EndBattle();
        //}
        //else
        //{
        //    state = BattleStateTwo.ENEMYONETURN;
        //    StartCoroutine(EnemyTurnOne());
        //}
    }

    private Vector3 GetMeteorPosition(int index)
    {
        if(enemyPosition1obj == null)
        {
            float distance = Vector3.Distance(playerPosition.position, enemyPositionTwo.position);
            float step = distance / 8f; // Divide by 8 to get 7 intervals

            Vector3 direction = (enemyPositionTwo.position - playerPosition.position).normalized;
            Vector3 position = playerPosition.position + direction * (step * (index + 1));

        // Offset the position upwards to spawn the meteor above the battlefield
            position.y += 10f;

            return position;

        }
        else
        {   
            float distance = Vector3.Distance(playerPosition.position, enemyPositionOne.position);
            float step = distance / 8f; // Divide by 8 to get 7 intervals

            Vector3 direction = (enemyPositionOne.position - playerPosition.position).normalized;
            Vector3 position = playerPosition.position + direction * (step * (index + 1));

            // Offset the position upwards to spawn the meteor above the battlefield
            position.y += 10f;

            return position;
        }
        //float distance = Vector3.Distance(playerPosition.position, enemyPositionOne.position);
        //float step = distance / 8f; // Divide by 8 to get 7 intervals

        //Vector3 direction = (enemyPositionOne.position - playerPosition.position).normalized;
        //Vector3 position = playerPosition.position + direction * (step * (index + 1));

        // Offset the position upwards to spawn the meteor above the battlefield
        //position.y += 10f;

        //return position;
    }

    IEnumerator PlayerAttackLightning()
    {
        if (enemyPosition1obj == null && enemyPosition2obj == null)
        {
            state = BattleStateTwo.WON;
            EndBattle();
        }

        else if(enemyPosition1obj == null)
        {
            GameObject cloudGO = Instantiate(cloudPrefab, enemyPositionTwo.position + Vector3.up * 10f, Quaternion.identity);

            yield return new WaitForSeconds(1f);

            GameObject lightningStrikeGO = Instantiate(lightningStrikePrefab, enemyPositionTwo.position, Quaternion.identity);
            LightningStrike lightningStrike = lightningStrikeGO.GetComponent<LightningStrike>();
            float strikeTime = lightningStrike.strikeTime; // Access strikeTime property through the instance

            yield return new WaitForSeconds(strikeTime);


            enemyTwoHUD.SetHP(enemyUnitTwo.currentHP);
            dialogText.text = "Lightning Strike!";

            state = BattleStateTwo.ENEMYTWOTURN;
            StartCoroutine(EnemyTurnTwo());
        }
        else
        {
            GameObject cloudGO = Instantiate(cloudPrefab, enemyPositionOne.position + Vector3.up * 10f, Quaternion.identity);

            yield return new WaitForSeconds(1f);

            GameObject lightningStrikeGO = Instantiate(lightningStrikePrefab, enemyPositionOne.position, Quaternion.identity);
            LightningStrike lightningStrike = lightningStrikeGO.GetComponent<LightningStrike>();
            float strikeTime = lightningStrike.strikeTime; // Access strikeTime property through the instance

            yield return new WaitForSeconds(strikeTime);

            // Damage the enemy
            bool isDead = enemyUnit.TakeDamage(enemyUnitTwo.damage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogText.text = "Lightning Strike!";


            state = BattleStateTwo.ENEMYONETURN;
            StartCoroutine(EnemyTurnOne());

        }

        //GameObject cloudGO = Instantiate(cloudPrefab, enemyPositionOne.position + Vector3.up * 10f, Quaternion.identity);

        //yield return new WaitForSeconds(1f);

        //GameObject lightningStrikeGO = Instantiate(lightningStrikePrefab, enemyPositionOne.position, Quaternion.identity);
        //LightningStrike lightningStrike = lightningStrikeGO.GetComponent<LightningStrike>();
        //float strikeTime = lightningStrike.strikeTime; // Access strikeTime property through the instance

        //yield return new WaitForSeconds(strikeTime);

        //// Damage the enemy
        //bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        //enemyHUD.SetHP(enemyUnit.currentHP);
        //dialogText.text = "Lightning Strike!";

        //// Check if enemy is dead
        //if (isDead)
        //{
        //    state = BattleStateTwo.WON;
        //    EndBattle();
        //}
        //else
        //{
        //    state = BattleStateTwo.ENEMYONETURN;
        //    StartCoroutine(EnemyTurnOne());
        //}
    }

    IEnumerator HealPlayer()
    {


        if(enemyPosition1obj == null)
        {

            // Add 25 to the player's currentHP
            playerUnit.hpAmount += 25;

            // Ensure the player's currentHP doesn't exceed the maximumHP
            if (playerUnit.hpAmount > playerUnit.maxHPAmount)
            {
                playerUnit.hpAmount = playerUnit.maxHPAmount;
            }

            playerHUD.SetHP(playerUnit.maxHPAmount);
            dialogText.text = "You have been healed!";

            // Spawn a healing particle system on the player for 3 seconds
            GameObject particleSystemGO = Instantiate(healingParticleSystemPrefab, playerPosition.position, Quaternion.identity);
            ParticleSystem particleSystem = particleSystemGO.GetComponent<ParticleSystem>();
            particleSystem.Play();

            yield return new WaitForSeconds(3f);

            // Destroy the healing particle system
            Destroy(particleSystemGO);

            state = BattleStateTwo.ENEMYTWOTURN;
            StartCoroutine(EnemyTurnTwo());
        }
        else
        {
            // Add 25 to the player's currentHP
            playerUnit.hpAmount += 25;

            // Ensure the player's currentHP doesn't exceed the maximumHP
            if (playerUnit.hpAmount > playerUnit.maxHPAmount)
            {
                playerUnit.hpAmount = playerUnit.maxHPAmount;
            }

            playerHUD.SetHP(playerUnit.hpAmount);
            dialogText.text = "You have been healed!";

            // Spawn a healing particle system on the player for 3 seconds
            GameObject particleSystemGO = Instantiate(healingParticleSystemPrefab, playerPosition.position, Quaternion.identity);
            ParticleSystem particleSystem = particleSystemGO.GetComponent<ParticleSystem>();
            particleSystem.Play();

            yield return new WaitForSeconds(3f);

            // Destroy the healing particle system
            Destroy(particleSystemGO);

            state = BattleStateTwo.ENEMYONETURN;
            StartCoroutine(EnemyTurnOne());
        }

        //// Add 25 to the player's currentHP
        //playerUnit.currentHP += 25;

        //// Ensure the player's currentHP doesn't exceed the maximumHP
        //if (playerUnit.currentHP > playerUnit.maxHP)
        //{
        //    playerUnit.currentHP = playerUnit.maxHP;
        //}

        //playerHUD.SetHP(playerUnit.currentHP);
        //dialogText.text = "You have been healed!";

        //// Spawn a healing particle system on the player for 3 seconds
        //GameObject particleSystemGO = Instantiate(healingParticleSystemPrefab, playerPosition.position, Quaternion.identity);
        //ParticleSystem particleSystem = particleSystemGO.GetComponent<ParticleSystem>();
        //particleSystem.Play();

        //yield return new WaitForSeconds(3f);

        //// Destroy the healing particle system
        //Destroy(particleSystemGO);

        //state = BattleStateTwo.ENEMYONETURN;
        //StartCoroutine(EnemyTurnOne());
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

            bool isDead = playerHUD.TakeDamage(enemyUnit.damage);

            playerHUD.SetHP(playerUnit.hpAmount);

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

            bool isDead = playerHUD.TakeDamage(enemyUnit.damage);

            playerHUD.SetHP(playerUnit.hpAmount);

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
            earnEXP.GainDoubleEXP();
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
        abilityChoicePanel.SetActive(false);
        attackFleePanel.SetActive(true);

        if (lightingStrikeCD != 0)
        {
            lightingStrikeCD -= 1;
        }
        if (meteorShowerCD != 0)
        {
            meteorShowerCD -= 1;
        }
        if (fireBallCD != 0)
        {
            fireBallCD -= 1;
        }
        if (trippleArrowCD != 0)
        {
            trippleArrowCD -= 1;
        }
        if (healingCD != 0)
        {
            healingCD -= 1;
        }

        if (lightingStrikeCD == 0)
        {
            lightingStrike.interactable = true;

        }

        if (meteorShowerCD == 0)
        {

            meteorShower.interactable = true;
        }

        if (trippleArrowCD == 0)
        {

            tripleArrow.interactable = true;
        }

        if (fireBallCD == 0)
        {
            fireBall.interactable = true;
        }

        if (healingCD == 0)
        {
            healing.interactable = true;
        }

        lightingStrikeCDText.text = "(" + lightingStrikeCD + ")";
        meteorShowerCDText.text = "(" + meteorShowerCD + ")";
        fireBallCDText.text = "(" + fireBallCD + ")";
        trippleArrowCDText.text = "(" + trippleArrowCD + ")";
        healingCDText.text = "(" + healingCD + ")";

        dialogText.text = "Choose an action";
    }



    public void OnAttackButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        healing.interactable = false;

        if (state != BattleStateTwo.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnFireballButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        healing.interactable = false;

        fireBallCD = 2;
        fireBallCDText.text = "(" + fireBallCD + ")";

        if (state != BattleStateTwo.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttackFireball());
    }

    public void OnTripleArrowButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        healing.interactable = false;

        trippleArrowCD = 4;
        trippleArrowCDText.text = "(" + lightingStrikeCD + ")";

        if (state != BattleStateTwo.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttackTripleArrow());
    }

    public void OnMeteorButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        healing.interactable = false;

        meteorShowerCD = 2;
        meteorShowerCDText.text = "(" + meteorShowerCD + ")";

        if (state != BattleStateTwo.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttackMeteorShower());
    }

    public void OnLightningButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        healing.interactable = false;

        if (state != BattleStateTwo.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttackLightning());
    }

    public void OnHealButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        healing.interactable = false;

        healingCD = 4;
        healingCDText.text = "(" + healingCD + ")";

        if (state != BattleStateTwo.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(HealPlayer());
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