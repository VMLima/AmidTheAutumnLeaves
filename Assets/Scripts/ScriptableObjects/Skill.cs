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
    public string name;
    public int[] xpToLevel;
    private int numLevels;
    private int currentLevel;
    private bool atMaxLevel;

    private SO_Skill scriptableSkill;

    public Skill(ScriptableObject _scriptableObject) : base(_scriptableObject)
    {
        scriptableSkill = (SO_Skill)_scriptableObject;
        setAmount(0);
        xpToLevel = scriptableSkill.xpToLevel;
        numLevels = xpToLevel.Length;
        currentLevel = 0;
        atMaxLevel = false;
    }
    public int getLevel()
    {
        return currentLevel;
    }

    //check if you are at max level.
    //check if your xp has dinged level up
    //BEWARE, currently overflow xp will be discarded (it will hit the cap, be thrown away, then checked for level up)
    public override int addToAmountInterim(int _amount)
    {
        if(!atMaxLevel)
        {
            addToAmount(_amount);
            if (amount >= maxStack)
            {
                levelUp();
            }
            return amount;
        }
        return 0;
    }
    public override int subToAmountInterim(int _amount)
    {
        if(!atMaxLevel)
        {
            return subToAmount(_amount);
        }
        return 0;
    }

    //advance to the next level.
    //Overflow xp is added to the next level.
    private void levelUp()
    {
        amount -= xpToLevel[currentLevel];
        if(amount < 0)
        {
            amount = 0;
        }
        currentLevel++;
        if (currentLevel >= (numLevels))
        {
            //have reached max level
            atMaxLevel = true;
        }
    }
}
