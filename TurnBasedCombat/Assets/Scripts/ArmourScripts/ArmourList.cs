using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ArmourList : MonoBehaviour
{
    private Dictionary<int, Armour> m_ArmourMap = new Dictionary<int, Armour>();
    public List<Armour> Armour;

    private void Awake()
    {
        foreach (var armour in Armour)
        {
            m_ArmourMap.Add(armour.armourID, armour);
        }
    }

    public Armour Get(int id)
    {
        return m_ArmourMap[id];
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
