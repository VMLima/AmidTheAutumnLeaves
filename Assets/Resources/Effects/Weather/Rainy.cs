using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainy : EffectScript
{
    //WILL GENERALLY REFERENCE STUFF IN THE Player.cs file.
    //  health, warmth.
    //  might have to make functions to set warmth.

    int weatherCounter = 0;
    public override void onTick(int _currentStacks = 1)
    {
        //whatever it does every Frequency seconds.   
        if (weatherCounter < duration / 4)
        {
            //the weather is just starting up
            //add 0.1 warmth per second.
            Player.instance.modWarmth(-0.1f);
        }
        else if (weatherCounter < duration * 3 / 4)
        {
            //the weather is in its core
            Player.instance.modWarmth(-0.25f);
        }
        else
        {
            //the weather is winding down
            Player.instance.modWarmth(-0.1f);
        }
        weatherCounter++;

        //Debug.Log("Rainy:onTick:tick");
    }

    //when (addingStacks) stacks.
    public override void onStart(int oldNumStacks, int addingStacks)
    {
        //stuff that changes on weather effect starts
    }

    public override void onStop(int oldNumStacks, int removingStacks)
    {
        //stop everything changed in start

        //TIE INTO FUNCTION THAT GETS NEW RANDOM WEATHER
        WeatherManager.instance.pickRandomWeather();
    }
}
