using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class....
///     inherits from Incremental, which handles the addition, subtraction compared to min/max values
///     has a list holding xp needed to ding next level.
///         upon reaching next level, xp goes to 0.
///     sets the max number of levels based on the length of xpToLevel in the scriptable object.
///     will not add/remove xp if at max level.
///     
///     CURRENTLY-overflow xp will be discarded.
/// </summary>
public class Skill : Incremental
{
    public int[] xpToLevel;
    private int numLevels;

    public SO_Skill soSkill;

    public Skill(SO_Basic _scriptableObject) : base(_scriptableObject)
    {
        soSkill = (SO_Skill)_scriptableObject;
        setAmount(0);
        xpToLevel = soSkill.xpToLevel;
        numLevels = xpToLevel.Length;
        Debug.Log("Creating Skill named:" + soSkill.nameTag);
    }
    public int getLevel()
    {
        return soSkill.currentLevel;
    }

    //check if you are at max level.
    //check if your xp has dinged level up
    //BEWARE, currently overflow xp will be discarded (it will hit the cap, be thrown away, then checked for level up)
    public override int addToAmountInterim(int _amount)
    {
        Debug.Log("addToAmountInterim: start");
        if (!soSkill.maxLevel)
        {
            addToAmount(_amount);
            if (soSkill.amount >= soSkill.maxStack)
            {
                levelUp();
            }
            return soSkill.amount;
        }
        return 0;
    }
    public override int subToAmountInterim(int _amount)
    {
        if(!soSkill.maxLevel)
        {
            return subToAmount(_amount);
        }
        return 0;
    }

    //advance to the next level.
    //Overflow xp is added to the next level.
    public void levelUp()
    {
        soSkill.amount -= xpToLevel[soSkill.currentLevel];
        if(soSkill.amount < 0)
        {
            soSkill.amount = 0;
        }
        soSkill.currentLevel++;
        soSkill.maxStack = xpToLevel[soSkill.currentLevel];
        if (soSkill.currentLevel >= (numLevels))
        {
            //have reached max level
            soSkill.maxLevel = true;
        }
        Debug.Log("Skill:levelUp:" + soSkill.nameTag);
        SkillManager.instance.skillLevelEvent.Invoke();
    }
}
