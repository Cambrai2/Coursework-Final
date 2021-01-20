using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy", order = 54)]
public class BaseEnemy : ScriptableObject
{
    //List of abilities that the enemy can use
    public List<Abilities> Abilities;

    //Lists containing the items on the enemy drop table
    public List<Items> alwaysDropTable;
    public List<Items> commonDropTable;
    public List<Items> rareDropTable;

    //unique ID of the enemy
    [SerializeField]
    public int enemyID;

    //Level of the enemy
    [SerializeField]
    public int enemyLevel;

    //name of the enemy
    [SerializeField]
    public string enemyName;

    //description of the enemy
    [SerializeField]
    public string enemyDescription;

    //the enemy max health points
    [SerializeField]
    public int enemyMaxHP;

    //the enemy current health points
    [SerializeField]
    public int enemyCurHP;

    //enemy max mana points
    [SerializeField]
    public int enemyMaxMP;

    //enemy current mana points
    [SerializeField]
    public int enemyCurMP;

    //enemy base attack value
    [SerializeField]
    public int enemyBaseATK;

    //enemy current attack value
    [SerializeField]
    public int enemyCurATK;

    //enemy base defense value
    [SerializeField]
    public int enemyBaseDEF;

    //enemy current defense value
    [SerializeField]
    public int enemyCurDEF;

    //enemy base wisdom value
    [SerializeField]
    public int enemyBaseWIS;

    //enemy current wisdom value
    [SerializeField]
    public int enemyCurWIS;

    //enemy base strength value
    [SerializeField]
    public int enemyBaseSTR;

    //enemy current strength value
    [SerializeField]
    public int enemyCurSTR;

    //enemy base agility value
    [SerializeField]
    public int enemyBaseAGI;

    //enemy current agility value
    [SerializeField]
    public int enemyCurAGI;

    //enemy experience granted on death
    [SerializeField]
    public int experienceGranted;

    //enemy current defending state
    public bool isDefending = false;

    //value the enemy defense increases by when defending
    public int defendingValue;

    //boolean to check if the enemy is basic or custom/boss
    [SerializeField]
    public bool BasicEnemy = false;

    public void Awake()
    {

        //assign basic enemy values proportionate to their level as a basic enemy
        if (BasicEnemy == true)
        {
            enemyMaxHP = (int)Mathf.Round(10 + Mathf.Pow(enemyLevel, 1.50f));
            enemyMaxMP = (int)(150 / 99 * enemyLevel) + 10;
            enemyBaseATK = (int)Mathf.Round(600 / 99 * enemyLevel) + 10;
            enemyBaseSTR = (int)Mathf.Round(600 / 99 * enemyLevel) + 10;
            enemyBaseDEF = (int)Mathf.Round(600 / 99 * enemyLevel) + 10;
            enemyBaseWIS = (int)Mathf.Round(600 / 99 * enemyLevel) + 10;
            enemyBaseAGI = (int)Mathf.Round(600 / 99 * enemyLevel) + 10;

            enemyCurATK = enemyBaseATK;
            enemyCurSTR = enemyBaseSTR;
            enemyCurDEF = enemyBaseDEF;
            enemyCurWIS = enemyBaseWIS;
            enemyCurHP = enemyMaxHP;
            enemyCurMP = enemyMaxMP;
            enemyCurAGI = enemyBaseAGI;

            experienceGranted = (int)Mathf.Round(333 * Mathf.Pow(enemyLevel, 1.25f));

            defendingValue = enemyBaseDEF / 10;
        }
        
    }
}
