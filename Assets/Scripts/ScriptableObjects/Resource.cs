using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// basic resource script
///     so far just holds name and amount.
/// </summary>
public class Resource : Incremental
{
    //Incremental uses Amount, setAmount, getAmount...
    public SO_Item scriptableItem;
    public Resource(ScriptableObject _scriptableObject) : base(_scriptableObject)
    {
        scriptableItem = (SO_Item)_scriptableObject;
        setAmount(1);
    }
}
