using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{

    List<ButtonSO> activeButtons;
    public GameObject buttonPanel;
    private int buttonIndex;

    [HideInInspector] public static ButtonManager instance;

    public bool hasButton(ButtonSO feature)
    {
        if (activeButtons != null)
        {
            foreach (ButtonSO f in activeButtons)
            {
                if (f == feature)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void addButtonToUI(string _name, bool turnOn = true)
    {
        addButtonToUI(GetButton(_name), turnOn);
    }

    public void addButtonArrayToUI(string _name, bool turnOn = true)
    {
        addButtonArrayToUI(GetButtonArray(_name), turnOn);
    }

    public void deleteButtons(string _partialName)
    {
        for(int i = buttonPanel.transform.childCount-1;i>=0 ;i--)
        {
            if (buttonPanel.transform.GetChild(i).name.Contains(_partialName)) Destroy(buttonPanel.transform.GetChild(i).gameObject);
        }
    }

    public void deactivateAllButtons()
    {
        
        for(int i = 0;i<activeButtons.Count;i++)
        {
            
            addButtonToUI(activeButtons[i], false);
        }
    }

    ButtonSO GetButton(string _name)
    {
        return IncManager.instance.Get<ButtonSO>(_name);
    }

    ButtonArraySO GetButtonArray(string _name)
    {
        return IncManager.instance.Get<ButtonArraySO>(_name);
    }

    public void refreshText()
    {
        TextManager.instance.setText("");
        if (activeButtons != null)
        {
            foreach (ButtonSO feature in activeButtons)
            {
                TextManager.instance.addText(feature.getDescription(true));
            }
            TextManager.instance.addText("  ");
            foreach (ButtonSO feature in activeButtons)
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

    public void untoggleIfPressed(ButtonSO button)
    {
        if(button != null) button.haltEffects();
    }

    public void hideButtonInUI(string _name)
    {
        hideButtonInUI(GetButton(_name));
    }

    public void hideButtonInUI(ButtonSO button)
    {
        if (button != null) addButtonToUI(button, false);
    }
    public void addButtonToUI(ButtonSO button, bool turnOn = true)
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
    public void addButtonArrayToUI(ButtonArraySO buttonArray, bool turnOn = true)
    {
        if(buttonArray != null)
        {
            foreach(ButtonSO feature in buttonArray.features)
            {
                addButtonToUI(feature, turnOn);
            }
        }
        //adds buttons to scene
        //keeps tabs on buttons
    }

    public void unlockButton(ButtonSO feature)
    {
        feature.unlock();
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        activeButtons = new List<ButtonSO>();
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
