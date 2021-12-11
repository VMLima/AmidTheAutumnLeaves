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
    public string name;
    public int amount;
    public int maxStack;
    public int minStack;
    public SO_Basic soBasic;
    private bool unlocked;
    public Incremental(SO_Basic _scriptableObject)
    {
        soBasic = _scriptableObject;
        name = _scriptableObject.name;
        amount = 0;
        minStack = soBasic.minStack;
        maxStack = soBasic.maxStack;
        updateUnlocked();
        setupListeners();
    }
    
    public int getAmount()
    {
        return amount;
    }

    //GENERALLY DON'T OVERRIDE THIS..
    public int addAmount(int _amount)
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

    //set up future unlocks.
    private void setupListeners()
    {
        foreach (LockInfo info in soBasic.toUnlock)
        {
            //get its type
            //find the actual created object
            if (info.soBasic.GetType() == typeof(SO_Skill))
            {
                //add a listener to soBasic.LevelUp()
                
                Skill temp = SkillManager.instance.getSkill((SO_Skill)info.soBasic);
                if (temp != null)
                {
                    SkillManager.instance.skillLevelEvent.AddListener(updateUnlocked);
                }
                temp.levelUp();
                //after that is called check for
                //add a listener to temp.levelUp();
            }
            else if (info.soBasic.GetType() == typeof(SO_Resource))
            {
                //incTemp = ResourceManager.instance.getResource((SO_Resource)info.soBasic);
                Debug.LogError("Incremental:checkIfLocked: Resources not set up yet.");
            }
            else if (info.soBasic.GetType() == typeof(SO_Item))
            {

            }
            else
            {

            }
        }
    }


    //UNLOCK STUFF
    //if this changes unlocked->locked it needs to do some sort of refresh.
    //or maybe SkillManager, in setting up a skill will have an unlockSkill() function.
    public void updateUnlocked()
    {
        if (unlocked)
        {
            //run through each LockInfo...
            //find out where the state of the requirement is stored (based on it's data type)
            //run through the list of unlocked requirements, if it exists and has the right value...
            // keep checking requirements
            // otherwise it fails.
            foreach (LockInfo info in soBasic.toUnlock)
            {
                //get its type
                //find the actual created object
                if (info.soBasic.GetType() == typeof(SO_Skill))
                {
                    Skill temp = SkillManager.instance.getSkill((SO_Skill)info.soBasic);
                    if (temp.getLevel() >= info.amount)
                    {
                        //REQUIREMENT SUCCESS!!
                    }
                    else
                    {
                        unlocked = false;
                    }
                }
                else if (info.soBasic.GetType() == typeof(SO_Resource))
                {
                    //incTemp = ResourceManager.instance.getResource((SO_Resource)info.soBasic);
                    Debug.LogError("Incremental:checkIfLocked: Resources not set up yet.");
                }
                else if (info.soBasic.GetType() == typeof(SO_Item))
                {
                    Item temp = ItemManager.instance.getItem((SO_Item)info.soBasic);
                    //incTemp = ItemManager.instance.getItem((SO_Item)info.soBasic);
                    if (temp.getAmount() >= info.amount)
                    {
                        //REQUIREMENT SUCCESS!!!
                    }
                    else
                    {
                        unlocked = false;
                    }
                }
                else
                {
                    Debug.LogError("Incremental:checkIfLocked: unknown SO_basic type of " + soBasic.name);
                    unlocked = false;
                }
            }
            //all requirements are a success!!
        }
        unlocked = true;
    }
}
