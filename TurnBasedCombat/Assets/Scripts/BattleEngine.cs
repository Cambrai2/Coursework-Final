using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BattleEngine : MonoBehaviour
{

    public Animator anim1;
    public Animator anim2;
    public Animator anim3;
    public Animator anim4;
    public Animator animE1;
    public Animator animE2;
    public Animator animE3;
    public Animator animE4;
    //Damage manager script reference
    public DamageManager damageScript;

    //Game Hero references
    public BaseHero Hero1Data;
    public BaseHero Hero2Data;
    public BaseHero Hero3Data;
    public BaseHero Hero4Data;

    //current hero referencer
    public BaseHero HeroData;

    //list of heroes in scene
    public List<BaseHero> baseHeros = new List<BaseHero>();

    //Game Enemy references
    public BaseEnemy Enemy1Data;
    public BaseEnemy Enemy2Data;
    public BaseEnemy Enemy3Data;
    public BaseEnemy Enemy4Data;

    //current enemy referencer
    public BaseEnemy EnemyData;

    //list of enemies in scene
    public List<BaseEnemy> baseEnemies = new List<BaseEnemy>();

    //Enemy prefab reference
    public GameObject EnemyCharPrefab;

    //chosen ability reference
    public Abilities ChosenAbility;

    //chosen item reference
    public Items ChosenItem;

    //boolean holding action state
    public bool usingAbility = false;

    //Current fight state values
    public enum FightState {
        ENTERFIGHT,
        HERO1,
        HERO2,
        HERO3,
        HERO4,
        ENEMY1,
        ENEMY2,
        ENEMY3,
        ENEMY4,
        END
    }
    public FightState FightStates;

    //Textbox printout
    public Text ChatBox;


    void Start()
    {
        //assign values depending on hero and enemy levels
        Hero1Data.Awake();
        Hero2Data.Awake();
        Hero3Data.Awake();
        Hero4Data.Awake();
        Enemy1Data.Awake();
        Enemy2Data.Awake();
        Enemy3Data.Awake();
        Enemy4Data.Awake();

        //begin turn cycler
        FightStates = FightState.ENTERFIGHT;
        StateControl();

        //add assigned heros and enemies to their respective lists
        if(Hero1Data != null)
        {
            baseHeros.Add(Hero1Data);
        }
        if (Hero2Data != null)
        {
            baseHeros.Add(Hero2Data);
        }
        if (Hero3Data != null)
        {
            baseHeros.Add(Hero3Data);
        }
        if (Hero4Data != null)
        {
            baseHeros.Add(Hero4Data);
        }

        if (Enemy1Data != null)
        {
            baseEnemies.Add(Enemy1Data);
        }
        if (Enemy2Data != null)
        {
            baseEnemies.Add(Enemy2Data);
        }
        if (Enemy3Data != null)
        {
            baseEnemies.Add(Enemy3Data);
        }
        if (Enemy4Data != null)
        {
            baseEnemies.Add(Enemy4Data);
        }

        //instantiate enemy prefabs
        InstantiateEnemies();

        //cycle turn
        Invoke("Delay", 1);

    }

    void update()
    {

    }

    //turn cycler
    void Delay()
    {
        StateControl();
    }

    //clears the chat box of text
    void ChatClear()
    {
        //ChatBox.text = " ";
    }

    //set ability action check to false
    public void setUsingAbilityToFalse()
    {
        usingAbility = false;
    }

    //check if enemies are dead
    public void allDeadCheck()
    {

        //end the fight if all enemies HP is 0 or less
        if ((Enemy1Data.enemyCurHP <= 0) && (Enemy2Data.enemyCurHP <= 0) && (Enemy3Data.enemyCurHP <= 0) && (Enemy4Data.enemyCurHP <= 0))
        {
            FightWin();
            FightStates = FightState.END;
            StateControl();
        }
    }

    //end the fight if flee is successful
    public void Flee()
    {
        FightStates = FightState.END;
        StateControl();
    }

    //turn state switch statement.
    public void StateControl()
    {

        switch (FightStates)
        {

            case (FightState.ENTERFIGHT):
                Debug.Log("Fight has been entered!");
                //set turn to hero1
                FightStates = FightState.HERO1;
                break;

            case (FightState.HERO1):

                //check for dead
                allDeadCheck();

                //set turn to hero2
                FightStates = FightState.HERO2;
                if (Hero1Data.curHP > 0)
                {
                    HeroData = Hero1Data;
                    ChatBox.text = "It is now " + Hero1Data.name + "'s turn!";
                }
                else
                {
                    StateControl();
                    ChatBox.text = Hero1Data.name + " has fainted and cannot fight!";

                }
                
                //clear chatbox
                Invoke("ChatClear", 5);
                break;

            case (FightState.HERO2):
                
                //check for dead
                allDeadCheck();

                //set turn to hero3
                FightStates = FightState.HERO3;
                if (Hero2Data.curHP > 0)
                {
                    HeroData = Hero2Data;
                    ChatBox.text = "It is now " + Hero2Data.name + "'s turn!";

                }
                else
                {
                    StateControl();
                    ChatBox.text = Hero2Data.name + " has fainted and cannot fight!";

                }
                Invoke("ChatClear", 5);
                break;

            case (FightState.HERO3):

                //check for dead
                allDeadCheck();

                //set turn to hero4
                FightStates = FightState.HERO4;
                if (Hero3Data.curHP > 0)
                {
                    HeroData = Hero3Data;
                    ChatBox.text = "It is now " + Hero3Data.name + "'s turn!";

                }
                else
                {
                    StateControl();
                    ChatBox.text = Hero3Data.name + " has fainted and cannot fight!";

                }
                Invoke("ChatClear", 5);
                break;

            case (FightState.HERO4):

                //check for dead
                allDeadCheck();

                //set turn to enemy1
                FightStates = FightState.ENEMY1;
                if (Hero4Data.curHP > 0)
                {
                    HeroData = Hero4Data;
                    ChatBox.text = "It is now " + Hero4Data.name + "'s turn!";

                }
                else
                {
                    GameObject.Find("BattleManager").GetComponentInChildren<UImanager>().ActionPanel.SetActive(false);
                    StateControl();
                    ChatBox.text = Hero4Data.name + " has fainted and cannot fight!";
                   
                }
                Invoke("ChatClear", 5);
                break;

            case (FightState.ENEMY1):

                //check for dead
                allDeadCheck();

                if (Enemy1Data.enemyCurHP > 0)
                {
                    ChatBox.text = "It is now " + Enemy1Data.name + "'s turn!";
                    EnemyData = Enemy1Data;
                    damageScript.EnemyChooseAndAttack();
                }
                else
                {
                    ChatBox.text = Enemy1Data.enemyName + " has fainted and cannot fight!";
                }

                //set turn to enemy2
                FightStates = FightState.ENEMY2;

                Invoke("ChatClear", 5);
                Invoke("Delay", 5);
                break;
            case (FightState.ENEMY2):

                //check for dead
                allDeadCheck();

                if (Enemy2Data.enemyCurHP > 0)
                {
                    ChatBox.text = "It is now " + Enemy2Data.name + "'s turn!";
                    EnemyData = Enemy2Data;
                    damageScript.EnemyChooseAndAttack();
                }
                else
                {
                    ChatBox.text = Enemy2Data.enemyName + " has fainted and cannot fight!";
                }

                //set turn to enemy3
                FightStates = FightState.ENEMY3;

                Invoke("ChatClear", 5);
                Invoke("Delay", 5);
                break;

            case (FightState.ENEMY3):

                //check for dead
                allDeadCheck();

                if (Enemy3Data.enemyCurHP > 0)
                {
                    ChatBox.text = "It is now " + Enemy3Data.name + "'s turn!";
                    EnemyData = Enemy3Data;
                    damageScript.EnemyChooseAndAttack();
                }
                else
                {
                    ChatBox.text = Enemy3Data.enemyName + " has fainted and cannot fight!";
                }

                //set turn to enemy4
                FightStates = FightState.ENEMY4;

                Invoke("ChatClear", 5);
                Invoke("Delay", 5);
                break;

            case (FightState.ENEMY4):

                //check for dead
                allDeadCheck();

                if (Enemy4Data.enemyCurHP > 0)
                {
                    ChatBox.text = "It is now " + Enemy4Data.name + "'s turn!";
                    EnemyData = Enemy4Data;
                    damageScript.EnemyChooseAndAttack();
                }
                else
                {
                    ChatBox.text = Enemy4Data.enemyName + " has fainted and cannot fight!";
                }

                //set turn to hero1
                FightStates = FightState.HERO1;

                //enable user action panel
                Invoke("PanelLoad", 5);
                Invoke("ChatClear", 5);
                Invoke("Delay", 5);
                break;

            case (FightState.END):

                //load external scene when fight ends
                Invoke("DelayEnd", 3);
                break;

        }
    }

    public void DelayEnd()
    {
        SceneManager.LoadScene(sceneName: "EnvironmentScene");
    }
    public void PanelLoad()
    {
        GameObject.Find("BattleManager").GetComponentInChildren<UImanager>().ActionPanel.SetActive(true);
    }

    //instantiate enemy prefabs
    public void InstantiateEnemies()
    {

        //instantiate enemy prefab for each enemy
        float i = 0;
        foreach (var enemyChar in baseEnemies)
        {
            if (enemyChar.enemyCurHP > 0)
            {
                GameObject EnemyCharCreate = Instantiate(EnemyCharPrefab);
                EnemyCharCreate.transform.position = new Vector3(-3f + (i * 2f), 0f, 5f);
                
                
            }

            i++;
        }
        GameObject[] enemyPrefabs = GameObject.FindGameObjectsWithTag("enemyPrefab");
        animE1 = enemyPrefabs[0].GetComponentInChildren<Animator>();
        animE2 = enemyPrefabs[1].GetComponentInChildren<Animator>();
        animE3 = enemyPrefabs[2].GetComponentInChildren<Animator>();
        animE4 = enemyPrefabs[3].GetComponentInChildren<Animator>();

    }

    public void DieAnimEnemies()
    {
        if (Enemy1Data.enemyCurHP <= 0)
        {
            animE1.SetTrigger("Die");
        }
        if (Enemy2Data.enemyCurHP <= 0)
        {
            animE2.SetTrigger("Die");
        }
        if (Enemy3Data.enemyCurHP <= 0)
        {
            animE3.SetTrigger("Die");
        }
        if (Enemy4Data.enemyCurHP <= 0)
        {
            animE4.SetTrigger("Die");
        }
    }

    public void DieAnimHeroes()
    {
        if (Hero1Data.curHP <= 0)
        {
            anim1.SetTrigger("Die");
        }
        if (Hero2Data.curHP <= 0)
        {
            anim2.SetTrigger("Die");
        }
        if (Hero3Data.curHP <= 0)
        {
            anim3.SetTrigger("Die");
        }
        if (Hero4Data.curHP <= 0)
        {
            anim4.SetTrigger("Die");
        }

        if (Hero1Data.curHP + Hero2Data.curHP + Hero3Data.curHP + Hero4Data.curHP <= 0)
        {
            FightStates = FightState.END;
            StateControl();
        }
    }

    public int totalXP;

    //fight won function
    public void FightWin()
    {
        //grant xp from the enemy and chance for drops
        foreach (var enemy in baseEnemies)
        {
            totalXP += enemy.experienceGranted;
            int randomVal = Random.Range(1, 100);
            
            if ((randomVal >= 95) && (enemy.rareDropTable.Count != 0))
            {
                //Rare Drop
                randomVal = Random.Range(0, enemy.rareDropTable.Count);
                Hero1Data.Inventory.Add(enemy.rareDropTable[randomVal]);
                Debug.Log(enemy.enemyName + " dropped a " + enemy.rareDropTable[randomVal].itemName + "!");
            }
            else if ((randomVal >= 75) && (enemy.commonDropTable.Count != 0))
            {
                //common drop
                randomVal = Random.Range(0, enemy.commonDropTable.Count);
                Hero1Data.Inventory.Add(enemy.commonDropTable[randomVal]);
                Debug.Log(enemy.enemyName + " dropped a " + enemy.commonDropTable[randomVal].itemName + "!");
            }
        }

        //award xp to heroes
        Debug.Log("The team has recieved " + totalXP + "xp!");
        Hero1Data.experience += totalXP;
        Hero2Data.experience += totalXP;
        Hero3Data.experience += totalXP;
        Hero4Data.experience += totalXP;

    }

    public void FightFlee()
    {

    }
}

