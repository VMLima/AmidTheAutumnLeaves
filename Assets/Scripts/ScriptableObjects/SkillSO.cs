using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// data storage and easy creation for skills.
///     stores key elements.
///     values persist between runs? need to use reset() on start game to reset values (can work well with saves?)
/// </summary>

[CreateAssetMenu(fileName = "NewSkill", menuName = "Scriptable Object/Basic/Skill")]
public class SkillSO : IncrementableSO
{
    public int[] xpToLevel;

    [HideInInspector]
    public int currentLevel;
    [HideInInspector]
    public bool atMax;

    private int maxLevel;
    //UI to be added to
    //placement order in UI

    public override void reset()
    {
        base.reset();
        currentLevel = 0;
        atMax = false;
        maxLevel = xpToLevel.Length;
    }

    public override void whenUnlocked()
    {
        //active skill UI
    }

    public int getLevel()
    {
        return currentLevel;
    }

    //check if you are at max level.
    //check if your xp has dinged level up
    //BEWARE, currently overflow xp will be discarded (it will hit the cap, be thrown away, then checked for level up)
    public override void addToAmountOverride(float _amount)
    {
        if (!atMax)
        {
            addToAmount(_amount);
            if (amount >= maxStack)
            {
                levelUp();
            }
        }
    }
    public override void subToAmountOverride(float _amount)
    {
        if (!atMax)
        {
            subToAmount(_amount);
        }
    }

    //advance to the next level.
    //Overflow xp is added to the next level.
    public void levelUp()
    {
        if (!atMax)
        {
            amount -= xpToLevel[currentLevel];
            if (amount < 0)
            {
                amount = 0;
            }

            currentLevel++;
            if (currentLevel == maxLevel)
            {
                //have reached max level
                atMax = true;
                maxStack = 1;
            }
            else
            {
                maxStack = xpToLevel[currentLevel];
            }
            Debug.Log("Skill:levelUp:" + nameTag + " Current:" + currentLevel + " MaxLevel:" + maxLevel);
            IncManager.instance.skillLevelEvent.Invoke();
        }
    }
}