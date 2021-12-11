using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    //public int amount;
    //public int maxStack;
    //public int minStack;
    public SO_Basic soBasic;
    //private bool unlocked;
    public Incremental(SO_Basic _scriptableObject)
    {
        soBasic = _scriptableObject;
    }
    
    public int getAmount()
    {
        return soBasic.amount;
    }

    //adds amount to incremental
    // returns the new amount.
    // AddToAmountInterim can be overridien by inheriting classes to
    // add functionality or tweak values before addition/subtraction.
    public int addAmount(int _amount)
    {
        if (soBasic.unlocked)
        {
            if (_amount > 0)
            {
                return addToAmountInterim(_amount);
            }
            else
            {
                return subToAmount((-1 * _amount));
            }
        }
        return soBasic.amount;
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
        soBasic.amount += _amount;
        if (soBasic.amount > soBasic.maxStack)
        {
            soBasic.amount = soBasic.maxStack;
        }
        return soBasic.amount;
    }

    public int subToAmount(int _amount)
    {
        soBasic.amount -= _amount;
        if (soBasic.amount < soBasic.minStack)
        {
            soBasic.amount = soBasic.minStack;
        }
        return soBasic.amount;
    }
    public void setAmount(int _amount)
    {
        soBasic.amount = _amount;
        if (soBasic.amount < soBasic.minStack)
        {
            soBasic.amount = soBasic.minStack;
        }
        else if (soBasic.amount > soBasic.maxStack)
        {
            soBasic.amount = soBasic.maxStack;
        }
    }
}
