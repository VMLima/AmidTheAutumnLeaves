using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// much like SO_Nodes
///     BE CAREFUL STORING DATA HERE.  Any data this holds will be shared through every SO_Feature of this type that comes up.
///         fine for 'unlocked' bad for 'damage to current area from overruse'.
/// </summary>

[CreateAssetMenu(fileName = "NewFeature", menuName = "Scriptable Object/Feature")]
public class RoomFeatureSO : UIMenuSO
{
    public string activeDescription;
    public string inactiveDescripton;
    
    //private bool isActive;
    public Button button;
    //[HideInInspector] public GameObject buttonInstance;

    //core functionality of unlocking the feature.
    //unlock() sets unlocked=true then goes here.

    public override void activate(bool turnOn)
    {
        base.activate(turnOn);
        ButtonManager.instance.refreshText();
    }

    public override string compileTooltip()
    {
        ButtonEffectScript temp = button.GetComponent<ButtonEffectScript>();
        if(temp != null)
        {
            return temp.compileTooltip();
        }
        return "";
    }

    public override void declareUI()
    {
        UIPrefab = button.gameObject;// IncManager.instance.ButtonPrefab;
        UIPanel = IncManager.instance.ButtonPanel;
    }

    public override void setUIData()
    {
        foreach (Transform eachChild in UIInstance.transform)
        {
            if (eachChild.name == "HookName")
            {
                eachChild.GetComponent<TextMeshProUGUI>().text = name;
            }
        }
    }

    public string getDescription(bool _activeDescription)
    {
        if (_activeDescription && isActive && unlocked) return activeDescription;
        else if(!_activeDescription) return inactiveDescripton;
        return "";
    }
    /*
    public void destroyButton()
    {
        //cleaning up the instantiated button.
        if (buttonInstance != null) Destroy(buttonInstance.gameObject);
    }
    public void instantiateButton()
    {
        //giving prefab button an instance to add to menus.
        Debug.Log("RoomFeatureSO:instantiateButton: " + name);
        if (button == null) Debug.LogError("RoomFeatureSO:instantiateButton: NO BUTTON: " + name);
        else buttonInstance = (GameObject)Instantiate(button);
    }
    */
}