using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExit : ButtonEffectScript
{
    public override void onStart()
    {
        ButtonManager.instance.addButtonArrayToUI("EnterCave", false);
        ButtonManager.instance.addButtonArrayToUI("StartingButtons");
        refreshTooltip();
    }
}
