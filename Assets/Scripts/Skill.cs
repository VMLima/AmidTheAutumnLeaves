using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    //skill incrementer class
    /// <summary>
    /// it will take a skill and an amount as a parameter.  OnUpdate, each second will increase by amount.
    /// 
    /// </summary>
    
    //when you unlock the skill, start at skill level 0.
    //can be changed if wanted.
    public string name;
    public int[] xpToLevel;
    private int numLevels;
    private int skillLevel;
    private int skillProgress;
    private bool atMaxLevel;

    private SO_Skill scriptableSkill;

    public Skill(SO_Skill _scriptableSkill)
    {
        Debug.Log("Skill start instantiating");
        scriptableSkill = _scriptableSkill;
        name = _scriptableSkill.name;
        xpToLevel = _scriptableSkill.xpToLevel;

        numLevels = xpToLevel.Length;
        Debug.Log("Skill created: numLevels=" + numLevels);

        //can move these to scriptable object for between run memory?
        skillLevel = 0;
        skillProgress = 0;
        atMaxLevel = false;
    }

    public int getXP()
    {
        return skillProgress;
    }

    public int getLevel()
    {
        return skillLevel;
    }

    //skill.progress(10); progresses skill xp by 10.
    //negative values are ignored.
    public void addXP(int amount)
    {
        if (amount < 0) return;
        if (!atMaxLevel)
        {
            skillProgress += amount;
            if (skillProgress >= xpToLevel[skillLevel])
            {
                //leveld up!!
                levelUp();
            }
        }
    }
    
    //advance to the next level.
    //Overflow xp is added to the next level.
    private void levelUp()
    {
        skillProgress -= xpToLevel[skillLevel];
        if(skillProgress < 0)
        {
            skillProgress = 0;
        }
        skillLevel++;
        if (skillLevel >= (numLevels))
        {
            //have reached max level
            atMaxLevel = true;
        }
    }
}
