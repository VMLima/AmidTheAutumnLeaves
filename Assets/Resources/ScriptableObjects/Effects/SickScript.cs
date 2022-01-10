using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SickScript : EffectScript
{
    private int healthPerSec = -1;
    SkillSO Foraging;
    SkillSO NatureSence;
    IncManager incM;
    Player player;
    public override void onTick(int _currentStacks = 1)
    {
        Player.instance.modHealth(healthPerSec * _currentStacks);
        incM.Add(Foraging, 1);
        incM.Add(NatureSence, 1);
        Debug.Log("Foraging:" + Foraging.getAmount() + " Nature Sence:" + NatureSence.getAmount());
        Debug.Log("SickScript:effectOverride: sick tick : currentHealth = " + player.getHealth());
    }

    //when (addingStacks) stacks.
    public override void onStart(int oldNumStacks, int addingStacks)
    {
        
        incM = IncManager.instance;
        player = Player.instance;

        Foraging = incM.Get<SkillSO>("Foraging");
        NatureSence = incM.Get<SkillSO>("NatureSence");

        bool firstSick = false;
        //remove 10 max health for the first stack of sickness and 2 more max health for each extra stack of sickness you get.
        if (oldNumStacks == 0)
        {
            //if this is our first stack of sickness, remove 10 max health.
            incM.Add(Foraging, 8);
            player.modHealth(-10);
            addingStacks--;
            firstSick = true;
        }
        //for each additional stack of sickness we get, remove 2 more max health.
        player.modHealth(-2 * addingStacks);

        //DEBUG LOG OUTPUT FOR MY OWN TESTING PURPOSES
        if (firstSick)
        {
            Debug.Log("SickScript:onStartOverride: You have gotten " + (addingStacks+1) + " sick! currentHealth = " + player.getHealth());
        }
        else
        {
            Debug.Log("SickScript:onStartOverride: You have gotten " + addingStacks + " MORE sick! currentHealth = " + player.getHealth());
        }
    }

    //when (removingStacks) stacks.
    //  removingStacks is a positive number.
    public override void onStop(int oldNumStacks, int removingStacks)
    {
        bool finalSick = false;
        //inverse of above.
        //removing the last stack of sickness cures 10 health.
        //removing each other stack of sickness cures 2 health.
        if((oldNumStacks-removingStacks)<=0)
        {
            //if removing the last stack of sickness, restore 10 health.
            player.modHealth(10);
            removingStacks--;
            finalSick = true;
        }
        //gain 2 health back for each additional stack of sickness that ends.
        player.modHealth(2 * (removingStacks));

        //DEBUG LOG OUTPUT FOR MY OWN TESTING PURPOSES
        if (finalSick)
        {
            Debug.Log("SickScript:onStopOverride: You have gotten " + (removingStacks+1) + " better! currentHealth = " + player.getHealth());
        }
        else
        {
            Debug.Log("SickScript:onStopOverride: You have gotten " + removingStacks + " LESS sick! currentHealth = " + player.getHealth());
        }
    }
}
