using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMenuSO : CommonBaseSO
{
    public bool hideName = false;
    public bool hideQuantity = false;
    [HideInInspector]
    public GameObject UIInstance;
    [HideInInspector]
    public GameObject UIPrefab;
    [HideInInspector]
    public GameObject UIPanel;
    [HideInInspector]
    public bool isActive;
    [TextArea]public string tooltipText = "";
    public bool useOnlyTooltipText = false;
    [HideInInspector] public int UIIndex;

    public Sprite UISprite;
    [HideInInspector] public TextMeshProUGUI textDisplay;
    [HideInInspector] public Image imageDisplay;
    [HideInInspector] public Button clickButton;

    public IncrementalValuePair[] clickEffects;
    //public IncrementalValuePair[] passiveEffects;
    [HideInInspector] public Tooltip tooltip;
    public override void reset()
    {
        base.reset();
        
        declareUI();
        instantiateUI();
        activate(false);
        
    }

    

    public virtual void declareUI()
    {
        Debug.LogError("UnlockableObjectSO:setButtonInfo: have not overridden this method in:" + nameTag);
        //MUST OVERRIDE AND SET...
        // buttonPrefab
        //      the prefab to instantiate
        //  UIPanel
        //      the UI panel to add the button to.
    }

    public virtual void onPress()
    {
        //IF THE UI ELEMENT IS A BUTTON... CAN OVERRIDE THIS FUNCTION FOR WHAT HAPPENS ON IT'S PRESS.
        tooltip.clickReset();
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
            output += pair.amount + " " + pair.incrementable.nameTag;
            doAnd = true;
        }
        return output;
    }

    public void instantiateUI()
    {
        if(UIPrefab == null)
        {
            Debug.LogError("UnlockableObjectSO:createButton:No Button Prefab in: " + nameTag);
            return;
        }
        if(UIPanel == null)
        {
            Debug.LogError("UnlockableObjectSO:createButton:No UI Panel in: " + nameTag);
            return;
        }
        UIInstance = (GameObject)Instantiate(UIPrefab);
        UIInstance.transform.SetParent(UIPanel.transform);
        UIInstance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        tooltip = UIInstance.GetComponent<Tooltip>();

        if (UISprite != null)
        {
            Image tImage = UIInstance.GetComponent<Image>();
            tImage.sprite = UISprite;
            Color tColor = tImage.color;
            tColor.a = 1;
            tImage.color = tColor;
        }
        setUIData();

        Tooltip temp = UIInstance.GetComponent<Tooltip>();
        if(temp)
        {
            temp.setTooltip(compileTooltip());
            //Debug.Log("UIMENUSO:instantiateUI: found tooltip:" + nameTag + ":" + temp.getTooltip());
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
            if (hideName) temp.GetComponent<TextMeshProUGUI>().text = "";
            else temp.GetComponent<TextMeshProUGUI>().text = nameTag;
        }

        temp = UIInstance.transform.Find("HookQuantity");
        if (temp)
        {
            if (hideQuantity) temp.GetComponent<TextMeshProUGUI>().text = "";
            else
            {
                textDisplay = temp.GetComponent<TextMeshProUGUI>();
                textDisplay.text = "0";
            }
            
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
                    Debug.Log("UIMenuSO:activate:is activating:" + nameTag);
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
