using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFeatureArray", menuName = "Scriptable Object/FeatureArray")]
public class ButtonArraySO : ScriptableObject
{
    public new string name;
    public RoomFeatureSO[] features;
}
