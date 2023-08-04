using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class AttackButton : MonoBehaviour
{
    public BattleState state;

    [SerializeField] GameObject HunterSpawnSingle;

    //Panels
    public GameObject attackFleePanel;
    public GameObject abilityChoicePanel;
    [SerializeField] GameObject hunterAttackPanel;
    [SerializeField] GameObject mageAttackPanel;

    public Button fleeButton;
    public Button attackButton;

    [SerializeField] WorldState worldState;

    public Text succesfulFlee;
    public Text unsuccessfulFlee;

    public string mainScene;

    [SerializeField] string forestLevel;
    [SerializeField] string burningVillage;
    [SerializeField] string banditHideout;

    private void Awake()
    {
        state = BattleState.PLAYERTURN;
    }
    public void OnAttackButton()
    {

        if (state == BattleState.PLAYERTURN)
        {
            if (HunterSpawnSingle.activeSelf == true)
            {
                Debug.Log("Button is pressed");
                hunterAttackPanel.SetActive(true);
                Debug.Log("Panel is active");
                attackFleePanel.SetActive(false);
                Debug.Log("Panel is disabled");
            }
            else if (HunterSpawnSingle.activeSelf == false)
            {
                Debug.Log("Button is pressed");
                hunterAttackPanel.SetActive(true);
                Debug.Log("Panel is active");
                attackFleePanel.SetActive(false);
                Debug.Log("Panel is disabled");

                state = BattleState.PLAYERTWOTURN;

            }
        }
        else if (state == BattleState.PLAYERTWOTURN)
        {
            Debug.Log("Button is pressed");
            mageAttackPanel.SetActive(true);
            Debug.Log("Panel is active");
            attackFleePanel.SetActive(false);
            Debug.Log("Panel is disabled");

            state = BattleState.PLAYERTURN;
        }
    }

    public void OnFleeButton()
    {
        ButtonInactive();
        Debug.Log("Button working");
        int flee = Random.Range(1, 3);
        Debug.Log("Roll Happened");
        if(flee == 1)
        {
            Debug.Log("Roll Sucessfull");
            StartCoroutine(SuccessSwitchDelay());
        }
        else
        {
            Debug.Log("Roll Unsucesfull");
            StartCoroutine(FailFlee());
        }
    }

    IEnumerator SuccessSwitchDelay()
    {
        Debug.Log("Successful flee");
        yield return new WaitForSeconds(2);


        succesfulFlee.text = "you have succesfully fled";

        yield return new WaitForSeconds(2);

        if (worldState.sceneNumber == 1)
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

    IEnumerator FailFlee()
    {
        Debug.Log("Flee Fail");
        yield return new WaitForSeconds(2);
        unsuccessfulFlee.text = "You have Failed to flee";
        ButtonActive();
    }

    private void ButtonInactive()
    {
        fleeButton.interactable = false;
        attackButton.interactable = false;
    }

    private void ButtonActive()
    {
        Debug.Log("Button Active again");
        fleeButton.interactable = true;
        attackButton.interactable = true;
    }
}
