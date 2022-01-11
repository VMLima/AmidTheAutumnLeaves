using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : EffectScript
{
    private PlayerAttributeSO Health;

    public override void onStart(int oldNumStacks, int addingStacks)
    {
        //Debug.Log("Medkit:onStart");
        Health = IncManager.instance.Get<PlayerAttributeSO>("Health");
        frequency = 10; //every 10 seconds
    }
    public override void onTick(int numEffects)
    {
        //Debug.Log("Medkit:onTick");
        //gain 1 health.
        IncManager.instance.Add(Health, numEffects);
    }
}