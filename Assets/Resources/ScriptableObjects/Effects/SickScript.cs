using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickScript : EffectScript
{
    private int damagePerSec = 1;
    public override void effectOverride()
    {
        EffectManager.instance.health -= damagePerSec;
        Debug.Log("SickScript:effectOverride: currentHealth = " + EffectManager.instance.health + " duration: " + timeLeft);
    }

    public override void effectStackOverride()
    {
        
        //base.effectStackOverride(); //causes duration to be reset.
        //damagePerSec++; //take  more damage per second on stack.
    }

    public override void onStartOverride()
    {
        EffectManager.instance.health -= 10;
        Debug.Log("SickScript:onStartOverride: currentHealth = " + EffectManager.instance.health);
    }

    public override void onStopOverride()
    {

        EffectManager.instance.health += 15;
        Debug.Log("SickScript:onStopOverride: currentHealth = " + EffectManager.instance.health);
    }
}
