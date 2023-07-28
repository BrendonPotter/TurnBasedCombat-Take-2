using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;
    public int trueDamage;

    public int maxHP;
    public int currentHP;
    public int defaultHealth;

    private int levelBuffer;


    [SerializeField] SaveSystem enemyStats;
    public SaveSystem _levelVar;

    private void Start()
    {
        LevelBufferCreation();
        SetEnemyState();
    }
    public void SetEnemyState()
    {
        unitLevel = levelBuffer;
        maxHP = defaultHealth + (10 * unitLevel/2);
        currentHP = maxHP;

        damage = trueDamage + (2 * unitLevel/2);
        trueDamage *= unitLevel;
    }
    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if(currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LevelBufferCreation()
    {
        int baseNumber = _levelVar._levelVar; // Replace this with your desired base number
        int minOffset = Mathf.Max(0, baseNumber - 5);
        int maxOffset = baseNumber + 5;

        levelBuffer = Random.Range(minOffset, maxOffset + 1);
        Debug.Log(levelBuffer + "this is the buffer");
    }
}
