using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickScript : EffectScript
{
    private int damagePerSec = 1;
    public override void effectOverride(int _currentStacks = 1)
    {
        EffectManager.instance.health -= damagePerSec* _currentStacks;
        Debug.Log("SickScript:effectOverride: sick tick : currentHealth = " + EffectManager.instance.health);
    }

    //when (addingStacks) stacks of the effect start.
    public override void onStartOverride(int oldNumStacks, int addingStacks)
    {
        bool firstSick = false;
        //remove 10 max health for the first stack of sickness and 2 more max health for each extra stack of sickness you get.
        if (oldNumStacks == 0)
        {
            //if this is our first stack of sickness, remove 10 max health.
            EffectManager.instance.health -= 10;
            addingStacks--;
            firstSick = true;
            
        }
        //for each additional stack of sickness we get, remove 2 more max health.
        EffectManager.instance.health -= 2* addingStacks;

        //DEBUG LOG OUTPUT FOR MY OWN TESTING PURPOSES
        if (firstSick)
        {
            Debug.Log("SickScript:onStartOverride: You have gotten " + (addingStacks+1) + " sick! currentHealth = " + EffectManager.instance.health);
        }
        else
        {
            Debug.Log("SickScript:onStartOverride: You have gotten " + addingStacks + " MORE sick! currentHealth = " + EffectManager.instance.health);
        }
    }

    //when (removingStacks) stacks of the effect ends.
    //  removingStacks is a positive number.
    public override void onStopOverride(int oldNumStacks, int removingStacks)
    {
        bool finalSick = false;
        //inverse of above.
        //removing the last stack of sickness cures 10 health.
        //removing each other stack of sickness cures 2 health.
        if((oldNumStacks-removingStacks)<=0)
        {
            //if removing the last stack of sickness, restore 10 health.
            
            EffectManager.instance.health += 10;
            removingStacks--;
            finalSick = true;
        }
        //gain 2 health back for each additional stack of sickness that ends.
        EffectManager.instance.health += 2 * (removingStacks);

        //DEBUG LOG OUTPUT FOR MY OWN TESTING PURPOSES
        if (finalSick)
        {
            Debug.Log("SickScript:onStopOverride: You have gotten " + (removingStacks+1) + " better! currentHealth = " + EffectManager.instance.health);
        }
        else
        {
            Debug.Log("SickScript:onStopOverride: You have gotten " + removingStacks + " LESS sick! currentHealth = " + EffectManager.instance.health);
        }
    }
}
