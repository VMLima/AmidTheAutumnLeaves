using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : ButtonEffectScript
{
    public override void onStart()
    {
        ButtonManager.instance.activateButtonArray("Transition", false);
        ButtonManager.instance.activateButtonArray("Start");
    }
}
