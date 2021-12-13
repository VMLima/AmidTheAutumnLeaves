using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




//[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Object/Basic/Item")]
public class SO_Basic : SO_Root
{

    [Tooltip("Drag as many Prefab gameobjects holding 'EffectScript' classes here as the item needs.  Generally for on equip, ongoing effects, regen, damage, etc.")]
    public GameObject[] effectObjects;

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

    //when isActive = true, create its effect objects, attach it to... manager? something?.

    //when isActive = false, remove the object from... manager? something?

    //  That object needs the amount.  Precise, up to date.  Maybe I can pass this SO_Basic to it.

    public void startEffect(GameObject effect)
    {
        if (effect != null)
        {

            EffectScript effectScript = effect.GetComponent<EffectScript>();
            if (effectScript != null)
            {
                EffectManager.instance.startEffect(effectScript);
            }
            else
            {
                Debug.Log("SO_Basic:startEffect: ERROR gameObject:" + effect.name + ": attached to :" + name + ": does not have an EffectScript");
            }
        }
        else
        {
            Debug.Log("SO_Basic:startEffect: ERROR was passed empty game object");
        }
    }

    public void startEffect(string _effectName)
    {
        GameObject effectObject = getEffect(_effectName);
        if(effectObject != null)
        {
            startEffect(effectObject);
        }
        else
        {
            Debug.Log("SO_Basic:startEffect: ERROR string:" + _effectName + ":does not correspond to a game object on:" + name);
        }
    }

    public GameObject getEffect(string effectName)
    {
        foreach (GameObject effectObject in effectObjects)
        {
            if (effectObject.name != effectName) return effectObject;
        }
        return null;
    }


    public void activate(bool turnOn)
    {
        if(turnOn)
        {
            isActive = true;
            if (effectObjects != null)
            {
                foreach (GameObject effectObject in effectObjects)
                {
                    if(effectObject != null) EffectManager.instance.startEffect(effectObject);
                }
            }
        }
        else
        {
            isActive = false;
            foreach (GameObject effectObject in effectObjects)
            {
                EffectManager.instance.endEffect(effectObject);
            }
        }
    }

    public override void reset()
    {
        base.reset();
        foreach (GameObject effectObject in effectObjects)
        {
            effectObject.name = nameTag;
        }
        amount = 0;
        isActive = false;
        maxStack = maxAmount;
        minStack = minAmount;
        activate(false);
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