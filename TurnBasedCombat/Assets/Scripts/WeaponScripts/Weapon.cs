using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ScriptableObject
{
    //unique ID of the weapon
    [SerializeField]
    public int weaponID;

    //Name of the weapon
    [SerializeField]
    public string weaponName;

    //Description of the weapon
    [SerializeField]
    public string weaponDescription;

    //sprite of the weapon, not essential
    [SerializeField]
    public Sprite weaponIcon;

    //damage value of the weapon
    [SerializeField]
    public int weaponDamage;

    //accuracy of the weapon
    [SerializeField]
    public int weaponAccuracy;

}
