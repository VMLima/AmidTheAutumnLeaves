using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCave : ButtonEffectScript
{
    bool toggle = false;
    public override void onStart()
    {
        ButtonManager.instance.addButtonArrayToUI("StartingButtons", false);
        ButtonManager.instance.addButtonArrayToUI("EnterCave");
        refreshTooltip();
    }
}
