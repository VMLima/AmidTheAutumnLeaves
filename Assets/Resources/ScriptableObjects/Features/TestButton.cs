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

public class TestButton : MonoBehaviour
{

    //list of effects to have happen on button press.
    //public incrStruct[] toIncrementList;
    [Tooltip("")] public string description;
    [Tooltip("")] public bool toggleButton = true;
    private bool isToggled = false;

    void Awake()
    {
        //get this's button component and add listener to onClick()
        this.GetComponent<Button>().onClick.AddListener(onClickHidden);
        isToggled = false;
        Debug.Log("Instantiating: " + name);
    }

    //since buttons don't deal with stacks.  Am changing the methods to not deal with parameters.

    public virtual void onStart()
    {

    }

    public virtual void onStop()
    {

    }
    public virtual void onTick()
    {

    }

    void onClickHidden()
    {
        //basic functionality.  Everything is an 'onclick' no toggle.
        if (toggleButton)
        {
            if (isToggled)
            {
                isToggled = false;
                Debug.Log("Toggle off: " + name);
                //EffectManager.instance.startEffect(this);
            }
            else
            {
                isToggled = true;
                Debug.Log("Toggle on: " + name);
                //EffectManager.instance.endEffect(this);
            }

        }
        else
        {
            onStart();
            Debug.Log("press: " + name);
            //onStop(0,1);
        }
    }
}