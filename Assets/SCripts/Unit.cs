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


    [SerializeField] SaveSystem enemyStats;

    private void Start()
    {
        SetEnemyState();
    }
    public void SetEnemyState()
    {
        unitLevel = Mathf.RoundToInt(enemyStats._levelVar);
        maxHP = defaultHealth + (100 * unitLevel/2);
        currentHP = maxHP;

        damage = trueDamage;
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
}
