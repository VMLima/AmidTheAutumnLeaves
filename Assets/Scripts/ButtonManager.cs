using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    List<RoomFeatureSO> activeButtons;

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
