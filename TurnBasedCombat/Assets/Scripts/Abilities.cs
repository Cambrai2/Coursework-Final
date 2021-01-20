using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability", order = 56)]

public class Abilities : ScriptableObject
{

    //unique ID of the ability
    [SerializeField]
    public int id;

    //name of the ability
    [SerializeField]
    public string name;

    //Description of the ability
    [SerializeField]
    public string description;

    //mana cost of the ability
    [SerializeField]
    public int manaCost;

    //damage value of the ability
    [SerializeField]
    public int baseDamage;

    //attribute Type of the ability, currently not implemented
    [SerializeField]
    public attributeTypes attributeType;
    public enum attributeTypes
    {
        Water,
        Air,
        Earth,
        Fire
    }

    //target type of the ability
    [SerializeField]
    public targetTypes targetType;
    public enum targetTypes
    {
        soloEnemy,
        allEnemy,
        soloHero,
        allHero
    }
}
