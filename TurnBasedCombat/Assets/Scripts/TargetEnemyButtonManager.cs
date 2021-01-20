using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TargetEnemyButtonManager : MonoBehaviour
{
    public int i;

    //references for multiple scripts
    public BattleEngine referenceBattleEngine;
    public UImanager referenceUImanager;
    public DamageManager referenceDamageManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTarget()
    {

        //assign scripts to the references
        referenceBattleEngine = GameObject.Find("BattleManager").GetComponentInChildren<BattleEngine>();
        referenceUImanager = GameObject.Find("BattleManager").GetComponentInChildren<UImanager>();
        referenceDamageManager = GameObject.Find("BattleManager").GetComponentInChildren<DamageManager>();

        //set a target to the chosen target reference if their name matches the name on the button
        if (gameObject.GetComponentsInChildren<Text>()[2].text == referenceBattleEngine.Enemy1Data.enemyID.ToString())
        {
            referenceBattleEngine.EnemyData = referenceBattleEngine.Enemy1Data;
        }
        else if (gameObject.GetComponentsInChildren<Text>()[2].text == referenceBattleEngine.Enemy2Data.enemyID.ToString())
        {
            referenceBattleEngine.EnemyData = referenceBattleEngine.Enemy2Data;
        }
        else if (gameObject.GetComponentsInChildren<Text>()[2].text == referenceBattleEngine.Enemy3Data.enemyID.ToString())
        {
            referenceBattleEngine.EnemyData = referenceBattleEngine.Enemy3Data;
        }
        else if (gameObject.GetComponentsInChildren<Text>()[2].text == referenceBattleEngine.Enemy4Data.enemyID.ToString())
        {
            referenceBattleEngine.EnemyData = referenceBattleEngine.Enemy4Data;
        }

        //deactivate target enemy panel
        GameObject.Find("TargetEnemyPanel").SetActive(false);

        //choose function to act on depending on whicha action
        if (referenceBattleEngine.usingAbility == true)
        {
            Debug.Log("Ability Activated");
            referenceDamageManager.heroAbilityAttackSolo();
        }
        else
        {
            Debug.Log("Attack Activated");
            referenceDamageManager.heroStandardAttack();
        }

        //activate action panel if not hero 4 turn
        if (referenceBattleEngine.HeroData != referenceBattleEngine.Hero4Data)
        {
            Invoke("Delay", 5);
        }

        Invoke("Delay1", 5);
    }

    public BaseHero targetHero;

    public void setHeroTarget()
    {
        i = 0;

        BattleEngine referenceBattleEngine = GameObject.Find("BattleManager").GetComponentInChildren<BattleEngine>();
        UImanager referenceUImanager = GameObject.Find("BattleManager").GetComponentInChildren<UImanager>();
        DamageManager referenceDamageManager = GameObject.Find("BattleManager").GetComponentInChildren<DamageManager>();

        var item = referenceBattleEngine.ChosenItem;

        foreach (var hero in referenceBattleEngine.baseHeros)
        {
            if (hero.name == gameObject.GetComponentsInChildren<Text>()[0].text)
            {
                targetHero = hero;
            }
        }

        switch (item.StatusEffect.ToString())
        {
            case "REVIVE":
                if (targetHero.curHP <= 0)
                {
                    targetHero.curHP += item.restoreValue;
                    Debug.Log(targetHero.name + "has been revived!");
                }
                else
                {
                    Debug.Log(item.itemName + " had no effect!");
                }
                break;
            case "HEALTH":
                if (targetHero.curHP > 0 && targetHero.curHP > targetHero.baseHP)
                {
                    targetHero.curHP += item.restoreValue;
                    Debug.Log(targetHero.name + "has been healed!");
                }
                else
                {
                    Debug.Log(item.itemName + " had no effect!");
                }
                break;
            case "MANA":
                if (targetHero.curHP > 0 && targetHero.curMP > targetHero.baseMP)
                {
                    targetHero.curMP += item.restoreValue;
                    Debug.Log(targetHero.name + "has been given MP!");
                }
                else
                {
                    Debug.Log(item.itemName + " had no effect!");
                }
                break;
            case "MULTIRESTORE":
                if (targetHero.curHP > 0)
                {
                    targetHero.curHP += item.restoreValue;
                    targetHero.curMP += item.restoreValue;
                    Debug.Log(targetHero.name + "has been healed and given MP!");
                    if (targetHero.curHP > targetHero.baseHP)
                    {
                        targetHero.curHP = targetHero.baseHP;
                    }
                    if (targetHero.curMP > targetHero.baseMP)
                    {
                        targetHero.curMP = targetHero.baseMP;
                    }
                }
                else
                {
                    Debug.Log(item.itemName + " had no effect!");
                }
                break;
        }

        foreach (var inventoryItem in referenceBattleEngine.HeroData.Inventory)
        {
            
            if (inventoryItem.itemName == item.itemName)
            {
                referenceBattleEngine.HeroData.Inventory.RemoveAt(i);
                break;
            }

            i++;
        }

        if (referenceBattleEngine.HeroData != referenceBattleEngine.Hero4Data)
        {
            Invoke("Delay", 1);
            referenceUImanager.ActionPanel.SetActive(true);
        }

        referenceUImanager.DeleteItemsPrefab();
        GameObject.Find("TargetEnemyPanel").SetActive(false);
        referenceBattleEngine.StateControl();
    }
        

    void Delay()
    {
        Debug.Log("Delay Working");

        //activate hero action panel
        referenceUImanager.ActionPanel.SetActive(true);

        //delete button prefabs
        referenceUImanager.DeleteItemsPrefab();
    }

    void Delay1()
    {
        //cycle turn
        referenceBattleEngine.StateControl();
    }
}
