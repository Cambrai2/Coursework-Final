using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class WeaponList : MonoBehaviour
{
    private Dictionary<int, Weapon> m_WeaponMap = new Dictionary<int, Weapon>();
    public List<Weapon> Weapon;

    private void Awake()
    {
        foreach (var weapon in Weapon)
        {
            m_WeaponMap.Add(weapon.weaponID, weapon);
        }
    }

    public Weapon Get(int id)
    {
        return m_WeaponMap[id];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
