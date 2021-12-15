using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// HANDLES ALL INCREMENTABLE OBJECTS
///     holds arrays of all potential objects
///     can unlock them
///         by default only anything with an UnlockList is locked.  Can set impossible unlocks to force manual event based unlocking.
///     can add to them
///     can add them to their respective UI
///         atm automatically adds to UI if unlocked and positively incremented and has a UI
/// </summary>

public class IncManager : MonoBehaviour
{
    // hold all possible skills.
    SkillSO[] skillArray;
    ItemSO[] itemArray;
    ResourceSO[] resourceArray;

    public static IncManager instance;

    [HideInInspector]
    public UnityEvent skillLevelEvent = new UnityEvent();
    public UnityEvent resourceEvent = new UnityEvent();
    public UnityEvent itemEvent = new UnityEvent();

    public GameObject ItemPanel;
    public GameObject ResourcePanel;
    public GameObject SkillPanel;

    private bool gotResourceEvent = false;
    private bool gotItemEvent = false;
    void Awake()
    {
        instance = this;
        skillArray = Utils.GetAllScriptableObjects<SkillSO>();
        itemArray = Utils.GetAllScriptableObjects<ItemSO>();
        resourceArray = Utils.GetAllScriptableObjects<ResourceSO>();

        setup(itemArray);
        setup(skillArray);
        setup(resourceArray);
    }

    private void Update()
    {
        if(gotResourceEvent)
        {
            gotResourceEvent = false;
            resourceEvent.Invoke();
        }
        else if(gotItemEvent)
        {
            gotItemEvent = false;
            itemEvent.Invoke();
        }
    }


    void setup(IncrementableSO[] incArray)
    {
        foreach (IncrementableSO inc in incArray)
        {
            inc.reset();
        }
    }

    public T Get<T>(string _name) where T : IncrementableSO
    {
        IncrementableSO[] tempArray = null;
        if(typeof(T) == typeof(SkillSO))
        {
            tempArray = skillArray;
        }
        else if (typeof(T) == typeof(ResourceSO))
        {
            tempArray = skillArray;
        }
        else if (typeof(T) == typeof(ItemSO))
        {
            tempArray = skillArray;
        }

        if(tempArray != null)
        {
            foreach (IncrementableSO inc in tempArray)
            {
                if (inc.nameTag == _name)
                {
                    //already have skill
                    return (T)inc;
                }
            }
        }
        return null;
    }

   

    public void allSkillsDebugLog()
    {
        int i = 1;
        Debug.Log("==currentSkills===");
        foreach (SkillSO skill in skillArray)
        {
            Debug.Log("slot:" + i + "=" + skill.nameTag + " Level:" + skill.getLevel() + " XP:" + skill.getAmount());
            i++;
        }
        Debug.Log("===================");
    }

    public void Unlock(IncrementableSO inc)
    {
        //start up GUI
        if (inc != null)
        {
            inc.unlocked = true;
            inc.whenUnlocked();
        }

    }
    public void Unlock<T>(string _name) where T : IncrementableSO
    {
        IncrementableSO inc = Get<T>(_name);
        if (inc != null)
        {
            Unlock(inc);
        }
    }

    public void addToUI<T>(string _name, bool toActivate) where T : IncrementableSO
    {
        IncrementableSO inc = Get<T>(_name);
        addToUI(inc, toActivate);
    }

    public void addToUI(IncrementableSO inc, bool toActivate)
    {
        if (inc == null) return;
        else if (inc.UIActive != toActivate)
        {
            inc.UIActive = toActivate;
            //inc.UIGameObject;
            // ADDING TO CORRECT PANEL.
            //  can create an enum for it.  EquipPanel = 1, DebuffPanel = 2, ResourcePanel = 3....
            //      then in ItemSO/ResourceSO set the panelID.
            if (toActivate)
            {
                //adding to UI
            }
            else
            {
                //removing from UI
            }
        }
    }

    public float GetAmount<T>(string _name) where T : IncrementableSO
    {
        IncrementableSO inc = Get<T>(_name);
        if (inc != null) return inc.getAmount();
        return 0;
    }


    //ADD AMOUNT
    //either hand over an object to increment by amount
    // or give the type and a name.
    public void AddAmount(IncrementableSO inc, float amount = 1)
    {
        //if not active in ui and unlocked... add to UI
        if (inc != null)
        {
            //dirty but functional till we need something better.
            //Add to UI anything that isn't active in UI and is unlocked and is being incremented.
            //Remove from UI if a flag is checked based on which UI panel it is a part of.
            if(inc.hasUI)
            {
                if (inc.addAmount(amount) <= 0 && inc.removeFromUIOnEmpty) addToUI(inc, false);
                else if (!inc.UIActive && inc.unlocked) addToUI(inc, true);
            }
            else
            {
                inc.addAmount(amount);
            }

            //GOTTA UPDATE THIS ALL TOO.  Better listening for unlocks.
            //at least now having a bunch of resource ticks a second isn't screaming repeatedly in the same frame.
            if(!gotResourceEvent && inc.GetType() == typeof(ResourceSO))
            {
                gotResourceEvent = true;
            }
            else if (!gotItemEvent && inc.GetType() == typeof(ItemSO))
            {
                gotItemEvent = true;
            }
        }
    }
    //for button mashing and events.
    public void AddAmount<T>(string _name, float amount = 1) where T : IncrementableSO
    {
        AddAmount(Get<T>(_name), amount);
    }
}