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
        UIPrefab = null;    //still gotta make and hook up to the prefab.
        //UIImage
        //name
        //will be fed into the prefab.
        //textDisplay = (numerical text output panel.  Will get updated on addAmount())
        UIPanel = IncManager.instance.ResourcePanel;
    }
}