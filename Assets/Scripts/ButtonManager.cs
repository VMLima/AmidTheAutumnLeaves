using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    List<RoomFeatureSO> activeButtons;

    public GameObject buttonPanel;
    private int buttonIndex;

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
        addButtonToUI(GetButton(_name), turnOn);
    }

    public void activateButtonArray(string _name, bool turnOn = true)
    {
        addButtonArrayToUI(GetButtonArray(_name), turnOn);
    }

    RoomFeatureSO GetButton(string _name)
    {
        return IncManager.instance.Get<RoomFeatureSO>(_name);
    }

    RoomFeatureArraySO GetButtonArray(string _name)
    {
        return IncManager.instance.Get<RoomFeatureArraySO>(_name);
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

    //can be called to untoggle currently toggled buttons.
    public void untoggleIfPressed(string _name)
    {
        untoggleIfPressed(GetButton(_name));
    }

    public void untoggleIfPressed(RoomFeatureSO button)
    {
        if(button != null) button.haltEffects();
    }

    public void hideButtonInUI(string _name)
    {
        hideButtonInUI(GetButton(_name));
    }

    public void hideButtonInUI(RoomFeatureSO button)
    {
        if (button != null) addButtonToUI(button, false);
    }
    public void addButtonToUI(RoomFeatureSO button, bool turnOn = true)
    {
        
        if (button == null) return;

        button.activate(turnOn);   //toggles if it shows even if parented to the buttonPanel. and changes the text output. 
        if (turnOn && !hasButton(button))
        {
            button.setUIIndex(buttonIndex);
            buttonIndex++;
            activeButtons.Add(button);
        }
        refreshText();
    }
    public void addButtonArrayToUI(RoomFeatureArraySO buttonArray, bool turnOn = true)
    {
        if(buttonArray != null)
        {
            foreach(RoomFeatureSO feature in buttonArray.features)
            {
                addButtonToUI(feature, turnOn);
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
        buttonIndex = 0;
    }

    public void craft(string _name, float numCrafts = 1)
    {
        CraftRecipeSO c = IncManager.instance.Get<CraftRecipeSO>(_name);
        if (c != null) c.craft(numCrafts);
        return;
    }

    public void craft(CraftRecipeSO recipe, float numCrafts = 1)
    {
        recipe.craft(numCrafts);
    }

    private void Start()
    {
        
    }
}
