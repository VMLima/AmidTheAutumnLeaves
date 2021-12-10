using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public SO_Item scriptableItem;
    private int quantity;
    public string name;
    public bool singleInstance;
    public bool canStack;
    public int maxStack;
    public Item(SO_Item _scriptableItem)
    {
        scriptableItem = _scriptableItem;
        name = _scriptableItem.name;
        singleInstance = _scriptableItem.singleInstance;
        canStack = _scriptableItem.canStack;
        maxStack = _scriptableItem.maxStack;
        quantity = 1;

    }

    //adds a positive or negative value to the current quantity.
    //returns false if the resultant value is below 0.
    public bool addQuantity(int toAdd)
    {
        if (toAdd > 0)
        {
            return addToQuantity(toAdd);
        }
        else
        {
            return subToQuantity(-1 * toAdd);
        }
    }

    //sets the quantity to a certain value
    //if the item is not stackable, ignores setQuantity
    //if the value is higher than the max stack value... sets to max stack value
    //if the value is negative, does 0 instead.
    public void setQuantity(int toSet)
    {
        if (canStack)
        {
            if (toSet > maxStack)
            {
                quantity = maxStack;
            }
            else if (toSet < 0)
            {
                quantity = 0;
            }
            else
            {
                quantity = toSet;
            }
        }

    }

    //returns the quantity
    public int getQuantity()
    {
        return quantity;
    }

    #region PRIVATE FUNCTIONS

    //handles the positive side of addQuantity
    private bool addToQuantity(int toAdd)
    {
        if (canStack)
        {
            quantity += toAdd;
            if (quantity > maxStack)
            {
                //Debug.Log("Item:addToQunatity: reached max stack size");
                quantity = maxStack;
            }
        }
        //Debug.Log("Item:addToQunatity: new quantity=" + quantity.ToString());
        return true;
    }

    //handles the negative side of addQuantity
    private bool subToQuantity(int toSub)
    {
        quantity -= toSub;
        //Debug.Log("Item:subToQuantity: new quantity=" + quantity.ToString());
        if (quantity <= 0)
        {
            //all out of item.
            return false;
        }
        return true;
    }
    #endregion
}
