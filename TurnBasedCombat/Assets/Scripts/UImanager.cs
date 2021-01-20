using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UImanager : MonoBehaviour
{
    //battle engine reference
    public BattleEngine TurnData;

    //references for Ui objects within the scene
    public GameObject ActionPanel;
    public GameObject inventoryButtonPrefab;
    public GameObject inventoryCanvasParent;
    public GameObject InventoryItemName;
    public GameObject InventoryItemDescription;
    public GameObject HeroPanel;
    public GameObject abilityButtonPrefab;
    public GameObject abilityCanvasParent;
    public GameObject abilityName;
    public GameObject abilityDescription;
    public GameObject abilityManaCost;
    public GameObject targetEnemyButtonPrefab;
    public GameObject targetHeroButtonPrefab;
    public GameObject targetEnemyCanvasParent;

    //public List<GameObject> InventoryItems;
    public Vector3 Position;

    // Start is called before the first frame update
    void Start()
    {
        //update the hero bar ui element
        Invoke("Delay", 1);
        TurnData.HeroData = TurnData.Hero1Data;
    }

    void Delay()
    {
        HeroBarUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HeroBarUpdate()
    {
        //update each of the hero bar colours to meet the current state of that hero's hp
        Image img1 = GameObject.Find("HeroBar1").GetComponent<Image>();
        Image img2 = GameObject.Find("HeroBar2").GetComponent<Image>();
        Image img3 = GameObject.Find("HeroBar3").GetComponent<Image>();
        Image img4 = GameObject.Find("HeroBar4").GetComponent<Image>();

        if (TurnData.Hero1Data.curHP >= (TurnData.Hero1Data.baseHP/ 2))
        {
            img1.color = new Color(0, 221, 255, 255);
        }
        else if (TurnData.Hero1Data.curHP >= (TurnData.Hero1Data.baseHP / 3))
        {
            img1.color = new Color(225, 255, 0, 255);
        }
        else if (TurnData.Hero1Data.curHP >= 0)
        {
            img1.color = new Color(255, 163, 0, 255);
        }
        else if (TurnData.Hero1Data.curHP <= 0)
        {
            img1.color = new Color(255, 17, 0, 255);
        }

        if (TurnData.Hero2Data.curHP >= (TurnData.Hero2Data.baseHP / 2))
        {
            img2.color = new Color(0, 221, 255, 255);
        }
        else if (TurnData.Hero2Data.curHP >= (TurnData.Hero2Data.baseHP / 3))
        {
            img2.color = new Color(225, 255, 0, 255);
        }
        else if (TurnData.Hero2Data.curHP >= 0)
        {
            img2.color = new Color(255, 163, 0, 255);
        }
        else if (TurnData.Hero2Data.curHP <= 0)
        {
            img2.color = new Color(255, 17, 0, 255);
        }

        if (TurnData.Hero3Data.curHP >= (TurnData.Hero3Data.baseHP / 2))
        {
            img3.color = new Color(0, 221, 255, 255);
        }
        else if (TurnData.Hero3Data.curHP >= (TurnData.Hero3Data.baseHP / 3))
        {
            img3.color = new Color(225, 255, 0, 255);
        }
        else if (TurnData.Hero3Data.curHP >= 0)
        {
            img3.color = new Color(255, 163, 0, 255);
        }
        else if (TurnData.Hero3Data.curHP <= 0)
        {
            img3.color = new Color(255, 17, 0, 255);
        }

        if (TurnData.Hero4Data.curHP >= (TurnData.Hero4Data.baseHP / 2))
        {
            img4.color = new Color(0, 221, 255, 255);
        }
        else if (TurnData.Hero4Data.curHP >= (TurnData.Hero4Data.baseHP / 3))
        {
            img4.color = new Color(225, 255, 0, 255);
        }
        else if (TurnData.Hero4Data.curHP >= 0)
        {
            img4.color = new Color(255, 163, 0, 255);
        }
        else if (TurnData.Hero4Data.curHP <= 0)
        {
            img4.color = new Color(255, 17, 0, 255);
        }

        GameObject.Find("HeroBar1").GetComponentsInChildren<Text>()[0].text = TurnData.Hero1Data.name;
        GameObject.Find("HeroBar1").GetComponentsInChildren<Text>()[1].text = "HP:  " + TurnData.Hero1Data.curHP.ToString() + "/" + TurnData.Hero1Data.baseHP.ToString();
        GameObject.Find("HeroBar1").GetComponentsInChildren<Text>()[2].text = "MP:  " + TurnData.Hero1Data.curMP.ToString() + "/" + TurnData.Hero1Data.baseMP.ToString();

        GameObject.Find("HeroBar2").GetComponentsInChildren<Text>()[0].text = TurnData.Hero2Data.name;
        GameObject.Find("HeroBar2").GetComponentsInChildren<Text>()[1].text = "HP:  " + TurnData.Hero2Data.curHP.ToString() + "/" + TurnData.Hero2Data.baseHP.ToString();
        GameObject.Find("HeroBar2").GetComponentsInChildren<Text>()[2].text = "MP:  " + TurnData.Hero2Data.curMP.ToString() + "/" + TurnData.Hero2Data.baseMP.ToString();

        GameObject.Find("HeroBar3").GetComponentsInChildren<Text>()[0].text = TurnData.Hero3Data.name;
        GameObject.Find("HeroBar3").GetComponentsInChildren<Text>()[1].text = "HP:  " + TurnData.Hero3Data.curHP.ToString() + "/" + TurnData.Hero3Data.baseHP.ToString();
        GameObject.Find("HeroBar3").GetComponentsInChildren<Text>()[2].text = "MP:  " + TurnData.Hero3Data.curMP.ToString() + "/" + TurnData.Hero3Data.baseMP.ToString();

        GameObject.Find("HeroBar4").GetComponentsInChildren<Text>()[0].text = TurnData.Hero4Data.name;
        GameObject.Find("HeroBar4").GetComponentsInChildren<Text>()[1].text = "HP:  " + TurnData.Hero4Data.curHP.ToString() + "/" + TurnData.Hero4Data.baseHP.ToString();
        GameObject.Find("HeroBar4").GetComponentsInChildren<Text>()[2].text = "MP:  " + TurnData.Hero4Data.curMP.ToString() + "/" + TurnData.Hero4Data.baseMP.ToString();
    }


    public void InstantiateInventoryPrefab()
    {
        int i = 0;

        //instantiate a button for each item within the hero's inventory
        foreach (var item in TurnData.HeroData.Inventory)
        {
            Position.z = 0;
            if (i < 9)
            {
                Position.x = 110;
                Position.y = (-66 - (i * 40));
            }
            if ((i > 8) && (i < 18))
            {
                Position.x = 325;
                Position.y = (-66 - ((i - 9) * 40));
            }
            GameObject InventoryCreate = Instantiate(inventoryButtonPrefab, Position, Quaternion.identity);
            InventoryCreate.transform.SetParent(inventoryCanvasParent.transform, false);
            InventoryCreate.GetComponentsInChildren<Text>()[0].text = item.itemName;
            InventoryCreate.GetComponentsInChildren<Text>()[1].text = item.itemDescription;
            InventoryCreate.GetComponentsInChildren<Text>()[2].text = item.itemID.ToString();
            InventoryCreate.GetComponentsInChildren<Text>()[3].text = item.restoreValue.ToString();
            InventoryCreate.GetComponentsInChildren<Text>()[4].text = item.StatusEffect.ToString();
            InventoryCreate.GetComponentsInChildren<Image>()[0].sprite = item.itemIcon;
            i++;
        }

    }

    //delete the item button prefabs
    public void DeleteItemsPrefab()
    {
        GameObject[] itemBtns;
        itemBtns = GameObject.FindGameObjectsWithTag("inventoryBtn");
        foreach (GameObject itemBtn in itemBtns)
        {
            Destroy(itemBtn);
        }
    }

    //instantiate ability button prefab
    public void InstantiateAbilitiesPrefab()
    {
        int i = 0;
        foreach (var Ability in TurnData.HeroData.Abilities)
        {
            Position.z = 0;
            if (i < 4)
            {
                Position.x = 59;
                Position.y = (-24 - (i * 40));
            }
            if ((i > 3) && (i < 8))
            {
                Position.x = 174;
                Position.y = (-24 - ((i - 4) * 40));
            }
            if ((i > 7) && (i < 12))
            {
                Position.x = 289;
                Position.y = (-24 - ((i - 8) * 40));
            }
            if ((i > 11) && (i < 16))
            {
                Position.x = 404;
                Position.y = (-24 - ((i - 12) * 40));
            }
            GameObject AbilityCreate = Instantiate(abilityButtonPrefab, Position, Quaternion.identity);
            AbilityCreate.transform.SetParent(abilityCanvasParent.transform, false);
            AbilityCreate.GetComponentsInChildren<Text>()[0].text = Ability.name;
            AbilityCreate.GetComponentsInChildren<Text>()[1].text = Ability.description;
            AbilityCreate.GetComponentsInChildren<Text>()[2].text = Ability.id.ToString();
            AbilityCreate.GetComponentsInChildren<Text>()[3].text = Ability.manaCost.ToString();
            if (Ability.manaCost > TurnData.HeroData.curMP)
            {
                AbilityCreate.GetComponent<Button>().interactable = false;
            }
            i++;
        }
    }

    //instantiate target enemy button prefab
    public void InstantiateTargetEnemyPrefab()
    {
        GameObject[] Targets = GameObject.FindGameObjectsWithTag("inventoryBtn");
        foreach(GameObject target in Targets)
        {
            GameObject.Destroy(target);
        }

        int i = 0;
        
        foreach (var targetEnemy in TurnData.baseEnemies)
        {
            if (targetEnemy.enemyCurHP > 0)
            {
                Position.z = 0;
                Position.x = -7;
                Position.y = (-20 - (i * 40));
                GameObject targetEnemyCreate = Instantiate(targetEnemyButtonPrefab, Position, Quaternion.identity);
                targetEnemyCreate.transform.SetParent(targetEnemyCanvasParent.transform, false);
                targetEnemyCreate.GetComponentsInChildren<Text>()[0].text = targetEnemy.enemyName;
                targetEnemyCreate.GetComponentsInChildren<Text>()[1].text = targetEnemy.enemyCurHP.ToString();
                targetEnemyCreate.GetComponentsInChildren<Text>()[2].text = targetEnemy.enemyID.ToString();
                i++;
            }
            
        }
    }

    //instantiate target hero button prefab
    public void InstantiateTargetHeroPrefab()
    {
        int i = 0;
        foreach (var targetHero in TurnData.baseHeros)
        {
                Position.z = 0;
                Position.x = -7;
                Position.y = (-20 - (i * 40));
                GameObject targetHeroCreate = Instantiate(targetHeroButtonPrefab, Position, Quaternion.identity);
                targetHeroCreate.transform.SetParent(targetEnemyCanvasParent.transform, false);
                targetHeroCreate.GetComponentsInChildren<Text>()[0].text = targetHero.name;
                i++;
            
        }
    }

}
