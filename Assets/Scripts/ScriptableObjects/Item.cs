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
    public SO_Item scriptableItem;
    //public int maxStacks;
    public Item(ScriptableObject _scriptableObject) : base(_scriptableObject)
    {
        scriptableItem = (SO_Item)_scriptableObject;
        //maxStacks = scriptableItem.maxStacks;
        setAmount(1);
    }
}