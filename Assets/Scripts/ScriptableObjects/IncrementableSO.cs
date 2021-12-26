using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




//[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Object/Basic/Item")]
public class IncrementableSO : UnlockableObjectSO
{

    //[Tooltip("A Prefab gameobjects with 'EffectScript' inheriting classes thrown on it.  Generally for on equip, ongoing effects, regen, damage, etc.")]
    //public GameObject effectObject;

    [ReadOnly] public float amount = 0;

    [Tooltip("Max amount/level.")]
    public int maximum = 1;

    [HideInInspector]
    public int maxStack;

    [HideInInspector]
    public int minAmount = 0;

    [HideInInspector]
    public int minStack;


    [Tooltip("Any 'EffectScripts' attached to this BLANK PREFAB will be activatable.")]
    public GameObject EffectObject;

    public Image UIImage;
    [HideInInspector] public TextMeshProUGUI textDisplay;
    private GameObject imageDisplay;

    [HideInInspector]
    public bool removeFromUIOnEmpty = false;    //when setting up UI panel ID, set this.
    [HideInInspector]
    public int UIPanelID = 0;   //will be an Enum that correlates to a panel.  be used in IncManager to add/remove from correct panel.

    [HideInInspector]
    public bool UIActive = false;

    [HideInInspector]
    public bool hasUI = false;

    [HideInInspector] public EffectManager effectManager;

    public virtual float getUnlockValue()
    {
        return amount;
    }

    public virtual void connectToUI()
    {
        //getResourcePrefab()
        //instantiate. Creates a copy of it.
        //UIObject = ~~~~~
        //imageDisplay = to UIObject child... OutputImage.
        //set textDisplay = to UIObject child.... OutputText.
        removeFromUIOnEmpty = false;
        refreshUI();
    }

    void refreshUI()
    {
        //STILL GOTTA HOOK UP.
        if (hasUI)
        {
            //imageDisplay.image = ResourceImage;
            //textDisplay.text = amount.ToString();
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
        effectManager = EffectManager.instance;

        //add all objects to the UIs.
        //will activate/deactivate like ButtonUnlockSO.
        UIActive = false;
        connectToUI();
        //SETUP UI PANEL STUFF
        
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
                textDisplay.text = amount.ToString();
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
        if (amount > maxStack)
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
        else if (amount > maxStack)
        {
            amount = maxStack;
        }
    }
}