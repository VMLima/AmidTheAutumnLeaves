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
    public GameObject[] buttons;
    public override void whenUnlocked()
    {
        Debug.LogError("SO_Root:whenUnlocked: USING DEFAULT in " + nameTag);
        NodeManager.Instance.unlockFeature(this);
    }

    public override void reset()
    {
        //ANYTHING ADDED THAT SHOULD BE RESET ON NEW GAME NEEDS TO BE RESET HERE.
        base.reset();
    }
}