using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



//[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Object/Basic/Item")]
public class IncrementableSO : UIMenuSO
{

    //[Tooltip("A Prefab gameobjects with 'EffectScript' inheriting classes thrown on it.  Generally for on equip, ongoing effects, regen, damage, etc.")]
    //public GameObject effectObject;

    [ReadOnly] public float amount = 0;

    [Tooltip("Max amount/level. 0 means unlimited.")]
    public int maximum = 0;

    [HideInInspector]
    public int maxStack;

    [HideInInspector]
    public int minAmount = 0;

    [HideInInspector]
    public int minStack;

    [Tooltip("Any 'EffectScripts' attached to this BLANK PREFAB will be activatable.")]
    public GameObject EffectObject;
    private GameObject EffectObjectInstance;

    //[HideInInspector]
    public bool removeFromUIOnEmpty = false;    //when setting up UI panel ID, set this.
    [HideInInspector]
    public int UIPanelID = 0;   //will be an Enum that correlates to a panel.  be used in IncManager to add/remove from correct panel.

    [HideInInspector]
    public bool UIActive = false;

    [HideInInspector]
    public bool hasUI = false;

    private EffectScript[] effectArray;
    private bool hasEffects;

    private float incRate = 1;

    [HideInInspector] public EffectManager effectManager;

    
    public override void onPress()
    {
        base.onPress();
        if (this.getAmount() < 1) return;

        if (clickEffects != null && clickEffects.Length > 0)
        {
            foreach (IncrementalValuePair pair in clickEffects)
            {
                if(pair.incrementable != null) IncManager.instance.Add(pair.incrementable, pair.amount);
            }
        }
        
        IncManager.instance.Add(this, -1);
    }

    public virtual float getUnlockValue()
    {
        return (int)amount;
    }
    public void addEffects(float oldAmount)
    {
        if (!hasEffects) return;
        
        if (oldAmount != amount)
        {
            Debug.Log("IncrementableSO:addEffects:" + nameTag + ":" + (amount - oldAmount));
            effectManager.startEffect(effectArray, (int)(amount - oldAmount));
        }
    }
    //starts an effect script in the effect object matching effectName.
    //if effectName is blank, start every effect attached to the effect object.
    public void startEffect(string _effectName = "", int stacks = 1)
    {
        EffectScript[] effects = getEffect(_effectName);
        
        if(effects != null)
        {
            effectManager.startEffect(effects, stacks);
        }
        else
        {
            Debug.Log("SO_Basic:startEffect: ERROR string:" + _effectName + ":does not correspond to a game object on:" + nameTag);
        }
    }

    public EffectScript[] getEffect(string effectName = "")
    {
        //get all components of this.
        if (EffectObjectInstance == null) return null;
        List<EffectScript> effects = new List<EffectScript>();
        Component[] components = EffectObjectInstance.GetComponents(typeof(EffectScript));
        foreach (Component effect in components)
        {
            if((effectName == "") || (((EffectScript)effect).name == effectName))
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
        Component[] components = EffectObject.GetComponents(typeof(EffectScript));
        foreach (Component effect in components)
        {
            if ((effectName == "") || (((EffectScript)effect).name == effectName))
            {
                effectManager.endEffect(((EffectScript)effect).name);
            }
        }
    }

    public override void reset()
    {
        base.reset();
        amount = 0;
        maxStack = maximum;
        minStack = minAmount;
        incRate = 1;
        //effectArray
        effectManager = EffectManager.instance;
        hasEffects = false;
        if (EffectObject != null)
        {
            EffectObjectInstance = Instantiate(EffectObject);
            effectArray = getEffect();
            if (effectArray != null && effectArray.Length > 0) hasEffects = true;
        }
    }
    private void OnDestroy()
    {
        if (EffectObjectInstance)  Destroy(EffectObjectInstance);
    }

    public float getAmount()
    {
        return amount;
    }

    //adds amount to incremental
    // returns the new amount.
    // AddToAmountInterim can be overridien by inheriting classes to
    // add functionality or tweak values before addition/subtraction.
    public float addAmount(float _amount)
    {
        if (unlocked)
        {
            if (_amount > 0)
            {
                addToAmountOverride(_amount);
            }
            else
            {
                subToAmountOverride((-1 * _amount));
            }

            if (textDisplay != null)
            {
                //Debug.Log("IncremtableSO:addAmount: textDisplay is not null:" + name);
                textDisplay.text = getUnlockValue().ToString();
            }
        }
        return amount;
    }

    public virtual void addToAmountOverride(float _amount)
    {
         addToAmount(_amount);
    }

    public virtual void subToAmountOverride(float _amount)
    {
         subToAmount(_amount);
    }

    public void addToAmount(float _amount)
    {
        float oldAmount = amount;
        amount += _amount;
        amount = Mathf.Round(amount * 100f) / 100f;
        if ((maxStack > 0) && (amount > maxStack))
        {
            amount = maxStack;
        }
        if(hasEffects) addEffects(oldAmount);
    }

    public void subToAmount(float _amount)
    {
        float oldAmount = amount;
        amount -= _amount;
        amount = Mathf.Round(amount * 100f) / 100f;
        if (amount < minStack)
        {
            amount = minStack;
        }
        if (hasEffects) addEffects(oldAmount);
    }
    public void setAmount(float _amount)
    {
        float oldAmount = amount;
        amount = _amount;
        amount = Mathf.Round(amount * 100f) / 100f;
        if (amount < minStack)
        {
            amount = minStack;
        }
        else if ((maxStack > 0) && (amount > maxStack))
        {
            amount = maxStack;
        }
        if (hasEffects) addEffects(oldAmount);
    }
}