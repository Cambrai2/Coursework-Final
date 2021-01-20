using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : ScriptableObject
{
    //Unique ID for the piece of armour
    [SerializeField]
    public int armourID;

    //Name of the armour
    [SerializeField]
    public string armourName;

    //Description of the armour
    [SerializeField]
    public string armourDescription;

    //Sprite of the armour piece, not essential
    [SerializeField]
    public Sprite armourIcon;

    //defence value of the armour
    [SerializeField]
    public int armourDefence;
}
