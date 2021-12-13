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
public class SO_Root : ScriptableObject
{
    [Tooltip("MUST USE THIS EXACT NAME TO REFERENCE IT IN CODE")]
    public string nameTag;

    [Tooltip("Requirements to unlock.  When unlocked stays unlocked.")]
    public LockInfo[] toUnlock = new LockInfo[0];

    [HideInInspector]
    public bool unlocked = false;

    //gotta setup listeners for LockInfo stuff here!!!
    public SO_Root()
    {
        //BE VERY CAREFUL DOING ANYTHING HERE.
        //This happens only once.  Opening and closing the game 3 times will only call this first time.
    }

    public virtual void reset()
    {
        //where you should reset and call all things that need to be reset/called upon new game.
        //every SO_~~~ should have reset() called during Start()
        unsubscribeFromListeners();
        unlocked = false;
        setupListeners();
    }

    //take the list of toUnlock and create listeners for the right triggers to unlock.
    void setupListeners()
    {
        if(toUnlock != null)
        {
            //Debug.Log("Adding listener for " + nameTag);
            foreach (LockInfo info in toUnlock)
            {
                if (info.soBasic.GetType() == typeof(SO_Skill))
                {
                    //SKILL LISTENER.  attach to the 'skillLevelEvent' in SkillManager.
                    //  some day I will set it up better.  Listening for specific skill's levelUp,
                    //  instead of just checking on every one's.
                    SkillManager.instance.skillLevelEvent.AddListener(updateUnlocked);
                }
                else if (info.soBasic.GetType() == typeof(SO_Resource))
                {
                    Debug.LogError("Incremental:setupListeners: Resources not set up yet.");
                    //ALSO ADD A LINE IN unsubscribeFromListners, when setting up.
                }
                else if (info.soBasic.GetType() == typeof(SO_Item))
                {
                    Debug.LogError("Incremental:setupListeners: Item not set up yet.");
                    //ALSO ADD A LINE IN unsubscribeFromListners, when setting up.
                }
                else
                {
                    Debug.LogError("Incremental:setupListeners: UNKNOWN DATA TYPE.");
                }
            }
        }
        
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
        SkillManager.instance.skillLevelEvent.RemoveListener(updateUnlocked);
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