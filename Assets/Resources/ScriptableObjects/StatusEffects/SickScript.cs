using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickScript : StatusScript
{
    private int damagePerSec = 1;
    public override void effectOverride()
    {
        StatusManager.instance.health -= damagePerSec;
        Debug.Log("SickScript:effectOverride: currentHealth = " + StatusManager.instance.health);
    }

    public override void effectStackOverride()
    {
        
        base.effectStackOverride(); //causes duration to be reset.
        damagePerSec++; //take  more damage per second on stack.
    }

    public override void onStartOverride()
    {
        StatusManager.instance.health -= 20;
        Debug.Log("SickScript:onStartOverride: currentHealth = " + StatusManager.instance.health);
    }

    public override void onStopOverride()
    {
        StatusManager.instance.health += 20;
        Debug.Log("SickScript:onStopOverride: currentHealth = " + StatusManager.instance.health);
    }
}
