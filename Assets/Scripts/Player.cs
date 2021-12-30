using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    List<ItemSO> inventory;

    private float healthMax = 100;
    private float healthCurrent;
    private float healthRate;

    private float staminaMax = 100;
    private float staminaCurrent;
    private float staminaRate;

    private float warmthMax = 100;
    private float warmthCurrent;
    private float warmthRate;

    //thirst
    //weather -> warmth -> thirst/hunger rate change.
    //    weather-> Effects.  Since it ticks every duration.
    //    warmth -> EverySecond() below, can be another function called in that function that tags along and computes.
    //      thirst/hunger rate change

    //items -> +thirst/hunger.
    //     Tim got dis.

    public static Player instance;
    private void Awake()
    {
        instance = this;
        defaultValues();
    }

    void defaultValues()
    {
        healthCurrent = healthMax;
        healthRate = 0.1f;
        staminaCurrent = 100;
        staminaRate = 1;
        warmthCurrent = 100;
        warmthRate = 1;
    }

    //ideally stuff that effects healthRate and staminaRate will be added into the value.
    //so it is 1 uniformed tick.  Can remake rate into an array that is summed up to create a final value.
    //  effects that effect rate can simply add an element to it?  or how about a string-float pair.  can add and, when over, remove by key.
    //  or can make an effect.  each stack of health regen is 0.01? can go negative?
    //  so then if you want to modify health per second you just do EffectManager.instance.addStack("HealthPerSec", 100)
    //      I dunno, I'll work it out later.
    public void everySecond()
    {
        regen(ref healthCurrent, healthRate, healthMax);
        regen(ref staminaCurrent, staminaRate, staminaMax);
        regen(ref warmthCurrent, warmthRate, warmthMax);

        //warmthCurrent
        //THIRST/HUNGER RATE CALCULATIONS
        //thirst += (warmthCurrent - meanTemp)*blah

        //Debug.Log("Player:everySecond: warmthCurrent:" + warmthCurrent);

        cleanUpValue(ref healthCurrent);
        cleanUpValue(ref staminaCurrent);
        cleanUpValue(ref warmthCurrent);
    }

    void cleanUpValue(ref float value)
    {
        value = Mathf.Round(value * 100f) / 100f;   //round these to 2 decimal places.  Stops a lot of weird rounding errors.
    }

    public float getHealth()
    {
        return healthCurrent;
    }
    
    public void modHealth(float _amount)
    {
        regen(ref healthCurrent, _amount, healthMax);
    }

    public float getStamina()
    {
        return staminaCurrent;
    }

    public void modStamina(float _amount)
    {
        regen(ref staminaCurrent, _amount, staminaMax);
    }

    void regen(ref float current, float rate, float max, float min = 0)
    {
        current += rate;
        if (current > max) current = max;
        else if (current < min) current = min;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
