using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    //script references
    public BattleEngine targetControl;
    public UImanager UIdetails;

    public float Damage;
    public float DamageMultiplier;

    //stat placeholders
    public int HPsum;
    public int HPsum1;
    public int HPsum2;
    public int HPsum3;
    public int HPsum4;
    public int HeroCurSTR;
    public int HeroCurATK;
    public int HeroCurWIS;
    public int HeroCurDEF;
    public int HeroCurAGI;
    public int EnemyCurDEF;
    public int EnemyCurAGI;
    public int AbilityChoice;
    public int weaponDamage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void ValGrab()
     {
        //assign values to temporary value holders
        HeroCurSTR = targetControl.HeroData.curSTR;
        HeroCurATK = targetControl.HeroData.curATK;
        HeroCurWIS = targetControl.HeroData.curWIS;
        HeroCurDEF = targetControl.HeroData.curDEF;
        HeroCurAGI = targetControl.HeroData.curAGI;

        EnemyCurDEF = targetControl.EnemyData.enemyCurDEF;
        EnemyCurAGI = targetControl.EnemyData.enemyCurAGI;

        DamageMultiplier = ((float)Random.Range(75, 125)) / 100;

        if (targetControl.HeroData.weaponField != null)
        {
            weaponDamage += targetControl.HeroData.weaponField.weaponDamage;
        }
        else
        {
            weaponDamage = 0;
        }

        if (targetControl.ChosenAbility != null)
        {
            AbilityChoice = targetControl.ChosenAbility.baseDamage;
        }
        else
        {
            AbilityChoice = 0;
        }

    }

    public void EnemyAttackAnim()
    {
        if (targetControl.EnemyData == targetControl.Enemy1Data)
        {
            targetControl.animE1.SetTrigger("Melee");
        }
        if (targetControl.EnemyData == targetControl.Enemy2Data)
        {
            targetControl.animE2.SetTrigger("Melee");
        }
        if (targetControl.EnemyData == targetControl.Enemy3Data)
        {
            targetControl.animE3.SetTrigger("Melee");
        }
        if (targetControl.EnemyData == targetControl.Enemy4Data)
        {
            targetControl.animE4.SetTrigger("Melee");
        }
    }
    //called on standard attack
    public void heroStandardAttack()
    {

        ValGrab();

        if (targetControl.HeroData == targetControl.Hero1Data)
        {
            targetControl.anim1.SetTrigger("Melee");
        }
        if (targetControl.HeroData == targetControl.Hero2Data)
        {
            targetControl.anim2.SetTrigger("Melee");
        }
        if (targetControl.HeroData == targetControl.Hero3Data)
        {
            targetControl.anim3.SetTrigger("Melee");
        }
        if (targetControl.HeroData == targetControl.Hero4Data)
        {
            targetControl.anim4.SetTrigger("Melee");
        }
        //calculate damage
        Damage = (((HeroCurSTR + weaponDamage) * (HeroCurSTR + weaponDamage)) / ((HeroCurSTR + weaponDamage) + (EnemyCurDEF)));
        Damage *= DamageMultiplier;
        Damage = Mathf.Floor(Damage);

        //calculate hit chance and deal damage
        int HitChance = Random.Range(1, 10);
        if (HitChance > 9)
        {

            targetControl.ChatBox.text = targetControl.HeroData.name + " missed!";
        }
        else
        {
            //deal damage
            targetControl.EnemyData.enemyCurHP -= (int)Damage;

            targetControl.ChatBox.text = targetControl.HeroData.name + " has attacked " + targetControl.EnemyData.enemyName + " for " + Damage + "!";
        }

        if (targetControl.EnemyData.enemyCurHP <= 0)
        {

            targetControl.ChatBox.text = targetControl.EnemyData.enemyName + " has fainted!";

            targetControl.DieAnimEnemies();
        }
    }

    //called on ability use
    public void heroAbilityAttackSolo()
    {
        ValGrab();

        //calculate damage
        Damage = (((HeroCurWIS + AbilityChoice) * (HeroCurWIS + AbilityChoice)) / ((HeroCurWIS + AbilityChoice) + (EnemyCurDEF)));
        Damage *= DamageMultiplier;
        Damage = Mathf.Floor(Damage);

        //deal damage
        targetControl.EnemyData.enemyCurHP -= (int)Damage;

        targetControl.ChatBox.text = targetControl.HeroData.name + " has using an ability and hit " + targetControl.EnemyData.enemyName + " for " + Damage + "!";

        //minus mana cost from current mana
        targetControl.HeroData.curMP -= targetControl.ChosenAbility.manaCost;
        if (targetControl.EnemyData.enemyCurHP <= 0)
        {

            targetControl.ChatBox.text = targetControl.EnemyData.enemyName + " has fainted!";

            targetControl.DieAnimEnemies();
        }

        //update UI bar
        UIdetails.HeroBarUpdate();
    }

    //called when defending
    public void heroDefend()
    {
        //decrease defending value if defended last turn
        targetControl.ChatBox.text = (targetControl.HeroData.name + " chose to defend!");
        if (targetControl.HeroData.isDefending == true)
        {
            targetControl.HeroData.curDEF -= targetControl.HeroData.defendingValue;
        }

        //increase def by defending value
        targetControl.HeroData.curDEF += targetControl.HeroData.defendingValue;
        targetControl.HeroData.isDefending = true;

        if (targetControl.HeroData.name != targetControl.Hero4Data.name)
        {
            Invoke("Delay", 1);
        }

        targetControl.StateControl();

    }

    public void heroFlee()
    {
        //calculate flee chance
        Damage = HeroCurAGI + (targetControl.Enemy1Data.enemyCurAGI + targetControl.Enemy2Data.enemyCurAGI + targetControl.Enemy3Data.enemyCurAGI + targetControl.Enemy4Data.enemyCurAGI)/4;
        DamageMultiplier = Random.Range(0, Damage);
        if (DamageMultiplier < HeroCurAGI)
        {
            //Flee successful
            UIdetails.ActionPanel.SetActive(false);
            targetControl.ChatBox.text = targetControl.HeroData.name + " fleed successfully!";
            targetControl.Flee();
        }
        else
        {
            //Flee unsuccessful
            UIdetails.ActionPanel.SetActive(false);
            targetControl.ChatBox.text = targetControl.HeroData.name + " fleed successfully!";
            targetControl.StateControl();
            Invoke("Delay", 1);
        }
    }

    public void Delay()
    {
        UIdetails.ActionPanel.SetActive(true);
    }

    public void EnemyChooseAndAttack()
    {

        //raise hero hp sum to 0 if below
        HPsum = 0;
        foreach (var Hero in targetControl.baseHeros)
        {
            if (Hero.curHP < 0)
            {
                Hero.curHP = 0;
            }
            HPsum += Hero.curHP;
        }
        if (targetControl.Hero1Data.curHP < 1)
        {
            HPsum1 = 0;
        }
        else
        {
            HPsum1 = HPsum / targetControl.Hero1Data.curHP;
        }
        if (targetControl.Hero2Data.curHP < 1)
        {
            HPsum2 = 0;
        }
        else
        {
            HPsum2 = HPsum / targetControl.Hero2Data.curHP;
        }
        if (targetControl.Hero3Data.curHP < 1)
        {
            HPsum3 = 0;
        }
        else
        {
            HPsum3 = HPsum / targetControl.Hero3Data.curHP;
        }
        if (targetControl.Hero4Data.curHP < 1)
        {
            HPsum4 = 0;
        }
        else
        {
            HPsum4 = HPsum / targetControl.Hero4Data.curHP;
        }

        //assign hero to chosen hero reference
        var TargetSum = Random.Range(1, (HPsum1 + HPsum2 + HPsum3 + HPsum4));
        if (TargetSum > 0 && TargetSum <= (HPsum1))
        {
            //Target Hero1
            Debug.Log("Hero1 Targeted!");
            targetControl.HeroData = targetControl.Hero1Data;
        }
        else if(TargetSum > HPsum1 && TargetSum <= (HPsum1 + HPsum2)){
            //Target Hero2
            Debug.Log("Hero2 Targeted!");
            targetControl.HeroData = targetControl.Hero2Data;
        }
        else if (TargetSum > (HPsum1 + HPsum2) && TargetSum <= (HPsum1 + HPsum2 + HPsum3)){
            //Target Hero3
            Debug.Log("Hero3 Targeted!");
            targetControl.HeroData = targetControl.Hero3Data;
        }
        else if (TargetSum > (HPsum1 + HPsum2 + HPsum3) && TargetSum <= (HPsum1 + HPsum2 + HPsum3 + HPsum4))
        {
            //Target Hero4
            Debug.Log("Hero4 Targeted!");
            targetControl.HeroData = targetControl.Hero4Data;
        }

        //get defence values from armour pieces
        int armourDef = 0;
        if (targetControl.HeroData.EquippedHelmet != null)
        {
            armourDef += targetControl.HeroData.EquippedHelmet.armourDefence;
        }
        if (targetControl.HeroData.EquippedChestplate != null)
        {
            armourDef += targetControl.HeroData.EquippedChestplate.armourDefence;
        }
        if (targetControl.HeroData.EquippedLegguards != null)
        {
            armourDef += targetControl.HeroData.EquippedLegguards.armourDefence;
        }
        if (targetControl.HeroData.EquippedBoots != null)
        {
            armourDef += targetControl.HeroData.EquippedBoots.armourDefence;
        }

        //roll to choose which action to take by the enemy
        var choiceVal = Random.Range(1, 100);
        if (choiceVal > 0 && choiceVal < 61)
        {
            choiceVal = 1;
        }
        else if (choiceVal > 60 && choiceVal < 85)
        {
            choiceVal = 2;
        }
        else if (choiceVal > 84 && choiceVal < 96)
        {
            choiceVal = 3;
        }
        else if (choiceVal > 95 && choiceVal <= 100)
        {
            choiceVal = 4;
        }
        switch (choiceVal)
        {
            case 1:
                //standard attack
                targetControl.ChatBox.text = ("Enemy Chose to Standard Attack!");

                //calculate damage value of the attack
                DamageMultiplier = ((float)Random.Range(75, 125))/100;
                Damage = (((targetControl.EnemyData.enemyCurATK) * (targetControl.EnemyData.enemyCurATK)) / ((targetControl.EnemyData.enemyCurATK) + (targetControl.HeroData.curDEF + armourDef)));
                Damage *= DamageMultiplier;
                Damage = Mathf.Floor(Damage);


                //calculate miss chance and dead damage appropriately
                int HitChance = Random.Range(1, 10);
                if (HitChance > 8)
                {
                    targetControl.ChatBox.text = (targetControl.EnemyData.enemyName + " missed!");
                }
                else
                {
                    targetControl.ChatBox.text = (targetControl.EnemyData.enemyName + " Has attacked " + targetControl.HeroData.name + " for " + Damage + "!");

                    targetControl.HeroData.curHP -= (int)Damage;
                }

                //hero hp check
                if (targetControl.HeroData.curHP < 0)
                {
                    targetControl.HeroData.curHP = 0;
                }

                EnemyAttackAnim();
                break;

            case 2:
                //ability use
                targetControl.ChatBox.text = ("Enemy Chose To Use An Ability!");

                //choosed random ability from list of abilities
                int numberOfAbilities = targetControl.EnemyData.Abilities.Count;
                if (numberOfAbilities != 0)
                {
                    int i = (Random.Range(0, numberOfAbilities));
                    if (targetControl.EnemyData.Abilities[i].manaCost < targetControl.EnemyData.enemyCurMP)
                    {
                        //calculate damage value of the attack
                        DamageMultiplier = ((float)Random.Range(75, 125)) / 100;
                        Damage = (((targetControl.EnemyData.enemyCurWIS + targetControl.EnemyData.Abilities[i].baseDamage) * (targetControl.EnemyData.enemyCurWIS + targetControl.EnemyData.Abilities[i].baseDamage)) / ((targetControl.EnemyData.enemyCurWIS + targetControl.EnemyData.Abilities[i].baseDamage) + (targetControl.HeroData.curDEF)));
                        Damage *= DamageMultiplier;
                        Damage = Mathf.Floor(Damage);

                        //deal damage to target hero
                        targetControl.HeroData.curHP -= (int)Damage;
                        targetControl.EnemyData.enemyCurMP -= targetControl.EnemyData.Abilities[i].manaCost;
                    }
                    else
                    {
                        targetControl.ChatBox.text = (targetControl.EnemyData.enemyName + " does not have enough mana to cast " + targetControl.EnemyData.Abilities[i].name);
                    }

                    if (targetControl.HeroData.curHP < 0)
                    {
                        targetControl.HeroData.curHP = 0;
                    }
                }
                else
                {
                    targetControl.ChatBox.text = (targetControl.EnemyData.enemyName + " has not learnt any abilties!");
                }
                
                break;

            case 3:
                //defend
                targetControl.ChatBox.text = ("Enemy Chose to Defend!");

                //raise defence value when defending or remove defence bonus when finished defending
                if (targetControl.EnemyData.isDefending == true)
                {
                    targetControl.EnemyData.enemyCurDEF -= targetControl.EnemyData.defendingValue;
                }
                targetControl.EnemyData.enemyCurDEF += targetControl.EnemyData.defendingValue;
                targetControl.EnemyData.isDefending = true;

                break;
            case 4:
                //flee
                targetControl.ChatBox.text = ("Enemy Chose to Try and Flee!");
                Damage = targetControl.EnemyData.enemyCurAGI + (targetControl.Hero1Data.curAGI + targetControl.Hero2Data.curAGI + targetControl.Hero3Data.curAGI + targetControl.Hero4Data.curAGI) / 4;
                DamageMultiplier = Random.Range(0, Damage);
                if (DamageMultiplier < targetControl.EnemyData.enemyCurAGI)
                {
                    //Flee successful
                    targetControl.ChatBox.text = (targetControl.EnemyData.name + " fleed successfully!");
                    Debug.Log("Enemy Flee");
                    targetControl.Flee();
                }
                else
                {
                    //Flee unsuccessful
                    targetControl.ChatBox.text = (targetControl.EnemyData.name + " failed to flee!");
                    targetControl.StateControl();
                    Invoke("Delay", 1);
                }
                break;
        }
        UIdetails.HeroBarUpdate();
        targetControl.DieAnimHeroes();

    }

}
