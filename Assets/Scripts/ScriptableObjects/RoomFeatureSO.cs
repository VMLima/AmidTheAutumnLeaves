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
public class RoomFeatureSO : UnlockableSO
{
    public string activeDescription;
    public string inactiveDescripton;
    
    private bool isActive;
    public GameObject button;
    [HideInInspector] public GameObject buttonInstance;

    //core functionality of unlocking the feature.
    //unlock() sets unlocked=true then goes here.
    public override void whenUnlocked()
    {
        //Debug.Log("RoomFeatureSO:whenUnlocked: unlocking: " + name);
        if (buttonInstance == null) Debug.LogError("RoomFeatureSO:whenUnlocked: NO BUTTON: " + name);
        else activate(true);
    }

    public override void reset()
    {
        //ANYTHING ADDED THAT SHOULD BE RESET ON NEW GAME NEEDS TO BE RESET HERE.
        base.reset();
        isActive = false;
    }

    public void activate(bool turnOn)
    {
        if (unlocked)
        {
            buttonInstance.SetActive(turnOn);
            isActive = turnOn;
        }
        else
        {
            buttonInstance.SetActive(false);
            isActive = false;
        }
        ButtonManager.instance.refreshText();
    }

    public string getDescription(bool _activeDescription)
    {
        if (_activeDescription && isActive) return activeDescription;
        else if(!_activeDescription) return inactiveDescripton;
        return "";
    }

    public void destroyButton()
    {
        //cleaning up the instantiated button.
        if (button == null) Debug.LogError("RoomFeatureSO:instantiateButton: NO BUTTON: " + name);
        else Destroy(buttonInstance.gameObject);
    }
    public void instantiateButton()
    {
        //giving prefab button an instance to add to menus.
        if(button == null) Debug.LogError("RoomFeatureSO:instantiateButton: NO BUTTON: " + name);
        else buttonInstance = (GameObject)Instantiate(button);
    }
}