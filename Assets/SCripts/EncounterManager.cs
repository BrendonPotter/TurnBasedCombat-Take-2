using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EncounterManager : MonoBehaviour
{
    private Vector3 previousPosition;
    private bool isMoving;

    public string battleScene1;
    public string battleScene2;

    public GameObject encounterText;

    public Image screenFadeImage;
    public float fadeDuration = 1.0f;

    public AudioSource encounterSound;
    public Camera transitioncamera;
    public Camera playercamera;
    public GameObject uiTransitioneffect;
    public AudioSource overworldMusic;


    //private var nextRoll;

    private void Start()
    {
        // Initialize the previous position to the player's starting position
        previousPosition = transform.position;
        StartCoroutine(EncounterCheck());
    }

    //private void Update()
    //{
        //if (Time.time > nextFlicker)
        //{
            //StartCoroutine(EncounterCheck());
            //nextRoll += 10;
        //}
        //StartCoroutine(EncounterCheck());

        //CheckMoving();

        //if (isMoving == true)
        //{
        //    StartCoroutine(EncounterCheck());
        //}
    //}

    IEnumerator EncounterCheck()
    {
        yield return new WaitForSeconds(5);

        int ecounterCheck = Random.Range(1, 10);

        if (ecounterCheck <= 4)
        {
            //stop music
            //overworldMusic.Stop();

            //GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
            encounterText.SetActive(true);
            
           // yield return StartCoroutine(ScreenFadeOut());
              
            // Play sound effect
            //encounterSound.Play();

            //// Enable UI element
            //uiTransitioneffect.SetActive(true);

            //// Enable camera
            //transitioncamera.enabled = true;

            //// Disable player's camera
            //playercamera.enabled = false;

            yield return new WaitForSeconds(2);
            
          

            SceneManager.LoadScene(battleScene1);
        }

        //else if (ecounterCheck >= 3 && ecounterCheck <= 4)
        //{
        //    GameObject.Find("Player").GetComponent<PlayerMovement>().enabled = false;
        //    encounterText.SetActive(true);

        //    yield return StartCoroutine(ScreenFadeOut());

        //    stop music
        //    overworldMusic.Stop();

        //    Play sound effect
        //    encounterSound.Play();

        //    Enable UI element
        //    uiTransitioneffect.SetActive(true);

        //    Enable camera
        //    transitioncamera.enabled = true;

        //    Disable player's camera
        //    playercamera.enabled = false;

        //    yield return new WaitForSeconds(2);



        //    SceneManager.LoadScene(battleScene2);
        //}
        else if(ecounterCheck > 4)    
        {
            Debug.Log("rolled high");
            StartCoroutine(EncounterCheck());
        }
    }

    /*
    IEnumerator ScreenFadeOut()
    {
        float elapsedTime = 0f;
        Color originalColor = screenFadeImage.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / fadeDuration);
            screenFadeImage.color = Color.Lerp(originalColor, targetColor, normalizedTime);
            yield return null;
        }

        screenFadeImage.color = targetColor;
    }
    */
}

//    void CheckMoving()
//    {
//        //Check if the player's position has changed since the last frame
//        if (transform.position != previousPosition)
//        {
//            Debug.Log("is Moving");
//            isMoving = true;
//        }
//        else
//        {
//            Debug.Log("Is not moving");
//            isMoving = false;
//        }

//        //Update the previous position to the current position for the next frame

//       previousPosition = transform.position;
//    }
//}
