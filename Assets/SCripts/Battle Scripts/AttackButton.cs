using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public GameObject attackFleePanel;
    public GameObject abilityChoicePanel;

    public Button fleeButton;
    public Button attackButton;

    public Text succesfulFlee;
    public Text unsuccessfulFlee;

    public string mainScene;
    public void OnAttackButton()
    {
        Debug.Log("Button is pressed");
        abilityChoicePanel.SetActive(true);
        Debug.Log("Panel is active");
        attackFleePanel.SetActive(false);
        Debug.Log("Panel is disabled");
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

        SceneManager.LoadScene(mainScene);

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
