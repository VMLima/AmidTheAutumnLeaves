using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Scriptable Object/Basic/Skill")]
public class SO_Skill : SO_Basic
{
    public int[] xpToLevel;
    [HideInInspector]
    public bool unlocked = false;
    //UI to be added to
    //placement order in UI
}