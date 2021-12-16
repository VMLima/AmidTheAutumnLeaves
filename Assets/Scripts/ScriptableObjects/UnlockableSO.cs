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
public class UnlockableSO : ScriptableObject
{
    [Tooltip("MUST USE THIS EXACT NAME TO REFERENCE IT IN CODE")]
    public string nameTag;

    [Tooltip("Requirements to unlock.  When unlocked stays unlocked.")]
    public LockInfoSO[] toUnlock = new LockInfoSO[0];


    [ReadOnly] public bool unlocked = false;

    [HideInInspector] public IncManager incManager;

    //gotta setup listeners for LockInfo stuff here!!!
    public UnlockableSO()
    {
        //BE VERY CAREFUL DOING ANYTHING HERE.
        //This happens only once.  Opening and closing the game 3 times will only call this first time.
    }

    public virtual void reset()
    {
        //where you should reset and call all things that need to be reset/called upon new game.
        //every SO_~~~ should have reset() called during Start()
        incManager = IncManager.instance;
        unsubscribeFromListeners();

        //if there are things to listen for... it is locked.  Which means you cannot gain quantity or see it in UI.
        unlocked = setupListeners();
    }

    //take the list of toUnlock and create listeners for the right triggers to unlock.
    bool setupListeners()
    {
        bool _unlocked = true;
        if(toUnlock != null)
        {
            //Debug.Log("Adding listener for " + nameTag);
            foreach (LockInfoSO info in toUnlock)
            {
                if(info.unlocker == null)
                {

                }
                else if (info.unlocker.GetType() == typeof(SkillSO))
                {
                    incManager.skillLevelEvent.AddListener(updateUnlocked);
                    _unlocked = false;
                }
                else if (info.unlocker.GetType() == typeof(ResourceSO))
                {
                    incManager.resourceEvent.AddListener(updateUnlocked);
                    _unlocked = false;
                }
                else if (info.unlocker.GetType() == typeof(ItemSO))
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
        return _unlocked;
    }

    public virtual void whenUnlocked()
    {
        Debug.LogError("SO_Root:whenUnlocked: USING DEFAULT in " + nameTag);
    }

    private void OnDestroy()
    {
        unsubscribeFromListeners();
    }

    void unsubscribeFromListeners()
    {
        incManager.skillLevelEvent.RemoveListener(updateUnlocked);
        //NEED TO ADD OTHER MANAGERS.
    }

    void updateUnlocked()
    {
        if (!unlocked)
        {
            if (Utils.checkUnlocked(toUnlock))
            {
                Debug.Log("SO_Root:updateUnlocked: unlocking... " + nameTag);
                unlocked = true;
                whenUnlocked();
            }
        }
        if (unlocked)
        {
            unsubscribeFromListeners();
        }
    }
}