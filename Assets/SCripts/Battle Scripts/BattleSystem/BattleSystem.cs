using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST }


public class BattleSystem : MonoBehaviour
{

    public GameObject[] enemyPrefabs;

    public BattleState state;

    //Prefab
    public GameObject playerPrefab;
    public GameObject fireballPrefab;
    public GameObject arrowPrefab;
    public GameObject meteorPrefab;
    public GameObject enemyPrefab;
    public GameObject cloudPrefab;
    public GameObject lightningStrikePrefab;
    public GameObject holybeamPrefab;
   
    public GameObject healingParticleSystemPrefab;
    public GameObject RageParticleSystemPrefab;
    public GameObject holyParticleSystemPrefab;
    

    //Position
    public Transform playerPosition;
    public Transform enemyPosition;

    //Player and Enemy HUD
    public Text dialogText;

    public PlayerHUD playerHUD;
    public BattleHUD enemyHUD;

    public string explorationScene;

    //Other script reference
    public Leveling earnEXP;
    public SaveSystem playerUnit;
    private Unit enemyUnit;

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
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        
    }

    IEnumerator SetupBattle()
    {

        int enemyChoice1 = Random.Range(0, enemyPrefabs.Length);

        GameObject playerGO = Instantiate(playerPrefab, playerPosition);

        GameObject enemyGO = Instantiate(enemyPrefabs[enemyChoice1], enemyPosition);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogText.text = "A wild " + enemyUnit.unitName + " has appear";

        playerHUD.SetHUD();
        playerHUD.SetLevelNum();
        enemyHUD.SetHUD(enemyUnit);


        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {

        //Damage the enemy
        bool isDead =  enemyUnit.TakeDamage(playerUnit.dealDamage);


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
        bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage+25);

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
            bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage+10);

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
        bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage + 80);

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
        bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage + 60);

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
        playerUnit.hpAmount += 30;

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

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator RageBoost()
    {
        // Add 25 to the player's Attack Stat
        playerUnit.dealDamage += 15;

       
        dialogText.text = "You have increased you're testostorne!";

        // Spawn a particle system on the player for 3 seconds
        GameObject particleSystemGO = Instantiate(RageParticleSystemPrefab, playerPosition.position, Quaternion.identity);
        ParticleSystem particleSystem = particleSystemGO.GetComponent<ParticleSystem>();
        particleSystem.Play();

        yield return new WaitForSeconds(3f);

        // Destroy the healing particle system
        Destroy(particleSystemGO);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator BlockAttack()
    {
        // Set the enemies Attack Stat to 0 to give the illusion of blocking
       
        enemyUnit.damage = 0;
        //My dumbass hasn't figured out how to set it back to its previous attack stat

        dialogText.text = "You have blocked the enemies attack!";

        // Spawn a particle system on the player for 3 seconds
        GameObject particleSystemGO = Instantiate(RageParticleSystemPrefab, playerPosition.position, Quaternion.identity);
        ParticleSystem particleSystem = particleSystemGO.GetComponent<ParticleSystem>();
        particleSystem.Play();

        yield return new WaitForSeconds(3f);

        // Destroy the healing particle system
        Destroy(particleSystemGO);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator Bully()
    {
        // Subtract 5 to the enemies Attack Stat
        enemyUnit.damage -= 5;


        dialogText.text = "You insulted the opposition now they feel depressed!";

        // Spawn a particle system on the player for 3 seconds
        GameObject particleSystemGO = Instantiate(RageParticleSystemPrefab, playerPosition.position, Quaternion.identity);
        ParticleSystem particleSystem = particleSystemGO.GetComponent<ParticleSystem>();
        particleSystem.Play();

        yield return new WaitForSeconds(3f);

        // Destroy the healing particle system
        Destroy(particleSystemGO);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator HealthSteal()
    {
        //Damage the enemy
        bool isDead = enemyUnit.TakeDamage(enemyUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        
        
        // Add to the player's currentHP
        playerUnit.hpAmount += 10;

        // Ensure the player's currentHP doesn't exceed the maximumHP
        if (playerUnit.hpAmount > playerUnit.maxHPAmount)
        {
            playerUnit.hpAmount = playerUnit.maxHPAmount;
        }

        playerHUD.SetHP(playerUnit.hpAmount);
        dialogText.text = "You have swiped their life force!";

        // Spawn a healing particle system on the player for 3 seconds
        GameObject particleSystemGO = Instantiate(holyParticleSystemPrefab, playerPosition.position, Quaternion.identity);
        ParticleSystem particleSystem = particleSystemGO.GetComponent<ParticleSystem>();
        particleSystem.Play();
        yield return new WaitForSeconds(3f);
         // Destroy the healing particle system
        Destroy(particleSystemGO);
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

    }

    IEnumerator MightySlash()
    {
        //temperarly increase damage
        playerUnit.dealDamage += 35;
        //Damage the enemy

        bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage);

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
            playerUnit.dealDamage = playerUnit.trueDamage;
        }
       
    }

    IEnumerator EnemyTurn()
    {
        dialogText.text = enemyUnit.unitName + "attacks";

        yield return new WaitForSeconds(1f);

        bool isDead = playerHUD.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.hpAmount);

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
            enemyUnit.damage = enemyUnit.trueDamage;
        }
    }


    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogText.text = "You won the Battle!";
            earnEXP.GainEXP();
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
        abilityChoicePanel.SetActive(false);
        attackFleePanel.SetActive(true);

        if(lightingStrikeCD != 0)
        {
            lightingStrikeCD -= 1;
        }
        if(meteorShowerCD != 0)
        {
            meteorShowerCD -= 1;
        }
        if (fireBallCD != 0)
        {
            fireBallCD -= 1;
        }
        if(trippleArrowCD != 0)
        {
            trippleArrowCD -= 1;
        }
        if(healingCD != 0)
        {
            healingCD -= 1;
        }

        if(lightingStrikeCD == 0)
        {
            lightingStrike.interactable = true;

        }

        if(meteorShowerCD == 0)
        {

            meteorShower.interactable = true;
        }

        if(trippleArrowCD == 0)
        {

            tripleArrow.interactable = true;
        }
        
        if(fireBallCD == 0)
        { 
            fireBall.interactable = true;
        }

        if(healingCD == 0)
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

        if(state != BattleState.PLAYERTURN)
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

        if (state != BattleState.PLAYERTURN)
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


        if (state != BattleState.PLAYERTURN)
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

        if (state != BattleState.PLAYERTURN)
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

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        lightingStrikeCD = 3;
        lightingStrikeCDText.text = "(" + lightingStrikeCD + ")";

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

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //lightingButton.interatable = false;
        StartCoroutine(HealPlayer());
    }

    public void OnRageButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(RageBoost());
    }

    public void OnBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(BlockAttack());
    }

    public void OnBullyButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(Bully());
    }

    public void OnHealthStealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(HealthSteal());
    }

    public void OnMightySlashButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(MightySlash());
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


