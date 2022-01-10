using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIncrementableSet", menuName = "Scriptable Object/IncrementableSet")]
public class IncArraySO : CommonBaseSO
{
    public IncrementalValuePair[] IncrementableArray = new IncrementalValuePair[0];
}