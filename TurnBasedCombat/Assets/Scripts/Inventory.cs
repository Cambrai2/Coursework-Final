using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory", order = 53)]
public class Inventory : ScriptableObject
{
	public List<Items> items;
}
