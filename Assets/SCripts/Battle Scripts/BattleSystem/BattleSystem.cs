using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum BattleState {START, PLAYERTURN,PLAYERTWOTURN, ENEMYTURN, WON, LOST }


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
    public Transform hunterSpawnSingle;
    public Transform hunterSpawnDuo;
    public Transform mageSpawnDuo;
    public Transform enemyPosition;

    //Gameobjects for transform to check if active
    [SerializeField] GameObject hunterSpawnSingleGO;
    [SerializeField] GameObject hunterSpawnDuoGO;
    [SerializeField] GameObject mageSpawnDuoGO;

    //Player and Enemy HUD
    public Text dialogText;

    public PlayerHUD hunterSingleHUD;
    public PlayerHUD hunterDuoHUD;
    public PlayerHUD mageDuoHUD;
    public BattleHUD enemyHUD;

    public string explorationScene;

    //cameras
    public Camera mainCamera;
    public Camera fireballCamera;
    public Camera buffCamera;
    public Camera MeteorCamera;
    public Camera arrowCamera;


    //Other script reference
    public Leveling earnEXP;
    public SaveSystem playerUnit;
    private Unit enemyUnit;
    
    //Attack Button panals
    public GameObject attackFleePanel;
    public GameObject abilityChoicePanel;
    [SerializeField] GameObject mageAbilityPanel;
    [SerializeField] GameObject hunterAbilityPanel;
 
    public Button meteorShower;
    public Button lightingStrike;
    public Button fireBall;
    public Button tripleArrow;
    public Button singleShot;
    public Button volly;

    public TextMeshProUGUI lightingStrikeCDText;
    public TextMeshProUGUI meteorShowerCDText;
    public TextMeshProUGUI trippleArrowCDText;
    public TextMeshProUGUI fireBallCDText;
    public TextMeshProUGUI healingCDText;

    public int lightingStrikeCD;
    public int meteorShowerCD;
    public int trippleArrowCD;
    public int fireBallCD;
    public int singleShotCD;

    public CheckCheckpoint playerCheck;

    //amimations
    public GameObject PlayerIdle;
    public GameObject PlayerFireball;
    public GameObject PlayerBuff;
    public GameObject PlayerMeteor;
    public GameObject PlayerTripleArrow;

    //SceneList
    [SerializeField] string forestLevel;
    [SerializeField] string burningVillage;
    [SerializeField] string banditHideout;

    [SerializeField] WorldState worldState;


    // Start is called before the first frame update
    void Start()
    {
        playerCheck.CheckPlayerNumber();
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        
    }

    IEnumerator SetupBattle()
    {

        int enemyChoice1 = Random.Range(0, enemyPrefabs.Length);

        GameObject playerGO = Instantiate(playerPrefab, hunterSpawnSingle);
        GameObject playerGOhunterDuo = Instantiate(playerPrefab, hunterSpawnDuo);
        GameObject playerGOMageDuo = Instantiate(playerPrefab, mageSpawnDuo);

        GameObject enemyGO = Instantiate(enemyPrefabs[enemyChoice1], enemyPosition);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogText.text = "A wild " + enemyUnit.unitName + " has appear";

        hunterSingleHUD.SetHUD();
        hunterSingleHUD.SetLevelNum();
        hunterDuoHUD.SetHUD();
        hunterDuoHUD.SetLevelNum();
        mageDuoHUD.SetHUD();
        mageDuoHUD.SetLevelNum();
        enemyHUD.SetHUD(enemyUnit);


        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator SingleShot()
    {

        //Damage the enemy
        bool isDead =  enemyUnit.TakeDamage(playerUnit.dealDamage);


        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogText.text = "the attack is successful!";

        yield return new WaitForSeconds(2f);

        if (hunterSpawnSingleGO.activeSelf == true)
        {
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
        else if (hunterSpawnSingleGO.activeSelf == false)
        {

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTWOTURN;
                PlayerTwo();
            }
        }
    }

    IEnumerator PlayerAttackFireball()
    {
        // Commented out from merge conflict - 2023/06/29 AG
        //// Switch to the fireball camera
        //PlayerIdle.SetActive(false);
        //PlayerFireball.SetActive(true);
        //fireballCamera.enabled = true;
        //mainCamera.enabled = false;

        //// Play the attack animation on the player
        //// Activate the player GameObject
        ////playerPrefab.SetActive(true);
        ////  Animator playerAnimator = playerPrefab.GetComponent<Animator>();
        //// playerAnimator.Play("Fireball");
        ////I have no clue how to get this animation to reference properly

        if (state == BattleState.PLAYERTURN)
        {
            if (hunterSpawnSingleGO.activeSelf == true)
            {
                // Spawn a fireball prefab
                GameObject fireballGO = Instantiate(fireballPrefab, hunterSpawnSingle.position, Quaternion.identity);
                Fireball fireball = fireballGO.GetComponent<Fireball>();
                fireball.SetTarget(enemyPosition.position);

                // Wait for the fireball to reach the enemy
                yield return new WaitForSeconds(fireball.travelTime);

                // Damage the enemy
                bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage + 25);

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

            else if (hunterSpawnSingleGO.activeSelf == false)
            {
                // Spawn a fireball prefab
                GameObject fireballGO = Instantiate(fireballPrefab, hunterSpawnSingle.position, Quaternion.identity);
                Fireball fireball = fireballGO.GetComponent<Fireball>();
                fireball.SetTarget(enemyPosition.position);

                // Wait for the fireball to reach the enemy
                yield return new WaitForSeconds(fireball.travelTime);

                // Damage the enemy
                bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage + 25);

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
                    state = BattleState.PLAYERTWOTURN;
                    PlayerTwo();
                }
            }
        }
        else if (state == BattleState.PLAYERTWOTURN)
        {
            // Spawn a fireball prefab
            GameObject fireballGO = Instantiate(fireballPrefab, hunterSpawnDuo.position, Quaternion.identity);
            Fireball fireball = fireballGO.GetComponent<Fireball>();
            fireball.SetTarget(enemyPosition.position);

            // Wait for the fireball to reach the enemy
            yield return new WaitForSeconds(fireball.travelTime);

            // Damage the enemy
            bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage + 25);

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

        // Switch back to the main camera
        fireballCamera.enabled = false;
        mainCamera.enabled = true;
        PlayerIdle.SetActive(true);
        PlayerFireball.SetActive(false);
    }

    IEnumerator PlayerAttackTripleArrow()
    {
        // Commented out from merge conflict - 2023/06/29 AG
        //// Switch to the  camera
        //PlayerIdle.SetActive(false);
        //PlayerTripleArrow.SetActive(true);
        //arrowCamera.enabled = true;
        //mainCamera.enabled = false;

        if (state == BattleState.PLAYERTURN)
        {
            if (hunterSpawnSingleGO.activeSelf == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Spawn an arrow prefab
                    GameObject arrowGO = Instantiate(arrowPrefab, hunterSpawnSingle.position, Quaternion.identity);
                    Arrow arrow = arrowGO.GetComponent<Arrow>();
                    arrow.SetTarget(enemyPosition.position);

                    // Wait for the arrow to reach the enemy
                    yield return new WaitForSeconds(arrow.travelTime);

                    // Damage the enemy
                    bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage + 10);

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
            else if (hunterSpawnSingleGO.activeSelf == false)
            {
                for (int i = 0; i < 3; i++)
                {
                    // Spawn an arrow prefab
                    GameObject arrowGO = Instantiate(arrowPrefab, hunterSpawnDuo.position, Quaternion.identity);
                    Arrow arrow = arrowGO.GetComponent<Arrow>();
                    arrow.SetTarget(enemyPosition.position);

                    // Wait for the arrow to reach the enemy
                    yield return new WaitForSeconds(arrow.travelTime);

                    // Damage the enemy
                    bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage + 10);

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
                    state = BattleState.PLAYERTWOTURN;
                    PlayerTwo();
                }
            }
        }
        else if (state == BattleState.PLAYERTWOTURN)
        {
            for (int i = 0; i < 3; i++)
            {
                // Spawn an arrow prefab
                GameObject arrowGO = Instantiate(arrowPrefab, hunterSpawnDuo.position, Quaternion.identity);
                Arrow arrow = arrowGO.GetComponent<Arrow>();
                arrow.SetTarget(enemyPosition.position);

                // Wait for the arrow to reach the enemy
                yield return new WaitForSeconds(arrow.travelTime);

                // Damage the enemy
                bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage + 10);

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
        
        PlayerIdle.SetActive(true);
        PlayerTripleArrow.SetActive(false);
        arrowCamera.enabled = false;
        mainCamera.enabled = true;
    }

    IEnumerator PlayerAttackMeteorShower()
    {
        PlayerIdle.SetActive(false);
        PlayerMeteor.SetActive(true);
        MeteorCamera.enabled = true;
        mainCamera.enabled = false;
        

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

        PlayerIdle.SetActive(true);
        PlayerMeteor.SetActive(false);
        MeteorCamera.enabled = false;
        mainCamera.enabled = true;

    }

    private Vector3 GetMeteorPosition(int index)
    {
        float distance = Vector3.Distance(hunterSpawnSingle.position, enemyPosition.position);
        float step = distance / 8f; // Divide by 8 to get 7 intervals

        Vector3 direction = (enemyPosition.position - hunterSpawnSingle.position).normalized;
        Vector3 position = hunterSpawnSingle.position + direction * (step * (index + 1));

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

        PlayerIdle.SetActive(false);
        PlayerBuff.SetActive(true);
        buffCamera.enabled = true;
        mainCamera.enabled = false;

        // Ensure the player's currentHP doesn't exceed the maximumHP
        if (playerUnit.hpAmount > playerUnit.maxHPAmount)
        {
            playerUnit.hpAmount = playerUnit.maxHPAmount;
        }

        hunterSingleHUD.SetHP(playerUnit.hpAmount);
        dialogText.text = "You have been healed!";

        // Spawn a healing particle system on the player for 3 seconds
        GameObject particleSystemGO = Instantiate(healingParticleSystemPrefab, hunterSpawnSingle.position, Quaternion.identity);
        ParticleSystem particleSystem = particleSystemGO.GetComponent<ParticleSystem>();
        particleSystem.Play();

        yield return new WaitForSeconds(3f);

        // Destroy the healing particle system
        Destroy(particleSystemGO);

        // Switch back to the main camera
        buffCamera.enabled = false;
        mainCamera.enabled = true;
        PlayerIdle.SetActive(true);
        PlayerBuff.SetActive(false);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator RageBoost()
    {
        // Add 25 to the player's Attack Stat
        playerUnit.dealDamage += 15;

        PlayerIdle.SetActive(false);
        PlayerBuff.SetActive(true);
        buffCamera.enabled = true;
        mainCamera.enabled = false;

        dialogText.text = "You have increased you're testostorne!";

        // Spawn a particle system on the player for 3 seconds
        GameObject particleSystemGO = Instantiate(RageParticleSystemPrefab, hunterSpawnSingle.position, Quaternion.identity);
        ParticleSystem particleSystem = particleSystemGO.GetComponent<ParticleSystem>();
        particleSystem.Play();

        yield return new WaitForSeconds(3f);

        // Destroy the healing particle system
        Destroy(particleSystemGO);
       
        // Switch back to the main camera
        buffCamera.enabled = false;
        mainCamera.enabled = true;
        PlayerIdle.SetActive(true);
        PlayerBuff.SetActive(false);

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
        GameObject particleSystemGO = Instantiate(RageParticleSystemPrefab, hunterSpawnSingle.position, Quaternion.identity);
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
        GameObject particleSystemGO = Instantiate(RageParticleSystemPrefab, hunterSpawnSingle.position, Quaternion.identity);
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

        hunterSingleHUD.SetHP(playerUnit.hpAmount);
        dialogText.text = "You have swiped their life force!";

        // Spawn a healing particle system on the player for 3 seconds
        GameObject particleSystemGO = Instantiate(holyParticleSystemPrefab, hunterSpawnSingle.position, Quaternion.identity);
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

    IEnumerator Volley()
    {

        yield return new WaitForSeconds(1f);

        // Damage the enemy
        bool isDead = enemyUnit.TakeDamage(playerUnit.dealDamage + 60);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogText.text = "Volley!";

        // Check if enemy is dead
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTWOTURN;
            PlayerTwo();
        }
    }

    IEnumerator EnemyTurn()
    {
        if (hunterSpawnSingleGO.activeSelf == true)
        {
            dialogText.text = enemyUnit.unitName + "attacks";

            yield return new WaitForSeconds(1f);

            bool isDead = hunterSingleHUD.TakeDamage(enemyUnit.damage);

            hunterSingleHUD.SetHP(playerUnit.hpAmount);

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
        else if(hunterSpawnSingleGO.activeSelf == false)
        {
            if(hunterSpawnDuoGO.activeSelf == true)
            {
                dialogText.text = enemyUnit.unitName + "attacks";

                yield return new WaitForSeconds(1f);

                bool isDead = hunterSingleHUD.TakeDamage(enemyUnit.damage);

                hunterDuoHUD.SetHP(playerUnit.hpAmount);

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
            else if(hunterSpawnDuoGO.activeSelf == false)
            {
                dialogText.text = enemyUnit.unitName + "attacks";

                yield return new WaitForSeconds(1f);

                bool isDead = mageDuoHUD.TakeDamage(enemyUnit.damage);

                mageDuoHUD.SetHP(playerUnit.hpAmount);

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
        mageAbilityPanel.SetActive(false);
        hunterAbilityPanel.SetActive(false);
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
        if(singleShotCD != 0)
        {
            singleShotCD -= 1;
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

        if(singleShotCD == 0)
        {
            singleShot.interactable = true;
        }

        lightingStrikeCDText.text = "(" + lightingStrikeCD + ")";
        meteorShowerCDText.text = "(" + meteorShowerCD + ")";
        fireBallCDText.text = "(" + fireBallCD + ")";
        trippleArrowCDText.text = "(" + trippleArrowCD + ")";
        healingCDText.text = "(" + singleShotCD + ")";

        dialogText.text = "Choose an action";
    }

    void PlayerTwo()
    {
        hunterAbilityPanel.SetActive(false);
        mageAbilityPanel.SetActive(false);
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
        if (singleShotCD != 0)
        {
            singleShotCD -= 1;
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

        if (singleShotCD == 0)
        {
            singleShot.interactable = true;
        }

        lightingStrikeCDText.text = "(" + lightingStrikeCD + ")";
        meteorShowerCDText.text = "(" + meteorShowerCD + ")";
        fireBallCDText.text = "(" + fireBallCD + ")";
        trippleArrowCDText.text = "(" + trippleArrowCD + ")";
        healingCDText.text = "(" +singleShotCD + ")";

        dialogText.text = "Choose an action";
    }



    //public void OnAttackButton()
    //{
    //    if (state == BattleState.PLAYERTURN)
    //    {
    //        tripleArrow.interactable = false;
    //    }
    //    meteorShower.interactable = false;
    //    lightingStrike.interactable = false;
    //    tripleArrow.interactable = false;
    //    fireBall.interactable = false;
    //    healing.interactable = false;

    //    if (state != BattleState.PLAYERTURN)
    //    {
    //        return;
    //    }

    //    StartCoroutine(PlayerAttack());
    //}

    public void OnFireballButton()
    {
        if(state == BattleState.PLAYERTURN)
        {
            meteorShower.interactable = false;
            lightingStrike.interactable = false;
            tripleArrow.interactable = false;
            fireBall.interactable = false;
            singleShot.interactable = false;
            volly.interactable = false;

            fireBallCD = 0;
            fireBallCDText.text = "(" + fireBallCD + ")";

            if (state != BattleState.PLAYERTURN)
            {
                return;
            }
            //else if(state != BattleState.PLAYERTWOTURN)
            //{
            //    return;
            //}

            StartCoroutine(PlayerAttackFireball());
        }
        else if(state == BattleState.PLAYERTWOTURN)
        {
            meteorShower.interactable = false;
            lightingStrike.interactable = false;
            tripleArrow.interactable = false;
            fireBall.interactable = false;
            singleShot.interactable = false;
            volly.interactable = false;

            fireBallCD = 0;
            fireBallCDText.text = "(" + fireBallCD + ")";

            if (state != BattleState.PLAYERTWOTURN)
            {
                return;
            }
            //else if(state != BattleState.PLAYERTWOTURN)
            //{
            //    return;
            //}

            StartCoroutine(PlayerAttackFireball());
        }
    }

    public void OnTripleArrowButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        singleShot.interactable = false;
        volly.interactable = false;

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
        singleShot.interactable = false;
        volly.interactable = false;

        meteorShowerCD = 2;
        meteorShowerCDText.text = "(" + meteorShowerCD + ")";

        if (state != BattleState.PLAYERTWOTURN)
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
        singleShot.interactable = false;
        volly.interactable = false;

        if (state != BattleState.PLAYERTWOTURN)
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
        singleShot.interactable = false;
        volly.interactable = false;

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        //lightingButton.interatable = false;
        StartCoroutine(HealPlayer());
    }

    public void OnRageButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        singleShot.interactable = false;
        volly.interactable = false;

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(RageBoost());
    }

    public void OnBlockButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        singleShot.interactable = false;
        volly.interactable = false;

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(BlockAttack());
    }

    public void OnVolleyButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        singleShot.interactable = false;
        volly.interactable = false;

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(Volley());

    }

    public void OnBullyButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        singleShot.interactable = false;
        volly.interactable = false;

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(Bully());
    }

    public void OnHealthStealButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        singleShot.interactable = false;
        volly.interactable = false;

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(HealthSteal());
    }

    public void OnMightySlashButton()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        singleShot.interactable = false;
        volly.interactable = false;

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(MightySlash());
    }

    public void OnSingleShot()
    {
        meteorShower.interactable = false;
        lightingStrike.interactable = false;
        tripleArrow.interactable = false;
        fireBall.interactable = false;
        singleShot.interactable = false;
        volly.interactable = false;

        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(SingleShot());
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

        if(worldState.sceneNumber == 1)
        {
            SceneManager.LoadScene(forestLevel);
        }
        if (worldState.sceneNumber == 2)
        {
            SceneManager.LoadScene(burningVillage);
        }
        if (worldState.sceneNumber == 3)
        {
            SceneManager.LoadScene(banditHideout);
        }
    }

}


