using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//unused atm.
[System.Serializable]
public struct effectStruct
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

    public effectStruct(IncrementableSO _incrementableSO, int _rateIncrease = 0, int _amountIncrease = 0, float _frequency = 1f, float _duration = 0)//, string _descTooltip = "", string _descEffect = "")
    {
        incrementableSO = _incrementableSO;
        rateIncrease = _rateIncrease;
        frequency = _frequency;
        amountIncrease = _amountIncrease;
        duration = _duration;
    }
}

//used for unlocking things.
[System.Serializable]
public struct IncrementalValuePair
{
    public IncrementableSO incrementable;
    public float amount;
    public IncrementalValuePair(IncrementableSO _incrementable, float _amount)
    {
        incrementable = _incrementable;
        amount = _amount;
    }
    public void setPair(IncrementableSO _incrementable, float _amount)
    {
        incrementable = _incrementable;
        amount = _amount;
    }
}

public class Structures
{

}