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
    //SkillSO[] skillArray;
    //ItemSO[] itemArray;
    //ResourceSO[] resourceArray;

    public static IncManager instance;

    [HideInInspector]
    public UnityEvent skillLevelEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent resourceEvent = new UnityEvent();
    [HideInInspector]
    public UnityEvent itemEvent = new UnityEvent();

    public DataStorageSO dataStorage;

    public GameObject ItemPanel;
    public GameObject ResourcePanel;
    public GameObject SkillPanel;

    private bool gotResourceEvent = false;
    private bool gotItemEvent = false;

    void startGame()
    {
        ButtonManager.instance.activateButtonArray("Start");
    }

    void Awake()
    {
        instance = this;

        compileDataStorage();   
        //updates and compiles the dataStorage scriptable object.  
        //links to all other scriptable objects and dataStorage.get<type>(_name) will return any scriptable object.
    }
    private void Start()
    {
        //functions that require everything setup to work.
        resetAllUnlockable();
        startGame();
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

    private void OnDestroy()
    {
        foreach(CommonBaseSO i in Utils.GetAllScriptableObjects<CommonBaseSO>())
        {
            i.destroyInstantiations();
        }
    }

    //get a scriptable object of type T and name _name.
    public T Get<T>(string _name) where T : CommonBaseSO
    {
        return dataStorage.get<T>(_name);
    }

    //get the amount in a scriptable object of type T and name _name
    public float GetAmount<T>(string _name) where T : IncrementableSO
    {
        IncrementableSO inc = Get<T>(_name);
        if (inc != null) return inc.getAmount();
        return 0;
    }

    public void AddAmount<T>(string _name, float amount = 1) where T : IncrementableSO
    {
        AddAmount(Get<T>(_name), amount);
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

            //add the quantity.  If it's new, activate.
            //if its empty and remove when empty.  deactivate.
            inc.addAmount(amount);
            if (inc.isActive == false) inc.activate(true);
            else if (inc.removeFromUIOnEmpty && inc.getAmount() <= 0) inc.activate(false);

            //GOTTA UPDATE THIS ALL TOO.  Better listening for unlocks.
            //at least now having a bunch of resource ticks a second isn't screaming repeatedly in the same frame.
            if(!gotResourceEvent && inc.GetType() == typeof(ResourceSO))
            {
                //gotResourceEvent = true;
                //Debug.Log("Resource Event");
                resourceEvent.Invoke();
            }
            else if (!gotItemEvent && inc.GetType() == typeof(ItemSO))
            {
                //gotItemEvent = true;
                //Debug.Log("Item Event");
                itemEvent.Invoke();
            }
        }
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

    void clearDataStorage()
    {
        dataStorage.clear();
    }

    void addToDataStorage(CommonBaseSO unl)
    {
        dataStorage.add(unl);
    }

    void compileDataStorage()
    {
        clearDataStorage();
        foreach (CommonBaseSO unl in Utils.GetAllScriptableObjects<CommonBaseSO>())
        {
            addToDataStorage(unl);
            //Debug.Log("name:" + unl.name);
        }
    }
    void resetAllUnlockable()
    {
        dataStorage.resetAll();
    }
}