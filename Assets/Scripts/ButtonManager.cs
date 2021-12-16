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
                if (feature.nameTag == _name) return feature;
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
                if (featureArray.nameTag == _name) return featureArray;
            }
        }
        Debug.LogError("ButtonManager:GetButtonArray: invalid name:" + _name);
        return null;
    }

    public void refreshText()
    {
        string text = "";
        TextManager.instance.setText("");
        if (activeButtons != null)
        {
            foreach(RoomFeatureSO feature in activeButtons)
            {
                TextManager.instance.addText(feature.description);
            }
        }
    }

    public void activateButton(RoomFeatureSO feature, bool turnOn = true)
    {
        if (feature == null) return;
        else if (turnOn)
        {
            if (hasButton(feature))
            {
                feature.buttonInstance.SetActive(true);
            }
            else
            {
                if (feature.unlocked) feature.buttonInstance.SetActive(true);
                else feature.buttonInstance.SetActive(false);

                feature.buttonInstance.transform.SetParent(buttonPanel.transform);
                feature.buttonInstance.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                activeButtons.Add(feature);
            }
        }
        else if (activeButtons != null)
        {
            foreach (RoomFeatureSO f in activeButtons)
            {
                if (f == feature)
                {
                    feature.buttonInstance.SetActive(false);
                }
            }
        }
        refreshText();
    }
    public void activateButtonArray(RoomFeatureArraySO buttonArray, bool turnOn = true)
    {
        if(buttonArray != null)
        {
            foreach(RoomFeatureSO f in buttonArray.features)
            {
                activateButton(f, turnOn);
            }
        }
        //adds buttons to scene
        //keeps tabs on buttons
    }

    public void unlockButton(RoomFeatureSO feature)
    {
        feature.buttonInstance.SetActive(false);
    }


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        activeButtons = new List<RoomFeatureSO>();
        buttonArray = Utils.GetAllScriptableObjects<RoomFeatureSO>();
        buttonArrayArray = Utils.GetAllScriptableObjects<RoomFeatureArraySO>();
    }

    private void Start()
    {
        resetButtons();
        instantiateButtons();

        activateButtonArray("SparseVegetation");
    }

    private void OnDestroy()
    {
        //clean up instantiation.
        if (buttonArray != null)
        {
            foreach (RoomFeatureSO button in buttonArray)
            {
                button.destroyButton();
            }
        }
    }

    void instantiateButtons()
    {
        if (buttonArray != null)
        {
            foreach (RoomFeatureSO button in buttonArray)
            {
                button.instantiateButton();
            }
        }
    }

    void resetButtons()
    {
        if(buttonArray != null)
        {
            foreach(RoomFeatureSO button in buttonArray)
            {
                button.reset();
            }
        }
    }
}
