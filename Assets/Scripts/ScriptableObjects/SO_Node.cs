using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "NewNode", menuName = "Scriptable Object/Node")]
public class SO_Node : ScriptableObject
{
    public string name;
    //public string description;
    //public Button[] buttons;

    public int numRandomFeatures;
    public SO_Feature[] presetFeatures;
    
    //UI to be added to
    //placement order in UI
}