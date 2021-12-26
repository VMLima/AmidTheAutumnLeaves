using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct incrStruct
{
    public IncrementableSO incrementableSO;

    [Tooltip("UNUSED.  Concept though is that all future gains of said resource will have this % added to it. 100 = 100% increase.")]
    public int rateIncrease;
    [Tooltip("Each Frequency will add this amount of resource.")]
    public int amountIncrease;
    [Tooltip("How often to trigger amount increases.")]
    public float frequency;
    [Tooltip("How long increases will last.  0 means untill this object is removed.")]
    public float duration;
    
    //public string descTooltip;
    //public string descEffect;
    public incrStruct(IncrementableSO _incrementableSO, int _rateIncrease = 0, int _amountIncrease = 0, float _frequency = 1f, float _duration = 0)//, string _descTooltip = "", string _descEffect = "")
    {
        incrementableSO = _incrementableSO;
        rateIncrease = _rateIncrease;
        frequency = _frequency;
        amountIncrease = _amountIncrease;
        duration = _duration;
        //tooltip description
        //descTooltip = _descTooltip;
        //extra effect description
        //descEffect = _descEffect;
    }
}


//SO_Basic holds a list of these as the requirements to unlock the element.
//when item/skill/etc is about to be created, first checks the SO_Basic if it is unlocked.
//  check if already unlocked
//      if it is a skill, then the amount is level amount
//      if it is an item/resource, then the amount is amount
//  potential future check for situations that will unlock.
//      something to do with listeners

[System.Serializable]
public struct LockInfoSO
{
    public IncrementableSO unlocker;
    public int amount;
}

public class Structures
{
    /// <summary>
    /// incrStruct
    ///     USED FOR ANY LONG TERM INCREMENT ACTION.
    ///     Buttons will add a created incrStruct to a list on press, remove that same one on toggle stop?
    ///         GOTTA FIGURE OUT THE DETAILS OF REMOVING THEM.
    ///     it holds a incrementable (skill/item/resource) and an amount to increment and a timeframe to increment it by.
    ///     can easily have inspector elements contain a list of these for easy button setup.
    ///     can easily have a tooltip reader read anything that does a long term effect.
    /// </summary>
}