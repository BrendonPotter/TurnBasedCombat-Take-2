using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WNeasbeySad : MonoBehaviour
{
    public float quitAfterSeconds = 16f;

    private float elapsedTime = 0f;

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= quitAfterSeconds)
        {
            QuitApplication();
        }
    }

    private void QuitApplication()
    {
        // Quit the application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
