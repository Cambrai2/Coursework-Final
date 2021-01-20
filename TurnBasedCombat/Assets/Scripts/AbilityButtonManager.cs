using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityButtonManager : MonoBehaviour, IPointerEnterHandler
{
    //Battle Engine script reference
    public BattleEngine referenceBattleEngine;

    //UImanager script reference
    public UImanager referenceUImanager;

    // Start is called before the first frame update
    void Start()
    {
        //assignment of script references
        referenceBattleEngine = GameObject.Find("BattleManager").GetComponentInChildren<BattleEngine>();
        referenceUImanager = GameObject.Find("BattleManager").GetComponentInChildren<UImanager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //assignment of prefab button values to UI text boxes
        GameObject.Find("AbilityName").GetComponentInChildren<Text>().text = gameObject.GetComponentsInChildren<Text>()[0].text;
        GameObject.Find("AbilityDescription").GetComponentInChildren<Text>().text = gameObject.GetComponentsInChildren<Text>()[1].text;
        GameObject.Find("AbilityDetails").GetComponentInChildren<Text>().text = "MP: " + gameObject.GetComponentsInChildren<Text>()[3].text + " / " + referenceBattleEngine.HeroData.curMP.ToString();
    }

    //Called when a UI ability button is selected
    public void selectAbility()
    {
        //temporary variable holding the ability name
        string nameOfAbility = GameObject.Find("AbilityName").GetComponentInChildren<Text>().text;

        //foreach statement to find the ability of the button pressed
        foreach (var Ability in referenceBattleEngine.HeroData.Abilities)
        {
            if (nameOfAbility == Ability.name)
            {
                referenceBattleEngine.ChosenAbility = Ability;
                Debug.Log("targetAbility is now: " + referenceBattleEngine.ChosenAbility.name);
                referenceBattleEngine.usingAbility = true;
            }
        }
        
        //enable enemy targeting canvas
        referenceUImanager.targetEnemyCanvasParent.SetActive(true);

        //disable ability selection canvas
        GameObject.Find("AbilitiesPanel").SetActive(false);

        //delete instantiated ability buttons
        referenceUImanager.DeleteItemsPrefab();

        //instantiate target enemy buttons
        referenceUImanager.InstantiateTargetEnemyPrefab();


    }

}
