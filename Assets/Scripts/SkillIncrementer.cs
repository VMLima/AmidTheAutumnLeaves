using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkillIncrementer
{
    //this is gonna get instantiated with a skill and amount per second.
    //held in a list of active passive incrementers in SkillManager.
    private Skill skill;
    private int incrAmount = 0;
    private float timer;
    public string tag;
    public SkillIncrementer(string _tag, Skill _skill, int _incrAmount)
    {
        tag = _tag;
        skill = _skill;
        incrAmount = _incrAmount;
        timer = 0;
    }
    public void tick()
    {
        Debug.Log("SkillIncrementer:tick");
        if (skill != null) skill.addXP(incrAmount);
    }
}
