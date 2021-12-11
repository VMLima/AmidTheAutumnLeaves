using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




//[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Object/Basic/Item")]
public class SO_Basic : SO_Root
{

    [HideInInspector]
    public int amount = 0;

    [Tooltip("Starting max quantity of item.")]
    public int maxAmount = 1;

    [HideInInspector]
    public int maxStack;

    [Tooltip("Starting min quantity of item.")]
    public int minAmount = 0;

    [HideInInspector]
    public int minStack;

    [Tooltip("The UI prefab to represent this item.")]
    public GameObject prefab;

    public override void reset()
    {
        base.reset();
        amount = 0;
        maxStack = maxAmount;
        minStack = minAmount;
    }
}