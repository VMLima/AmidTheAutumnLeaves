using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager : MonoBehaviour
{
    // hold all possible skills.
    SkillSO[] skillArray;

    // hold all learned skills.
    //List<Skill> skillList;

    public static SkillManager instance;

    public UnityEvent skillLevelEvent = new UnityEvent();

    //IN THE FUTURE...
    //each skill script is in a prefab attached to the top game object.
    //The prefab is attached to a scriptable object.

    //IN THE FUTURE... skills are all created at start in another class, some globals class.
    // can do Globals.instance.skill_gathering.addXP(10);
    // instead of SkillManager.instance.getSkill("gathering").addXP(10);
    // maybe.  Possibly.

    //FOR NOW.. handled like items.
    //find all scriptable objects.
    //Create a Skill as you unlock feeding in the object as parameter.
    //add to a list.
    //events will handle the rest.
    void Awake()
    {
        instance = this;
        skillArray = Utils.GetAllScriptableObjects<SkillSO>();
        setupSkills();
    }

    void setupSkills()
    {
        foreach(SkillSO skill in skillArray)
        {
            skill.reset();
        }
    }

    public bool hasSkill(string skillName)
    {
        if (getSkill(skillName) == null) return false;
        return true;
    }

    public SkillSO getSkill(string skillName)
    {
        foreach (SkillSO skill in skillArray)
        {
            if (skill.nameTag == skillName)
            {
                //already have skill
                return skill;
            }
        }
        return null;
    }

    public SkillSO getSkill(SkillSO soSkill)
    {
        return getSkill(soSkill.nameTag);
    }

    public void unlockSkill(SkillSO skill)
    {
        //start up GUI
        if(skill != null)
        {
            skill.unlocked = true;
            skill.whenUnlocked();
        }    
        
    }

    public void allSkillsDebugLog()
    {
        int i = 1;
        Debug.Log("==currentSkills===");
        foreach (SkillSO skill in skillArray)
        {
            Debug.Log("slot:" + i + "=" + skill.nameTag + " Level:" + skill.getLevel() + " XP:" + skill.getAmount());
            i++;
        }
        Debug.Log("===================");
    }

    public void unlockSkill(string skillName)
    {
        SkillSO skill = getSkill(skillName);
        if (skill != null)
        {
            unlockSkill(skill);
        }
    }

    public int getXP(string skillName)
    {
        SkillSO skill = getSkill(skillName);
        if(skill != null) return skill.getAmount();
        return 0;
    }

    //for button mashing and events.
    public void addXP(string skillName, int amount)
    {
        SkillSO skill = getSkill(skillName);
        if (skill != null) skill.addAmount(amount);
    }
}