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

    public PlayerAttributeSO stamina;
    public PlayerAttributeSO water;
    public PlayerAttributeSO health;

    private IncManager incM;

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
        //defaultValues();
    }

    public void defaultValues()
    {
        health = incM.Get<PlayerAttributeSO>("Health");
        stamina = incM.Get<PlayerAttributeSO>("Stamina");
        water = incM.Get<PlayerAttributeSO>("Water");
        incM.Set(health, 45);
        incM.Set(stamina, 80);
        incM.Set(water, 100);
    }

    //ideally stuff that effects healthRate and staminaRate will be added into the value.
    //so it is 1 uniformed tick.  Can remake rate into an array that is summed up to create a final value.
    //  effects that effect rate can simply add an element to it?  or how about a string-float pair.  can add and, when over, remove by key.
    //  or can make an effect.  each stack of health regen is 0.01? can go negative?
    //  so then if you want to modify health per second you just do EffectManager.instance.addStack("HealthPerSec", 100)
    //      I dunno, I'll work it out later.
    public void everySecond()
    {
        incM.Add(stamina, -0.1f);
        incM.Add(water, -0.1f);
    }

    void cleanUpValue(ref float value)
    {
        value = Mathf.Round(value * 100f) / 100f;   //round these to 2 decimal places.  Stops a lot of weird rounding errors.
    }

    public float getHealth()
    {
        return healthCurrent;
    }
    public float getStamina()
    {
        return staminaCurrent;
    }
    public float getWarmth()
    {
        return warmthCurrent;
    }

    public void modHealth(float _amount)
    {
        regen(ref healthCurrent, _amount, healthMax);
    }

    public void modStamina(float _amount)
    {
        regen(ref staminaCurrent, _amount, staminaMax);
    }

    public void modWarmth(float _amount)
    {
        regen(ref warmthCurrent, _amount, warmthMax);
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
        incM = IncManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
