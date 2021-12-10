using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Scriptable Object/Basic/Skill")]
public class SO_Skill : ScriptableObject
{
    public string name;
    public int[] xpToLevel;
    [HideInInspector]
    public bool unlocked = false;

    [Tooltip("The UI prefab to represent this item.")]
    public GameObject prefab;
    //UI to be added to
    //placement order in UI
}