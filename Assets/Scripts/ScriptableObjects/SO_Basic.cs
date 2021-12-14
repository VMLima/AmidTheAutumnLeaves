using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




//[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Object/Basic/Item")]
public class SO_Basic : SO_Root
{

    [Tooltip("Drag as many Prefab gameobjects holding 'EffectScript' classes here as the item needs.  Generally for on equip, ongoing effects, regen, damage, etc.")]
    public GameObject effectObject;

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

    [Tooltip("The UI prefab to represent this item.  Okay if None.")]
    public GameObject UIGameObject;

    private bool isActive = false;

    //starts an effect script in the effect object matching effectName.
    //if effectName is blank, start every effect attached to the effect object.
    public void startEffect(string _effectName = "", int stacks = 1)
    {
        EffectScript[] effects = getEffect(_effectName);
        
        if(effects != null)
        {
            EffectManager.instance.startEffect(effects, stacks);
        }
        else
        {
            Debug.Log("SO_Basic:startEffect: ERROR string:" + _effectName + ":does not correspond to a game object on:" + name);
        }
    }

    public EffectScript[] getEffect(string effectName = "")
    {
        //get all components of this.
        List<EffectScript> effects = new List<EffectScript>();
        Component[] components = effectObject.GetComponents(typeof(EffectScript));
        foreach (Component effect in components)
        {
            if((effectName == "") || (((EffectScript)effect).nameTag == effectName))
            {
                effects.Add(((EffectScript)effect));
            }
        }
        return effects.ToArray();
    }

    //ends an effect script in the effect object matching effectName.
    //if effectName is blank, end every effect attached to this object.
    public void endEffect(string effectName = "")
    {
        List<EffectScript> effects = new List<EffectScript>();
        Component[] components = effectObject.GetComponents(typeof(EffectScript));
        foreach (Component effect in components)
        {
            if ((effectName == "") || (((EffectScript)effect).nameTag == effectName))
            {
                EffectManager.instance.endEffect(((EffectScript)effect).nameTag);
            }
        }
    }

    public override void reset()
    {
        base.reset();
        amount = 0;
        isActive = false;
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