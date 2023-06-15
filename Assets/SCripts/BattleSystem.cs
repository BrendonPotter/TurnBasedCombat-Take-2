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
    public GameObject fireballPrefab;
    public GameObject arrowPrefab;
    public GameObject meteorPrefab;
    public GameObject enemyPrefab;
    public GameObject cloudPrefab;
    public GameObject lightningStrikePrefab;
    public GameObject healingParticleSystemPrefab;


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

    IEnumerator PlayerAttackFireball()
    {
        // Spawn a fireball prefab
        GameObject fireballGO = Instantiate(fireballPrefab, playerPosition.position, Quaternion.identity);
        Fireball fireball = fireballGO.GetComponent<Fireball>();
        fireball.SetTarget(enemyPosition.position);

        // Wait for the fireball to reach the enemy
        yield return new WaitForSeconds(fireball.travelTime);

        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogText.text = "You cast a fireball!";

        // Destroy the fireball
        Destroy(fireballGO);

        yield return new WaitForSeconds(2f);

        // Check if enemy is dead
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
    }

    IEnumerator PlayerAttackTripleArrow()
    {
        for (int i = 0; i < 3; i++)
        {
            // Spawn an arrow prefab
            GameObject arrowGO = Instantiate(arrowPrefab, playerPosition.position, Quaternion.identity);
            Arrow arrow = arrowGO.GetComponent<Arrow>();
            arrow.SetTarget(enemyPosition.position);

            // Wait for the arrow to reach the enemy
            yield return new WaitForSeconds(arrow.travelTime);

            // Damage the enemy
            bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogText.text = "You shot an arrow!";

            // Destroy the arrow
            Destroy(arrowGO);

            yield return new WaitForSeconds(0.5f);
        }

        // Check if enemy is dead
        if (enemyUnit.currentHP <= 0)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerAttackMeteorShower()
    {
        for (int i = 0; i < 5; i++)
        {
            // Calculate the position for the meteor
            Vector3 meteorPosition = GetMeteorPosition(i);

            // Spawn a meteor prefab
            GameObject meteorGO = Instantiate(meteorPrefab, meteorPosition, Quaternion.identity);
            Meteor meteor = meteorGO.GetComponent<Meteor>();
            meteor.SetTarget(enemyPosition.position);

            yield return null;
        }

        // Wait for all meteors to reach the enemy
        yield return new WaitForSeconds(Meteor.travelTime);

        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogText.text = "You summoned meteors!";

        

        // Check if enemy is dead
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
    }

    private Vector3 GetMeteorPosition(int index)
    {
        float distance = Vector3.Distance(playerPosition.position, enemyPosition.position);
        float step = distance / 8f; // Divide by 8 to get 7 intervals

        Vector3 direction = (enemyPosition.position - playerPosition.position).normalized;
        Vector3 position = playerPosition.position + direction * (step * (index + 1));

        // Offset the position upwards to spawn the meteor above the battlefield
        position.y += 10f;

        return position;
    }

    IEnumerator PlayerAttackLightning()
    {
        GameObject cloudGO = Instantiate(cloudPrefab, enemyPosition.position + Vector3.up * 10f, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        GameObject lightningStrikeGO = Instantiate(lightningStrikePrefab, enemyPosition.position, Quaternion.identity);
        LightningStrike lightningStrike = lightningStrikeGO.GetComponent<LightningStrike>();
        float strikeTime = lightningStrike.strikeTime; // Access strikeTime property through the instance

        yield return new WaitForSeconds(strikeTime);

        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogText.text = "Lightning Strike!";

        // Check if enemy is dead
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
    }

    IEnumerator HealPlayer()
    {
        // Add 25 to the player's currentHP
        playerUnit.currentHP += 25;

        // Ensure the player's currentHP doesn't exceed the maximumHP
        if (playerUnit.currentHP > playerUnit.maxHP)
        {
            playerUnit.currentHP = playerUnit.maxHP;
        }

        playerHUD.SetHP(playerUnit.currentHP);
        dialogText.text = "You have been healed!";

        // Spawn a healing particle system on the player for 3 seconds
        GameObject particleSystemGO = Instantiate(healingParticleSystemPrefab, playerPosition.position, Quaternion.identity);
        ParticleSystem particleSystem = particleSystemGO.GetComponent<ParticleSystem>();
        particleSystem.Play();

        yield return new WaitForSeconds(3f);

        // Destroy the healing particle system
        Destroy(particleSystemGO);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
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

    public void OnFireballButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttackFireball());
    }

    public void OnTripleArrowButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttackTripleArrow());
    }

    public void OnMeteorButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttackMeteorShower());
    }

    public void OnLightningButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttackLightning());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
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

}


