using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//inherits unlocking and button activating.
[CreateAssetMenu(fileName = "PlayerAttribute", menuName = "Scriptable Object/PlayerAttribute")]
public class PlayerAttributeSO : IncrementableSO
{

    public override void declareUI()
    {
        UIPrefab = IncManager.instance.StatPrefab;
        UIPanel = IncManager.instance.StatPanel;
    }

    public override void reset()
    {
        base.reset();
        amount = 50;
    }

    public override void whenUnlocked()
    {
        isActive = true;
        base.whenUnlocked();
    }
}
