using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillManager : MonoBehaviour
{
    // hold all possible skills.
    SO_Skill[] SOSkillArray;

    // hold all learned skills.
    List<Skill> skillList;

    // hold all skill incrementers
    List<SkillIncrementer> activeIncrementers;

    public static SkillManager instance;
    private float timer;

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
        skillList = new List<Skill>();
        SOSkillArray = Utils.GetScriptableSkills<SO_Skill>();
        setupSkills();
        activeIncrementers = new List<SkillIncrementer>();
        timer = 0f;
    }

    void setupSkills()
    {
        skillList.Clear();
        foreach(SO_Skill skill in SOSkillArray)
        {
            skillList.Add(new Skill(skill));
            skill.reset();
        }
    }

    public bool hasSkill(string skillName)
    {
        foreach (Skill skill in skillList)
        {
            if (skill.soSkill.nameTag == skillName)
            {
                //already have skill
                return true;
            }
        }
        return false;
    }

    public Skill getSkill(string skillName)
    {
        foreach (Skill skill in skillList)
        {
            if (skill.soSkill.nameTag == skillName)
            {
                //already have skill
                return skill;
            }
        }
        return null;
    }

    public Skill getSkill(SO_Skill soSkill)
    {
        return getSkill(soSkill.nameTag);
    }

    private SO_Skill getScriptableObject(string prefabName)
    {
        foreach (SO_Skill soSkill in SOSkillArray)
        {
            if (soSkill.nameTag == prefabName)
            {
                //found the prefab.
                return soSkill;
            }
        }
        return null;
    }

    public void unlockSkill(SO_Skill skill)
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
        foreach (Skill skill in skillList)
        {
            Debug.Log("slot:" + i + "=" + skill.soSkill.nameTag + " Level:" + skill.getLevel() + " XP:" + skill.getAmount());
            i++;
        }
        Debug.Log("===================");
    }

    public void unlockSkill(string skillName)
    {
        Skill skill = getSkill(skillName);
        if (skill != null)
        {
            unlockSkill(skill.soSkill);
        }
    }

    public int getXP(string skillName)
    {
        Skill skill = getSkill(skillName);
        if(skill != null) return skill.getAmount();
        return 0;
    }

    //for button mashing and events.
    public void addXP(string skillName, int amount)
    {
        Skill skill = getSkill(skillName);
        if (skill != null) skill.addAmount(amount);
    }

    //tag is the unique call by which the passiveGain can be stopped
    //for periodic xp gain over a stretch of time.
    public void passiveXP(string tag, string skillName, int amount)
    {
        Debug.Log("SkillManager:passiveXP:" + skillName + ":" + amount);
        Skill skill = getSkill(skillName);
        if(skill != null)
        {
            SkillIncrementer inc = new SkillIncrementer(tag, skill, amount);
            activeIncrementers.Add(inc);
        }
    }

    //stops active incrementer(s) with the tag.
    //if all is true, will stop all active incrementers with the tag.
    //if all is false, will stop only 1 active incrementer with the tag.
    public void stopPassiveXP(string tag, bool all = false)
    {
        for (int i = activeIncrementers.Count - 1; i >= 0; i--)
        {
            if(activeIncrementers[i].tag == tag)
            {
                activeIncrementers.RemoveAt(i);
                if (!all) return;
            }
        }
    }

    private void tickPassive()
    {
        foreach(SkillIncrementer s in activeIncrementers)
        {
            s.tick();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //every second update.
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            tickPassive();
            timer -= 1f;
        }
    }
}
