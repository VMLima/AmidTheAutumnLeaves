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


[CreateAssetMenu(fileName = "NewStatus", menuName = "Scriptable Object/StatusEffect")]
public class SO_StatusEffect : ScriptableObject
{
    [Tooltip("MUST USE THIS EXACT NAME TO REFERENCE IT IN CODE")]
    public string nameTag;

    [Tooltip("Prefab Game object with the script that handles functionality and any UI elements")]
    public GameObject UIObject;

    //[Tooltip("Timer based duration of effect.  0 means no timer based removal.")]
    //public float duration = 0;

    public virtual void reset()
    {
        //where you should reset and call all things that need to be reset/called upon new game.
        //every SO_~~~ should have reset() called during Start()

    }

    public void start()
    {
        
    }
}