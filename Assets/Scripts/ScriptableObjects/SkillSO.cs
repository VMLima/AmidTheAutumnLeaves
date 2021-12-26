using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// data storage and easy creation for skills.
///     stores key elements.
///     values persist between runs? need to use reset() on start game to reset values (can work well with saves?)
/// </summary>

[CreateAssetMenu(fileName = "NewSkill", menuName = "Scriptable Object/Basic/Skill")]
public class SkillSO : IncrementableSO
{
    //public int[] xpToLevel;
    [Tooltip("How much xp to reach lvl 1.")]
    public int baseXP = 100;
    [Tooltip("Each level takes X times as much xp as the last.")]
    public float levelXPMultiplier = 1.25f;
    [Tooltip("Each level takes X more xp than the last.")]
    public float levelXPAddition = 50;

    [ReadOnly] public int currentLevel;

    [ReadOnly] public float xpToLevel;

    [HideInInspector] public bool atMax;

    private int maxLevel;
    //UI to be added to
    //placement order in UI

    public override void reset()
    {
        base.reset();
        currentLevel = 0;
        atMax = false;
        maxLevel = maximum;
        xpToLevel = baseXP;
    }

    public override void declareUI()
    {
        UIPanel = IncManager.instance.SkillPanel;
        UIPrefab = Utils.GetPrefab("SkillPrefab");    //still gotta make and hook up to the prefab.
    }

    public override float getUnlockValue()
    {
        return currentLevel;
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
            amount += _amount;
            if (amount >= xpToLevel)
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
            amount -= xpToLevel;
            if (amount < 0)
            {
                amount = 0;
            }

            currentLevel++;
            if (currentLevel == maxLevel)
            {
                //have reached max level
                atMax = true;
            }
            else
            {
                xpToLevel *= levelXPMultiplier; 
                xpToLevel += levelXPAddition;
            }
            Debug.Log("Skill:levelUp:" + name + " Current:" + currentLevel + " MaxLevel:" + maxLevel);
            incManager.skillLevelEvent.Invoke();
        }
        textDisplay.text = currentLevel.ToString();
    }
}