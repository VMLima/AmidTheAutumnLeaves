using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMenuSO : CommonBaseSO
{
    [HideInInspector]
    public GameObject UIInstance;
    [HideInInspector]
    public GameObject UIPrefab;
    [HideInInspector]
    public GameObject UIPanel;
    [HideInInspector]
    public bool isActive;
    public string tooltipText = "";
    public bool useOnlyTooltipText = false;
    [HideInInspector] public int UIIndex;

    public Sprite UISprite;
    [HideInInspector] public TextMeshProUGUI textDisplay;
    [HideInInspector] public Image imageDisplay;
    [HideInInspector] public Button clickButton;

    public IncrementalValuePair[] clickEffects;
    //public IncrementalValuePair[] passiveEffects;
    public override void reset()
    {
        base.reset();
        declareUI();
        instantiateUI();
        activate(false);
        
    }

    

    public virtual void declareUI()
    {
        Debug.LogError("UnlockableObjectSO:setButtonInfo: have not overridden this method in:" + name);
        //MUST OVERRIDE AND SET...
        // buttonPrefab
        //      the prefab to instantiate
        //  UIPanel
        //      the UI panel to add the button to.
    }

    public virtual void onPress()
    {
        //IF THE UI ELEMENT IS A BUTTON... CAN OVERRIDE THIS FUNCTION FOR WHAT HAPPENS ON IT'S PRESS.
    }

    public override void whenUnlocked()
    {
        //Debug.Log("ButtonUnlockSO:whenUnlocked:" + name + ":isActive=" + isActive);
        activate(isActive);
        //if already activated, but unlock blocked, now will activate.
    }

    public virtual string  compileTooltip()
    {
        if (useOnlyTooltipText || clickEffects == null || clickEffects.Length <= 0) return tooltipText;

        string output = "";
        if (tooltipText != "")  output += tooltipText + "\n";
        output += "Use 1 = ";
        bool doAnd = false;
        foreach (IncrementalValuePair pair in clickEffects)
        {
            if (doAnd) output += " and ";
            output += pair.amount + " " + pair.incrementable.name;
            doAnd = true;
        }
        return output;
    }

    public void instantiateUI()
    {
        if(UIPrefab == null)
        {
            Debug.LogError("UnlockableObjectSO:createButton:No Button Prefab in: " + name);
            return;
        }
        if(UIPanel == null)
        {
            Debug.LogError("UnlockableObjectSO:createButton:No UI Panel in: " + name);
            return;
        }
        UIInstance = (GameObject)Instantiate(UIPrefab);
        UIInstance.transform.SetParent(UIPanel.transform);
        UIInstance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        setUIData();

        Tooltip temp = UIInstance.GetComponent<Tooltip>();
        if(temp)
        {
            temp.setTooltip(compileTooltip());
            Debug.Log("UIMENUSO:instantiateUI: found tooltip:" + name + ":" + temp.getTooltip());
        }
        
        
        activate(false);
    }

    public virtual void setUIData()
    {
        Transform temp = UIInstance.transform.Find("HookImage");
        if (temp)
        {
            imageDisplay = temp.GetComponent<Image>();
            imageDisplay.sprite = UISprite;
        }

        temp = UIInstance.transform.Find("HookName");
        if (temp)
        {
            temp.GetComponent<TextMeshProUGUI>().text = name;
        }

        temp = UIInstance.transform.Find("HookQuantity");
        if (temp)
        {
            textDisplay = temp.GetComponent<TextMeshProUGUI>();
            textDisplay.text = "0";
        }

        temp = UIInstance.transform.Find("HookButton");
        if (temp && clickEffects != null && clickEffects.Length > 0)
        {
            clickButton = temp.GetComponent<Button>();
            clickButton.onClick.RemoveListener(onPress);
            clickButton.onClick.AddListener(onPress);
        }
    }

    public void setUIIndex(int indexvalue)
    {
        UIIndex = indexvalue;
        UIInstance.transform.SetSiblingIndex(UIIndex);
    }

    public void reorderUI()
    {
        //UIInstance.transform.SetAsLastSibling();
    }

    public override void destroyInstantiations()
    {
        //Debug.Log("ButtonUnlockSO:destroyInstantiatons:" + name);
        base.destroyInstantiations();
        if (UIInstance == null)
        {
            //Debug.Log("ButtonUnlockSO:destroyButton: NO BUTTON TO DESTROY: " + name);
        }
        else Destroy(UIInstance.gameObject);
    }


    public virtual void activate(bool _isActive)
    {
        //if(isActive != turnOn)
        //{
            isActive = _isActive;
            if ((UIInstance != null))
            {
                if (unlocked && _isActive)
                {
                    Debug.Log("UIMenuSO:activate:is activating:" + name);
                    //reorderUI();
                    UIInstance.SetActive(true);
                }
                else
                {
                    UIInstance.SetActive(false);
                }
                
            }
        //}
    }
}
