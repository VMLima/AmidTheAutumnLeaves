using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickScript : EffectScript
{
    private int damagePerSec = 1;
    public override void effectOverride(int _numStacks)
    {
        EffectManager.instance.health -= damagePerSec* _numStacks;
        Debug.Log("SickScript:effectOverride: sick tick : currentHealth = " + EffectManager.instance.health);
    }

    public override void effectStackOverride()
    {
        //ONLY FUNCTIONS IF 'Stack Effects' IS CHECKED OFF.
        //then this will be the only effect every time a new application of the effect happens.
        //base.effectStackOverride() simply resets the duration back to full.
        base.effectStackOverride();
    }

    //when the effect starts.
    public override void onStartOverride(int numStacks)
    {
        //  numStacks is ONLY RELEVANT IF 'Stack Effects' IS CHECKED ON.  otherwise it will always be 1.

        //remove 10 max health for the first stack of sickness you get and 2 more max health for each extra stack of sickness you get.
        //getNumEffects() gives you the number of stacks BEFORE numStacks is factored it.
        if (getNumEffects() == 0)
        {
            //if this is our first stack of sickness, remove 10 max health.
            EffectManager.instance.health -= 10;
            numStacks--;
        }
        //for each additional stack of sickness we get, remove 2 more max health.
        EffectManager.instance.health -= 2*numStacks;
        
        Debug.Log("SickScript:onStartOverride: You have gotten sick! currentHealth = " + EffectManager.instance.health);
    }

    //when the effect ends.
    public override void onStopOverride(int numStacks)
    {
        //gain 2 health back for each stack of sickness that gets cured or ends.
        EffectManager.instance.health += 2 * (numStacks);
        Debug.Log("SickScript:onStopOverride: You have gotten better! currentHealth = " + EffectManager.instance.health);
    }
}
