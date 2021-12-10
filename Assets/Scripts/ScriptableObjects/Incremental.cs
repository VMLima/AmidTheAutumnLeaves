using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class....
///     THIS CLASS EXISTS SO YOU CAN INSERT ANY INCREMENTAL (skill/item/resource) along with an amount 
///     into a certain struct (hasn't been setup yot)
///     and those structs can be easily incremented, and easily converted into hoverable tooltips.
///     
///     basic addition, subtraction from an amount.  Compares it to min/max size.
///     addToAmountInterim - CAN BE OVERRIDEN
///         this function gets the amount to be added and calls AddToAmount(int _amount).
///         it can handle any situational logic before actual addition and min/max size checks.
///     subToAmountInterim - CAN BE OVERRIDEN
///         as above, but if addAmount(int _amount) called in a script has _amount negative.
///         the amount recieved by subToAmountInterim WILL BE POSITIVE.  It is the amount to subtract.
/// </summary>

public abstract class Incremental
{
    public string name;
    public int amount;
    public int maxStack;
    public int minStack;
    public ScriptableObject scriptableObject;
    public Incremental(ScriptableObject _scriptableObject)
    {
        scriptableObject = _scriptableObject;
        name = _scriptableObject.name;
        amount = 1;
    }
    
    public int getAmount()
    {
        return amount;
    }

    //GENERALLY DON'T OVERRIDE THIS..
    public int addAmount(int _amount)
    {
        amount += _amount;
        if (_amount > 0)
        {
            return addToAmountInterim(_amount);
        }
        else
        {
            return subToAmount(-1 * _amount);
        }
    }

    public virtual int addToAmountInterim(int _amount)
    {
        return addToAmount(_amount);
    }

    public virtual int subToAmountInterim(int _amount)
    {
        return subToAmount(_amount);
    }

    public int addToAmount(int _amount)
    {
        _amount += _amount;
        if (_amount > maxStack)
        {
            _amount = maxStack;
        }
        return _amount;
    }

    public int subToAmount(int _amount)
    {
        _amount -= _amount;
        if (_amount < minStack)
        {
            _amount = minStack;
        }
        return _amount;
    }
    public virtual void setAmount(int _amount)
    {
        amount = _amount;
        if (_amount < minStack)
        {
            amount = minStack;
        }
        else if (_amount > maxStack)
        {
            amount = maxStack;
        }
    }
}
