using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartDataOnTitle : MonoBehaviour
{
    [SerializeField] DataReseter restartOnTitleScene;
    // Start is called before the first frame update
    void Awake()
    {
        restartOnTitleScene.ResetData();
        restartOnTitleScene.ResetWorldState();
    }
}
