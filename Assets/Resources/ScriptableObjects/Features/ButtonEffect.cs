using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HANDLES BUTTON PRESS EFFECTS
///     this base script can be dragged onto any button prefab and through the inspector you can...
///         set up any simple press->resource functionality
///         (addition, subtraction... skill xp, resource amount, item quantity...)
/// A INHERITABLE CLASS
///     if you want more complex effects
///     this handles button press effects.  This base script can be inherited and the..
///         onClickExtra()
///         function can be overriden and any extra effects for on click can be added there.
/// </summary>

public class ButtonEffect : MonoBehaviour
{

    //list of effects to have happen on button press.
    public incrStruct[] toIncrementList;
    
    void Awake()
    {
        //get this's button component and add listener to onClick()
        this.GetComponent<Button>().onClick.AddListener(onClick);
    }

    public virtual void onClickExtra()
    {
        //BLANK.  Exists to be overriden.
    }

    public void onClick()
    {
        //basic functionality.  Everything is an 'onclick' no toggle.
        if (toIncrementList != null)
        {
            foreach (incrStruct info in toIncrementList)
            {
                
                if(info.soBasic != null) IncManager.instance.AddAmount(info.soBasic, info.incAmount);
                Debug.Log("OnClick");
            }
        }
        onClickExtra();
    }
}