using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunnyDay : EffectScript
{
    int weatherCounter = 0;
    public override void onTick(int _currentStacks = 1)
    {
        //whatever it does every Frequency seconds.   
        if (weatherCounter < 5)
        {
            //if weather is in the first 50 seconds do this stuff

        }
        else
        {

        }
        weatherCounter++;
    }

    //when (addingStacks) stacks.
    public override void onStart(int oldNumStacks, int addingStacks)
    {
        //everything to change when weather effect starts
        //player warmth + 10;
        
    }

    public override void onStop(int oldNumStacks, int removingStacks)
    {
        //stop everything changed in start
        //player warmth - 10;

       //TIE INTO FUNCTION THAT GETS NEW RANDOM WEATHER
    }
}
