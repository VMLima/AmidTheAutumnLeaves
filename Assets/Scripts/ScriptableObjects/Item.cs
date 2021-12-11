using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// basic item holder script.
///     so far holds just name and amount.
/// </summary>
public class Item : Incremental
{
    //Incremental uses Amount, setAmount, getAmount...
    public SO_Item soItem;
    public bool DeleteWhenEmpty;
    //public int maxStacks;
    public Item(SO_Basic _scriptableObject) : base(_scriptableObject)
    {
        soItem = (SO_Item)_scriptableObject;
        DeleteWhenEmpty = soItem.DeleteWhenEmpty;
        //maxStacks = scriptableItem.maxStacks;
        setAmount(1);
        Debug.Log("Item creation and set: " + soBasic.amount);
    }
}