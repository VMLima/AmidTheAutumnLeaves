using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : ButtonEffectScript
{
    private PlayerAttributeSO Health;

    private void Start()
    {
        toggleButton = true;
        frequency = 2;
    }

    public override void onStart(int oldNumStacks, int addingStacks)
    {
        //Debug.Log("Medkit:onStart");
        Health = IncManager.instance.Get<PlayerAttributeSO>("Health");
        
    }

    public override void onTick(int numEffects)
    {
        //normal stamina/thirst drain.
        Player.instance.everySecond();
        //recover health faster.
        IncManager.instance.Add(Health, 1);
    }
}
