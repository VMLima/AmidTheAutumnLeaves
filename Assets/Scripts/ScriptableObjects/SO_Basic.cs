using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




//[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Object/Basic/Item")]
public class SO_Basic : SO_Root
{

    [HideInInspector]
    public int amount = 0;

    [Tooltip("Starting max quantity of item.")]
    public int maxAmount = 1;

    [HideInInspector]
    public int maxStack;

    [Tooltip("Starting min quantity of item.")]
    public int minAmount = 0;

    [HideInInspector]
    public int minStack;

    [Tooltip("The UI prefab to represent this item.")]
    public GameObject prefab;

    public override void reset()
    {
        base.reset();
        amount = 0;
        maxStack = maxAmount;
        minStack = minAmount;
    }

    public int getAmount()
    {
        return amount;
    }

    //adds amount to incremental
    // returns the new amount.
    // AddToAmountInterim can be overridien by inheriting classes to
    // add functionality or tweak values before addition/subtraction.
    public int addAmount(int _amount)
    {
        if (unlocked)
        {
            if (_amount > 0)
            {
                return addToAmountOverride(_amount);
            }
            else
            {
                return subToAmount((-1 * _amount));
            }
        }
        return amount;
    }

    public virtual int addToAmountOverride(int _amount)
    {
        return addToAmount(_amount);
    }

    public virtual int subToAmountOverride(int _amount)
    {
        return subToAmount(_amount);
    }

    public int addToAmount(int _amount)
    {
        amount += _amount;
        if (amount > maxStack)
        {
            amount = maxStack;
        }
        return amount;
    }

    public int subToAmount(int _amount)
    {
        amount -= _amount;
        if (amount < minStack)
        {
            amount = minStack;
        }
        return amount;
    }
    public void setAmount(int _amount)
    {
        amount = _amount;
        if (amount < minStack)
        {
            amount = minStack;
        }
        else if (amount > maxStack)
        {
            amount = maxStack;
        }
    }
}