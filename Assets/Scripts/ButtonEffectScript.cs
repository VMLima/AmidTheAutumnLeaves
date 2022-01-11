using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

public class ButtonEffectScript : EffectScript
{

    //list of effects to have happen on button press.
    //public incrStruct[] toIncrementList;
    //[Tooltip("")] public string description;

    public string buttonTitle;
    public Color ButtonTitleColor = Color.black;
    [TextArea]public string buttonTooltip;
    public float tooltipHoverDelay = 1f;
    public bool doNotShowCostsInTooltip = false;
    public Color buttonColor = Color.gray;
    [HideInInspector] public Tooltip tooltip;
    private Image image;
    
    private TextMeshProUGUI titleGUI;
    [Tooltip("")] public bool toggleButton = true;
    private bool isToggled;
    private ColorBlock ToggleBlock;
    private ColorBlock NormalBlock;
    private Button button;
    private Color32 normalColor;
    private Color32 pressedColor;
    [Tooltip("For a non-toggle button, it is the stamina lost on action.  For a toggle button, it is the stamina lost each tick.")]
    //public float staminaCost;
    public List<IncrementalValuePair> onStartEffect;
    public List<IncrementalValuePair> onTickEffect;
    public bool mirrorStartonStop = true;


    bool cannotPress = false;
    float waitTime = 1;

    public int getPanelIndex()
    {
        return button.transform.GetSiblingIndex();
    }

    public void deleteSelf()
    {
        Destroy(button.gameObject);
    }

    public void PressDelay(float _waitTime = 1)
    {
        PreventPresses();
        StopAllCoroutines();
        StartCoroutine(noPressFor(_waitTime));
    }

    public void PreventPresses()
    {
        cannotPress = true;
    }    

    public void AllowPresses()
    {
        cannotPress = false;
    }
    public IEnumerator noPressFor(float _waitTime = 1)
    {
        yield return new WaitForSeconds(_waitTime);
        AllowPresses();
    }

    public void updateButton()
    {
        if (titleGUI != null)
        {
            titleGUI.text = buttonTitle;
            titleGUI.color = ButtonTitleColor;
            titleGUI.ForceMeshUpdate();
        }
        else Debug.LogError("ButtonEffectScript:updateButton:not titleGUI found in:" + button.name);
        if (tooltip != null) tooltip.setTooltip(compileTooltip());
        if (image != null) image.color = buttonColor;
    }

    public void setTooltipHoverDelay(float timeToWait)
    {
        tooltip.setHoverDelay(timeToWait);
    }

    public void refreshTooltip(float timeToWait = -1)
    {
        if (button.IsActive())
        {
            tooltip.clickReset(timeToWait);
        }
        else tooltip.hideMessage();
    }

    public void setButtonText(string newTitle, string newTooltip = "")
    {
        if (newTitle != "") buttonTitle = newTitle;
        if (newTooltip != "") buttonTooltip = newTooltip;
        updateButton();
    }

    public void setTitle(string newTitle)
    {
        buttonTitle = newTitle;
        updateButton();
    }

    public void setTextColor(Color newColor)
    {
        ButtonTitleColor = newColor;
        updateButton();
    }    
    public void setButtonColor(Color newColor)
    {
        buttonColor = newColor;
        updateButton();
    }
    public void setTooltip(string newTooltip)
    {
        buttonTooltip = newTooltip;
        updateButton();
    }

    protected override void Awake()
    {
        base.Awake();
        //get this's button component and add listener to onClick()
        this.GetComponent<Button>().onClick.AddListener(onClickHidden);
        isToggled = false;

        button = this.GetComponent<Button>();

        Transform temp = button.transform.Find("HookName");
        if (temp) titleGUI = temp.GetComponent<TextMeshProUGUI>();
        else Debug.LogError("ButtonEffectScript:Awake:Could not find HookName to set button name in:" + button.name);

        tooltip = button.GetComponent<Tooltip>();
        image = button.GetComponent<Image>();
        setTooltipHoverDelay(tooltipHoverDelay);

        //Debug.Log("button name:" + button.name);
        normalColor = button.colors.normalColor;
        pressedColor = button.colors.selectedColor;
        NormalBlock = button.colors;
        NormalBlock.normalColor = normalColor;
        NormalBlock.selectedColor = normalColor;
        //button effect script needs to tell tooltip
        updateButton();
    }


    public virtual string compileTooltip()
    {
        if (doNotShowCostsInTooltip) return buttonTooltip;
        string output = "";
        bool hasTooltip = false;
        if (buttonTooltip != "")
        {
            output += buttonTooltip;
            hasTooltip = true;
        }
        bool doAnd;

        if (onStartEffect != null && onStartEffect.Count > 0)
        {
            if(hasTooltip)
            {
                output += "\n\n";
                hasTooltip = false;
            }
            //output += "On Press = ";
            doAnd = false;
            foreach (IncrementalValuePair pair in onStartEffect)
            {
                if (doAnd) output += "\n";
                doAnd = true;
                output += pair.amount + " " + pair.incrementable.name;
            }
            output += "\n";
        }
        if (toggleButton)
        {
            if (onTickEffect != null && onTickEffect.Count >= 1)
            {
                if (hasTooltip)
                {
                    output += "\n\n";
                    hasTooltip = false;
                }
                output += "Every " + frequency + " seconds = ";
                doAnd = false;
                foreach (IncrementalValuePair pair in onTickEffect)
                {
                    if (doAnd) output += "\n";
                    doAnd = true;
                    output += pair.amount + " " + pair.incrementable.name;
                }
                output += "\n";
            }
        }
        return output;
    }

    //since buttons don't deal with stacks.  Am changing the methods to not deal with parameters.
    public override void onStart(int oldNumStacks, int addingStacks)
    {
        if (cannotPress) return;
        if (onStartEffect != null && onStartEffect.Count > 0)
        {
            foreach(IncrementalValuePair pair in onStartEffect)
            {
                IncManager.instance.Add(pair.incrementable, pair.amount);
            }
        }
        onStart();
    }

    public override void onStop(int oldNumStacks, int removingStacks)
    {
        if (cannotPress) return;
        if (mirrorStartonStop)
        {
            if (onStartEffect != null && onStartEffect.Count > 0)
            {
                foreach (IncrementalValuePair pair in onStartEffect)
                {
                    IncManager.instance.Add(pair.incrementable, -1*pair.amount);
                }
            }
        }
        onStop();
    }

    public override void onTick(int numEffects)
    {
        if (onTickEffect != null && onTickEffect.Count > 0)
        {
            foreach (IncrementalValuePair pair in onTickEffect)
            {
                IncManager.instance.Add(pair.incrementable, pair.amount);
            }
        }
        onTick();
    }

    public virtual void onStart()
    {
        tooltip.clickReset();
    }

    public virtual void onStop()
    {
        tooltip.clickReset();
    }
    public virtual void onTick()
    {
        
    }

    public void setStartEffect(IncrementableSO cost, float newCost)
    {
        if(onStartEffect != null && onStartEffect.Count > 0)
        {
            for(int i = 0; i<onStartEffect.Count;i++)
            {
                if (onStartEffect[i].incrementable == cost)
                {
                    onStartEffect[i].setPair(cost, newCost);
                    return;
                }
            }
        }
        onStartEffect.Add(new IncrementalValuePair(cost, newCost));
    }
    public void addStartEffect(IncrementableSO cost, float newCost)
    {
        onStartEffect.Add(new IncrementalValuePair(cost, newCost));
    }
    public void removeStartEffect(IncrementableSO cost = null)
    {
        if (cost == null)
        {
            onStartEffect.Clear();
            return;
        }
        if (onStartEffect != null && onStartEffect.Count > 0)
        {
            for (int i = onStartEffect.Count - 1; i >= 0; i--)
            {
                if (onStartEffect[i].incrementable == cost)
                {
                    onStartEffect.RemoveAt(i);
                    return;
                }
            }
        }
    }

    public void haltEffects()
    {
        //turns everything off.
        //ends ongoing effects
        //Debug.Log("ButtonEffectScript:haltEffects:" + name + ":toggleButton:" + toggleButton + " : isToggled:" + isToggled);
        if (toggleButton && isToggled)
        {
            //Debug.Log("ButtonEffectScript:haltEffects: is toggle button and toggled");
            onClickHidden();
            onStop(0, 0);
        }
        else
        {

        }
        
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
                //Debug.Log("ButtonEffectScript:haltEffects:" + name+":  Toggled: " + isToggled);
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
                //Debug.Log("ButtonEffectScript:haltEffects:" + name + ":  Toggled: " + isToggled);
                EffectManager.instance.startEffect(this);
            }
        }
        else
        {
           // Debug.Log("press: " + name);
            onStart(0,0);
            //onStop(0,1);
        }
    }
    
}