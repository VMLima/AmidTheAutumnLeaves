using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "NewResource", menuName = "Scriptable Object/Basic/Resource")]
public class SO_Resource : ScriptableObject
{
    [Tooltip("MUST USE THIS EXACT NAME TO REFERENCE IT IN CODE")]
    public string name;

    [Tooltip("If Gaining more overflows stack size and less than maxStacks, overflow into new inventory stack.")]
    public int maxStacks = 1;

    [Tooltip("Gaining more items adds to quantity.  Like Gold.")]
    public bool canStack = false;

    [Tooltip("Starting max quantity of item.")]
    public int maxStack = 1;

    [Tooltip("The UI panel this will be added to.  0=??? 1=??? 2=?????")]
    public int UIScreen = 0;

    [Tooltip("Lower values = on sort, placed more leftward in UI")]
    public int UIIndex = 000; //can have certain inventory items first/second/third, etc.
    //placement order in UI

    [Tooltip("The UI prefab to represent this item.")]
    public GameObject prefab;
}