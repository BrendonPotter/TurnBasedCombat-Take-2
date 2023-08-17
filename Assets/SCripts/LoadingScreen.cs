using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreenUI;
    public Slider loadingProgressBar;
    public Text loadingText;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        loadingScreenUI.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingProgressBar.value = progress;
            loadingText.text = "Loading: " + (progress * 100f).ToString("F0") + "%";

            if (progress >= 1.0f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
