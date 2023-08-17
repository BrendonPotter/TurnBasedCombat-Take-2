using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteruptThePoly : MonoBehaviour
{
    [SerializeField] FollowThePlayer followPlayerScript;
    [SerializeField] NpcTalk checkScriptEnable;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        followPlayerScript.playerIsInRange = true;
        if (checkScriptEnable.scriptEnabled == false)
        {
            followPlayerScript.enabled = true;
        }
    }
}
