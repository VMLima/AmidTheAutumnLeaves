using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : ButtonEffectScript
{
    private void Start()
    {
        toggleButton = true;
        //IncManager.instance.startDarkness();
        //IncManager.instance.endDarkness();
        //ButtonManager.instance.activateButtonArray("DarknessArray");
    }
    bool toggle = false;
    public override void onStart()
    {
        FlashlightManager.ShowFlashlight_Static();
        TooltipManager.StartEvent_Static();
        
    }
    public override void onStop()
    {
        FlashlightManager.HideFlashlight_Static();
        TooltipManager.StopEvent_Static();
    }
}
