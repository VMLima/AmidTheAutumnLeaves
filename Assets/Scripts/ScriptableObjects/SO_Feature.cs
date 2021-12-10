using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "NewFeature", menuName = "Scriptable Object/Feature")]
public class SO_Feature : ScriptableObject
{
    public string name;
    public string description;
    public GameObject[] buttons;
}