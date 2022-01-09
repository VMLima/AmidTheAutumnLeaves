using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestRiverButtonScript : ButtonEffectScript
{
    bool toggle = false;
    public override void onStart()
    {
        //ButtonManager.instance.activateButton("ForestForest", toggle);
        //toggle = !toggle;
        //Debug.Log("Turning off Start");
        ButtonManager.instance.activateButtonArray("Start", false);
        //Debug.Log("Turning on Transition");
        ButtonManager.instance.activateButtonArray("Transition");
    }
}
