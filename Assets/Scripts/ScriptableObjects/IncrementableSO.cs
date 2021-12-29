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

    public Sprite UISprite;
    [HideInInspector] public TextMeshProUGUI textDisplay;
    [HideInInspector] public Image imageDisplay;

    [HideInInspector]
    public bool removeFromUIOnEmpty = false;    //when setting up UI panel ID, set this.
    [HideInInspector]
    public int UIPanelID = 0;   //will be an Enum that correlates to a panel.  be used in IncManager to add/remove from correct panel.

    [HideInInspector]
    public bool UIActive = false;

    [HideInInspector]
    public bool hasUI = false;

    private float incRate = 1;

    [HideInInspector] public EffectManager effectManager;

    public virtual float getUnlockValue()
    {
        return amount;
    }

    void refreshUI()
    {
        //STILL GOTTA HOOK UP.
        if(imageDisplay != null) imageDisplay.sprite = UISprite;
        if(textDisplay != null) textDisplay.text = amount.ToString();
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
            Debug.Log("SO_Basic:startEffect: ERROR string:" + _effectName + ":does not correspond to a game object on:" + name);
        }
    }

    public EffectScript[] getEffect(string effectName = "")
    {
        //get all components of this.
        List<EffectScript> effects = new List<EffectScript>();
        Component[] components = EffectObject.GetComponents(typeof(EffectScript));
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
        effectManager = EffectManager.instance;
        foreach (Transform eachChild in UIInstance.transform)
        {
            if (eachChild.name == "HookImage")
            {
                imageDisplay = eachChild.GetComponent<Image>();
                imageDisplay.sprite = UISprite;
            }
            else if (eachChild.name == "HookName")
            {
                eachChild.GetComponent<TextMeshProUGUI>().text = name;
            }
            else if (eachChild.name == "HookQuantity")
            {
                textDisplay = eachChild.GetComponent<TextMeshProUGUI>();
                textDisplay.text = "0";
            }
        }
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
        amount += _amount;
        if ((maxStack > 0) && (amount > maxStack))
        {
            amount = maxStack;
        }
    }

    public void subToAmount(float _amount)
    {
        amount -= _amount;
        if (amount < minStack)
        {
            amount = minStack;
        }
    }
    public void setAmount(float _amount)
    {
        amount = _amount;
        if (amount < minStack)
        {
            amount = minStack;
        }
        else if ((maxStack > 0) && (amount > maxStack))
        {
            amount = maxStack;
        }
    }
}