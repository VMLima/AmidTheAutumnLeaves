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

public class ButtonEffect : EffectScript
{

    //list of effects to have happen on button press.
    //public incrStruct[] toIncrementList;
    //[Tooltip("")] public string description;
    [Tooltip("")] public bool toggleButton = true;
    private bool isToggled = false;
    private ColorBlock ToggleBlock;
    private ColorBlock NormalBlock;
    private Button button;
    private Color32 normalColor;
    private Color32 pressedColor;


    protected override void Awake()
    {
        base.Awake();
        //get this's button component and add listener to onClick()
        this.GetComponent<Button>().onClick.AddListener(onClickHidden);
        isToggled = false;
        button = this.GetComponent<Button>();
        //Debug.Log("button name:" + button.name);
        normalColor = button.colors.normalColor;
        pressedColor = button.colors.selectedColor;
        NormalBlock = button.colors;
        NormalBlock.normalColor = normalColor;
        NormalBlock.selectedColor = normalColor;


    }


    //since buttons don't deal with stacks.  Am changing the methods to not deal with parameters.
    public override void onStart(int oldNumStacks, int addingStacks)
    {
        onStart();
    }

    public override void onStop(int oldNumStacks, int removingStacks)
    {
        onStop();
    }

    public override void onTick(int numEffects)
    {
        onTick();
    }

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
            if(isToggled)
            {
                isToggled = false;
                NormalBlock.normalColor = normalColor;
                NormalBlock.selectedColor = normalColor;
                button.colors = NormalBlock;
                //Debug.Log("Toggle off: " + name);
                EffectManager.instance.endEffect(this);
            }
            else
            {
                //Debug.Log("Toggle on: " + name);
                isToggled = true;
                NormalBlock.normalColor = pressedColor;
                NormalBlock.selectedColor = pressedColor;
                button.colors = NormalBlock;
                //button.colors = ToggleBlock;
                EffectManager.instance.startEffect(this);
            }
        }
        else
        {
            //Debug.Log("press: " + name);
            onStart();
            //onStop(0,1);
        }
    }
    
}