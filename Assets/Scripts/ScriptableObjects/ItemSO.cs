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

    public override void declareUI()
    {
        UIPrefab = null;    //still gotta make and hook up to the prefab.
        //UIImage
        //name
        //will be fed into the prefab.
        //textDisplay = (numerical text output panel.  Will get updated on addAmount())
        UIPanel = IncManager.instance.ItemPanel;
    }

    public override void reset()
    {
        base.reset();
    }
}