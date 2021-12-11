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
public class SO_Skill : SO_Basic
{
    public int[] xpToLevel;
    [HideInInspector]
    public int currentLevel;
    [HideInInspector]
    public bool maxLevel;
    //UI to be added to
    //placement order in UI

    public override void reset()
    {
        base.reset();
        currentLevel = 0;
        maxLevel = false;
    }

    public override void whenUnlocked()
    {
        //active skill UI
    }
}