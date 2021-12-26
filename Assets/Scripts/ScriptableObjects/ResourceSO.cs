using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "NewResource", menuName = "Scriptable Object/Basic/Resource")]
public class ResourceSO : IncrementableSO
{
    public override void declareUI()
    {
        UIPrefab = Utils.GetPrefab("ResourcePrefab");    //still gotta make and hook up to the prefab.
        UIPanel = IncManager.instance.ResourcePanel;
    }
}