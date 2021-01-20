using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryButtonManager : MonoBehaviour, IPointerEnterHandler
{
    public UImanager referenceUImanager;
    // Start is called before the first frame update
    void Start()
    {
        //assign usmanager script to reference
        referenceUImanager = GameObject.Find("BattleManager").GetComponentInChildren<UImanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //assign text values of the item to the item inspector
        Debug.Log(gameObject.GetComponentInChildren<Text>().text);
        GameObject.Find("ItemNameText").GetComponentInChildren<Text>().text = gameObject.GetComponentsInChildren<Text>()[0].text;
        GameObject.Find("ItemDescriptionText").GetComponentInChildren<Text>().text = gameObject.GetComponentsInChildren<Text>()[1].text;
        GameObject.Find("ItemImage").GetComponentInChildren<Image>().sprite = gameObject.GetComponentsInChildren<Image>()[0].sprite;
    }

    //delete item buttons when item selected
    public void DeleteItemsPrefab()
    {
        GameObject[] itemBtns;
        itemBtns = GameObject.FindGameObjectsWithTag("inventoryBtn");
        foreach(GameObject itemBtn in itemBtns)
        {
            Destroy(itemBtn);
            Debug.Log("Test");
        }
    }

    public int i;

    public void onClick()
    {
        //assign item to chosen item referencer in battlemanager script
        foreach (var item in GameObject.Find("BattleManager").GetComponentInChildren<BattleEngine>().HeroData.Inventory)
        {
            if (gameObject.GetComponentsInChildren<Text>()[0].text == item.itemName)
            {
                GameObject.Find("BattleManager").GetComponentInChildren<BattleEngine>().ChosenItem = item;
            }
        }

        //activate target enemy panel
        referenceUImanager.targetEnemyCanvasParent.SetActive(true);
        //disable inventory panel
        GameObject.Find("InventoryPanel").SetActive(false);
        //delete item prefabs
        referenceUImanager.DeleteItemsPrefab();
        //instantiate targethero prefabs
        referenceUImanager.InstantiateTargetHeroPrefab();
    }
}
