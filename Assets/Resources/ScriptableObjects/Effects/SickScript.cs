using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickScript : EffectScript
{
    private int damagePerSec = 1;
    public override void effectOverride()
    {
        EffectManager.instance.health -= damagePerSec;
        Debug.Log("SickScript:effectOverride: sick tick : currentHealth = " + EffectManager.instance.health + " timeLeft: " + timeLeft);
    }

    public override void effectStackOverride()
    {
        //this test effect is using 'Stack Effect' instead of reseting duration or some such on new effect application.
        //only have this function still here as an example of how to right it and for the rough explanation.
    }

    //when the effect starts.
    public override void onStartOverride()
    {
        EffectManager.instance.health -= 10;
        Debug.Log("SickScript:onStartOverride: You have gotten sick! currentHealth = " + EffectManager.instance.health);
    }

    //when the effect ends.
    public override void onStopOverride()
    {

        EffectManager.instance.health += 10;
        Debug.Log("SickScript:onStopOverride: You have gotten better! currentHealth = " + EffectManager.instance.health);
    }
}
