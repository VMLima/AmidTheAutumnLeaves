using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




//[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Object/Basic/Item")]
public class SO_Basic : ScriptableObject
{
    [Tooltip("MUST USE THIS EXACT NAME TO REFERENCE IT IN CODE")]
    public string name;

    [Tooltip("Starting max quantity of item.")]
    public int maxStack = 1;

    [Tooltip("Starting min quantity of item.")]
    public int minStack = 0;

    [Tooltip("Requirements to unlock.  When unlocked stays unlocked")]
    public LockInfo[] toUnlock;

    [Tooltip("The UI prefab to represent this item.")]
    public GameObject prefab;
}