using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HANDLES BUTTON PRESS EFFECTS
///     Override onStart, onStop, onTick.
///     toggle buttons will call all 3.
///         onStart on toggle on
///         onTick every frequency thereafter
///         onStop on toggle stop
///     single press buttons
///         onStart on toggle on
/// </summary>

public class CraftEffectScript : ButtonEffectScript
{

    //list of effects to have happen on button press.
    //public incrStruct[] toIncrementList;
    //[Tooltip("")] public string description;
    IncrementalValuePair[] craftArray;
    IncrementalValuePair[] costArray;
    CraftRecipeSO parent;
    //on stop? do these.

    public void setArrays(IncrementalValuePair[] _craftArray, IncrementalValuePair[] _costArray, CraftRecipeSO _parent)
    {
        craftArray = _craftArray;
        costArray = _costArray;
        name = _parent.nameTag;
        parent = _parent;
    }

    protected override void Awake()
    {
        duration = 1;
        toggleButton = false;
        base.Awake();
    }

    public override void onStart()
    {
        parent.craft();
    }
}