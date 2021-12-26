using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    //get all buttons
    //get all button lists.

    List<RoomFeatureSO> activeButtons;
    RoomFeatureSO[] buttonArray;
    RoomFeatureArraySO[] buttonArrayArray;
    CraftSO[] craftArray;

    public GameObject buttonPanel;

    [HideInInspector] public static ButtonManager instance;

    public bool hasButton(RoomFeatureSO feature)
    {
        if (activeButtons != null)
        {
            foreach (RoomFeatureSO f in activeButtons)
            {
                if (f == feature)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void activateButton(string _name, bool turnOn = true)
    {
        activateButton(GetButton(_name), turnOn);
    }

    public void activateButtonArray(string _name, bool turnOn = true)
    {
        activateButtonArray(GetButtonArray(_name), turnOn);
    }

    RoomFeatureSO GetButton(string _name)
    {
        if(buttonArray != null)
        {
            foreach(RoomFeatureSO feature in buttonArray)
            {
                if (feature.name == _name) return feature;
            }
        }
        Debug.LogError("ButtonManager:GetButton: invalid name:" + _name);
        return null;
    }

    RoomFeatureArraySO GetButtonArray(string _name)
    {

        if (buttonArray != null)
        {
            foreach (RoomFeatureArraySO featureArray in buttonArrayArray)
            {
                if (featureArray.name == _name) return featureArray;
            }
        }
        Debug.LogError("ButtonManager:GetButtonArray: invalid name:" + _name);
        return null;
    }

    public void refreshText()
    {
        TextManager.instance.setText("");
        if (activeButtons != null)
        {
            foreach (RoomFeatureSO feature in activeButtons)
            {
                TextManager.instance.addText(feature.getDescription(true));
            }
            TextManager.instance.addText("  ");
            foreach (RoomFeatureSO feature in activeButtons)
            {
                TextManager.instance.addText(feature.getDescription(false));
            }
        }
    }

    public void activateButton(RoomFeatureSO button, bool turnOn = true)
    {
        
        if (button == null) return;

        button.activate(turnOn);   //toggles if it shows even if parented to the buttonPanel. and changes the text output. 
        if (turnOn && !hasButton(button))
        {
            activeButtons.Add(button);
        }
        refreshText();
    }
    public void activateButtonArray(RoomFeatureArraySO buttonArray, bool turnOn = true)
    {
        if(buttonArray != null)
        {
            foreach(RoomFeatureSO feature in buttonArray.features)
            {
                activateButton(feature, turnOn);
            }
        }
        //adds buttons to scene
        //keeps tabs on buttons
    }

    public void unlockButton(RoomFeatureSO feature)
    {
        feature.unlock();
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        activeButtons = new List<RoomFeatureSO>();
        buttonArray = Utils.GetAllScriptableObjects<RoomFeatureSO>();
        buttonArrayArray = Utils.GetAllScriptableObjects<RoomFeatureArraySO>();
        craftArray = Utils.GetAllScriptableObjects<CraftSO>();
    }

    public void craft(string toCraft, float numCrafts = 1)
    {
        if (craftArray == null) return;
        foreach(CraftSO c in craftArray)
        {
            if (c.name == toCraft)
            {
                c.craft(numCrafts);
                return;
            }
        }
        Debug.LogError("ButtonManager:craft: could not find name:" + name);
    }

    public void craft(CraftSO recipe, float numCrafts = 1)
    {
        recipe.craft(numCrafts);
    }

    private void Start()
    {
        //resetButtons();
        //instantiateButtons();

        activateButtonArray("Start");
    }
}
