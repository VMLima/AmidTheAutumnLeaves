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
    public string description;
    public GameObject button;
    [HideInInspector] public GameObject buttonInstance;
    public override void whenUnlocked()
    {
        Debug.Log("RoomFeatureSO:whenUnlocked: unlocking: " + nameTag);
        //NodeManager.Instance.unlockFeature(this);
        if (buttonInstance == null) Debug.LogError("RoomFeatureSO:whenUnlocked: NO BUTTON: " + nameTag);
        else ButtonManager.instance.unlockButton(this);
        buttonInstance.SetActive(true);
    }

    public override void reset()
    {
        //ANYTHING ADDED THAT SHOULD BE RESET ON NEW GAME NEEDS TO BE RESET HERE.
        base.reset();
    }

    public void destroyButton()
    {
        if (button == null) Debug.LogError("RoomFeatureSO:instantiateButton: NO BUTTON: " + nameTag);
        else Destroy(buttonInstance.gameObject);
    }
    public void instantiateButton()
    {
        if(button == null) Debug.LogError("RoomFeatureSO:instantiateButton: NO BUTTON: " + nameTag);
        else buttonInstance = (GameObject)Instantiate(button);
    }
}