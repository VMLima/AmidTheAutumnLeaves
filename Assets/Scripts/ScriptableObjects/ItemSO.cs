using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Object/Basic/Item")]
public class ItemSO : IncrementableSO
{
    [Tooltip("Removes the item from the inventory list when quantity hits 0")]
    public bool DeleteWhenEmpty = false; //can have certain inventory items first/second/third, etc.
    //[Tooltip("Lower values = on sort, placed more leftward in UI")]
    //public int UIIndex = 000; //can have certain inventory items first/second/third, etc.
    //placement order in UI

    public override void reset()
    {
        base.reset();
    }
}