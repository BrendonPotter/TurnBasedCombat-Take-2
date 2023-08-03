using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class WorldState : ScriptableObject
{
    public bool obtainKey;

    [Header("Enemy 1")]
    public bool contactEnemy1;
    public bool winVsEnemy1;

    [Header("Enemy 2")]
    public bool contactEnemy2;
    public bool winVsEnemy2;

    [Header("Enemy 3")]
    public bool contactEnemy3;
    public bool winVsEnemy3;

    [Header("Enemy 4")]
    public bool contactEnemy4;
    public bool winVsEnemy4;

    [Header("Enemy 5")]
    public bool contactEnemy5;
    public bool winVsEnemy5;

    [Header("Hide and Seek State")]
    public bool agreeToPlay;
    public bool successTask;
    public bool failTask;

    public int founded;

    public int sceneNumber;

    public bool bossDead;

    public bool Value
    {
        get { return obtainKey; }
        set { obtainKey = value; }
    }
}


