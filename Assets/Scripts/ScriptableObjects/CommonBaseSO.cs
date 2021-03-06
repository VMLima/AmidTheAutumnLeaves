using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// base functionality for scriptable objects
///     a reset().  Data in scriptable objects is held between runs, so necessary.
///     a list of elements needed to be at certain values to unlock this scriptable object
///         if empty, nothing happens.
///         if values... creates/destroys appropriate listners
///         WhenUnlocked() then needs to be overriden for what to do when the unlock requirements happen.
/// </summary>


//[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Object/Basic/Item")]
public class CommonBaseSO : ScriptableObject
{
    [Tooltip("If empthy, auto sets on game run to object name.  Can custom set it here though.  MUST USE EXACTLY THIS STRING TO CALL THE OBJECT IN CODE.")]
    //public new string nameTag;
    public string nameTag;

    [Tooltip("Requirements to unlock.  When unlocked stays unlocked.")]
    public IncrementalValuePair[] toUnlock = new IncrementalValuePair[0];


    [ReadOnly] public bool unlocked = false;

    [HideInInspector] public IncManager incManager;

    //gotta setup listeners for LockInfo stuff here!!!
    public CommonBaseSO()
    {
        //BE VERY CAREFUL DOING ANYTHING HERE.
        //This happens only once.  Opening and closing the game 3 times will only call this first time.
    }

    public virtual void destroyInstantiations()
    {
        //probably not necessary, but I have it anyway.  Whatever prefabs this object has created, this can be called to destroy them.
    }

    public virtual void reset()
    {
        //where you should reset and call all things that need to be reset/called upon new game.
        //every SO_~~~ should have reset() called during Start()
        if (nameTag == "") nameTag = name;
        destroyInstantiations();
        incManager = IncManager.instance;
        unsubscribeFromListeners();

        //if there are things to listen for... it is locked.  Which means you cannot gain quantity or see it in UI.
        unlocked = setupListeners();
        //Debug.Log("Unlockable: " + name);
    }

    //take the list of toUnlock and create listeners for the right triggers to unlock.
    bool setupListeners()
    {
        bool _unlocked = true;
        if(toUnlock != null)
        {
            //Debug.Log("Adding listener for " + nameTag);
            foreach (IncrementalValuePair info in toUnlock)
            {
                if(info.incrementable == null)
                {
                    _unlocked = false;
                }
                else if (info.incrementable.GetType() == typeof(SkillSO))
                {
                    incManager.skillLevelEvent.AddListener(updateUnlocked);
                    _unlocked = false;
                }
                else if (info.incrementable.GetType() == typeof(ResourceSO))
                {
                    incManager.resourceEvent.AddListener(updateUnlocked);
                    _unlocked = false;
                }
                else if (info.incrementable.GetType() == typeof(ItemSO))
                {
                    incManager.itemEvent.AddListener(updateUnlocked);
                    _unlocked = false;
                }
                else
                {
                    Debug.LogError("Incremental:setupListeners: UNKNOWN DATA TYPE.");
                }
            }
        }
        //Debug.Log("setupListeners: unlockState: " + nameTag + ":" + _unlocked);
        return _unlocked;
    }

    public virtual void whenUnlocked()
    {
        //Debug.LogError("SO_Root:whenUnlocked: NO UNLOCK FUNCTIONALITY IN " + name);
    }

    private void OnDestroy()
    {
        unsubscribeFromListeners();
    }

    void unsubscribeFromListeners()
    {
        incManager.skillLevelEvent.RemoveListener(updateUnlocked);
        incManager.resourceEvent.RemoveListener(updateUnlocked);
        incManager.itemEvent.RemoveListener(updateUnlocked);
        //NEED TO ADD OTHER MANAGERS.
    }

    public virtual void unlock()
    {
        Debug.Log("SO_Root:updateUnlocked: unlock... " + nameTag);
        unlocked = true;
        unsubscribeFromListeners();
        whenUnlocked();
    }

    void updateUnlocked()
    {
        //Debug.Log("checking lock..." + name);
        if (!unlocked)
        {
            if (Utils.checkUnlocked(toUnlock))
            {
                unlock();
            }
        }
        //if (unlocked)
        //{
        //    unsubscribeFromListeners();
        //}
    }
}