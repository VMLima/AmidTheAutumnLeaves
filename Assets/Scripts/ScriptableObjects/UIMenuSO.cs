using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public override void whenUnlocked()
    {
        //Debug.Log("ButtonUnlockSO:whenUnlocked:" + name + ":isActive=" + isActive);
        activate(isActive);
        //if already activated, but unlock blocked, now will activate.
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
        activate(false);
    }

    public virtual void setUIData()
    {

    }

    public void reorderUI()
    {
        UIInstance.transform.SetAsLastSibling();
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
                    reorderUI();
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
