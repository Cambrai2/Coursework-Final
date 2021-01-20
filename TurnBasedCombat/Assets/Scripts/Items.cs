using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 53)]
public class Items : ScriptableObject
{
    //unique item ID
    [SerializeField]
    public int itemID;

    //name of the item
    [SerializeField]
    public string itemName;

    //description of item
    [SerializeField]
    public string itemDescription;

    //sprite of the item
    [SerializeField]
    public Sprite itemIcon;

    //restore value of item
    [SerializeField]
    public int restoreValue;

    //item type
    [SerializeField]
    public statusEffect StatusEffect;
    public enum statusEffect
    {
        REVIVE,
        HEALTH,
        MANA,
        MULTIRESTORE
    }
}
