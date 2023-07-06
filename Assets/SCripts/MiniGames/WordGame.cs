using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WordGame : MonoBehaviour
{
    public TextMeshProUGUI wordText;
    public TextMeshProUGUI scoreText;
    public float timeLimit = 60f;

    public Canvas timerCanvas;
    public TMP_Text currentTimeText;
    public TMP_Text bestTimeText;

    private string[] words = { "apple", "banana", "cherry", "orange", "grape" };
    private string currentWord;
    private int score = 0;
    private float timer;
    private bool isPlaying = false;

    private float startTime;
    private float currentTime;
    private float bestTime;

    private const string BestTimeKey = "BestTime";

    private void Start()
    {
        scoreText.text = "Score: " + score;

        // Load the best time from PlayerPrefs
        bestTime = PlayerPrefs.GetFloat(BestTimeKey, float.MaxValue);

        // Disable the timer UI canvas
        timerCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlaying)
        {
            timer -= Time.deltaTime;
            currentTime = Time.time - startTime;
            currentTimeText.text = "Current Time: " + currentTime.ToString("F2");

            if (timer <= 0f)
            {
                EndGame();
            }
        }
    }

    public void StartGame()
    {
        score = 0;
        scoreText.text = "Score: " + score;

        timer = timeLimit;
        startTime = Time.time;
        currentTime = 0f;

        // Enable the timer UI canvas
        timerCanvas.gameObject.SetActive(true);
        isPlaying = true;

        // Start the word display coroutine
        StartCoroutine(GenerateWords());
    }

    public void TypeInput(string input)
    {
        if (isPlaying && input.ToLower() == currentWord.ToLower())
        {
            score++;
            scoreText.text = "Score: " + score;
        }
    }

    private System.Collections.IEnumerator GenerateWords()
    {
        while (isPlaying)
        {
            currentWord = words[Random.Range(0, words.Length)];
            wordText.text = currentWord;
            yield return new WaitForSeconds(2f);
            wordText.text = "";
            yield return new WaitForSeconds(1f);
        }
    }

    private void EndGame()
    {
        isPlaying = false;
        timerCanvas.gameObject.SetActive(false);

        // Update the best time if the current time is better
        if (currentTime < bestTime)
        {
            bestTime = currentTime;
            bestTimeText.text = "Best Time: " + bestTime.ToString("F2");

            // Save the best time to PlayerPrefs
            PlayerPrefs.SetFloat(BestTimeKey, bestTime);
            PlayerPrefs.Save();
        }
    }
}
