using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFeatureArray", menuName = "Scriptable Object/FeatureArray")]
public class RoomFeatureArraySO : CommonBaseSO
{
    public RoomFeatureSO[] features;
}
