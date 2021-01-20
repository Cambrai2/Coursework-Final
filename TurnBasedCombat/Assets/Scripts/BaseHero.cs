using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName = "Hero", order = 55)]

public class BaseHero : ScriptableObject
{
    //list of abilities belonging to the hero
    public List<Abilities> Abilities;

    //list of items in hero inventory
    public List<Items> Inventory;

    //equipped weapon slot
    [SerializeField]
    public Weapon weaponField;

    //equipped helmet
    [SerializeField]
    public Helmet EquippedHelmet;

    //equipped chestplate
    [SerializeField]
    public Chestplate EquippedChestplate;

    //equipped legguards
    [SerializeField]
    public Legguards EquippedLegguards;

    //equipped boots
    [SerializeField]
    public Boots EquippedBoots;

    //name of the hero
    [SerializeField]
    public string name;

    //Hero level
    [SerializeField]
    [Range(1, 99)] public int level;

    //current experience
    [SerializeField]
    public int experience;

    //experience needed to level up
    [SerializeField]
    public int experienceNeeded;

    //hero base health points
    [SerializeField]
    public int baseHP;

    //hero base mana points
    [SerializeField]
    public int baseMP;

    //hero base attack value
    [SerializeField]
    public int attack;

    //hero base strength value
    [SerializeField]
    public int strength;

    //hero base defense value
    [SerializeField]
    public int defense;

    //hero base wisdom value
    [SerializeField]
    public int wisdom;

    //hero base agility value
    [SerializeField]
    public int agility;

    //hero current health points
    [SerializeField]
    public int curHP;

    //hero current mana points
    [SerializeField]
    public int curMP;

    //hero current attack value
    [SerializeField]
    public int curATK;

    //hero current strength value
    [SerializeField]
    public int curSTR;

    //hero current defense value
    [SerializeField]
    public int curDEF;

    //hero current wisdom value
    [SerializeField]
    public int curWIS;

    //hero current agility value
    [SerializeField]
    public int curAGI;

    //hero defensive state
    public bool isDefending = false;
    //value defense increases by when defending
    public int defendingValue;

    public void Awake()
    {
        //assign stats depending on the hero current level
        baseHP = (800 / 99 * level) + 10;
        baseMP = (300 / 99 * level) + 10;
        attack = (500 / 99 * level) + 10;
        strength = (500 / 99 * level) + 10;
        defense = (500 / 99 * level) + 10;
        wisdom = (500 / 99 * level) + 10;
        agility = (500 / 99 * level) + 10;

        curHP = baseHP;
        curMP = baseMP;
        curATK = attack;
        curSTR = strength;
        curDEF = defense;
        curWIS = wisdom;
        curAGI = agility;

        experienceNeeded = (int)Mathf.Round(1000 * Mathf.Pow(level, 1.5f));

        defendingValue = defense / 10;
        LevelStepper();
    }

    public void LevelStepper()
    {
        //increases hero level depending on current experience
        if (experience > experienceNeeded)
        {
            level++;
            Awake();
            Debug.Log("Increase to level: " + level);
        }
    }
}
