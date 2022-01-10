using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCave : ButtonEffectScript
{
    public override void onStart()
    {
        ButtonManager.instance.activateButtonArray("EnterCave", false);
        ButtonManager.instance.activateButtonArray("StartingButtons");
        refreshTooltip();
    }
}
