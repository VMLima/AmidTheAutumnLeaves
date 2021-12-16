using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct incrStruct
{
    public IncrementableSO soBasic;
    public float incTime;
    public int incAmount;
    private float time;
    public string descTooltip;
    public string descEffect;
    public incrStruct(IncrementableSO _soBasic, int _incAmount = 1, float _incTime = 1f, string _descTooltip = "", string _descEffect = "")
    {
        //something to be incremented
        soBasic = _soBasic;
        //how often to be incremented
        incTime = _incTime;
        time = incTime;
        //how much to be incremented
        incAmount = _incAmount;
        //tooltip description
        descTooltip = _descTooltip;
        //extra effect description
        descEffect = _descEffect;
    }

    //however often this is called... running through a list of structs doing .tick(deltaTime) will handle all the passive gains.
    public void tick(float _time)
    {
        //still gotta work this out.
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