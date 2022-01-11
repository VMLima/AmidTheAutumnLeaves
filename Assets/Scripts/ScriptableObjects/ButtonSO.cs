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

[CreateAssetMenu(fileName = "NewButton", menuName = "Scriptable Object/Button")]
public class ButtonSO : UIMenuSO
{
    //public string activeDescription;
    //public string inactiveDescripton;
    
    //private bool isActive;
    public Button button;
    private ButtonEffectScript buttonEffect;
    
    //[HideInInspector] public GameObject buttonInstance;

    //core functionality of unlocking the feature.
    //unlock() sets unlocked=true then goes here.

    public override void activate(bool turnOn)
    {
        if (turnOn == false) haltEffects();
        base.activate(turnOn);
        ButtonManager.instance.refreshText();
    }

    public override string compileTooltip()
    {
        string output = "";
        if (tooltipText != "") output += tooltipText + "\n";
        if(buttonEffect != null) output += buttonEffect.compileTooltip();
        return output;
    }

    public override void declareUI()
    {
        if (button != null) UIPrefab = button.gameObject;
        else Debug.LogError("ButtonSO:declareUI:ButtonSO named:" + name + ": does not have button linked in inspector.");
        UIPanel = IncManager.instance.ButtonPanel;
    }

    public override void reset()
    {
        base.reset();
        //buttonEffect = UIInstance.GetComponent<ButtonEffectScript>();
    }

    public override void setUIData()
    {
        buttonEffect = UIInstance.GetComponent<ButtonEffectScript>();
    }

    public void haltEffects()
    {
        if (buttonEffect != null) buttonEffect.haltEffects();
    }

    public string getDescription(bool _activeDescription)
    {
        //if (_activeDescription && isActive && unlocked) return activeDescription;
        //else if(!_activeDescription) return inactiveDescripton;
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