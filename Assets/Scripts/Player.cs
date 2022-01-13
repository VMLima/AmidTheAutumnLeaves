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


    private float wetness; // How wet the player is from 0 (bone dry) to 100 (drenched from head to toe)
    private float bodyTemp = 0; // How comfortable the player is from -20 (dying of hypothermia) to 20 (dying of heatstroke)
    public int workDiff = 1; // Will be in the equation but always set to 1 for now.

    // May end up not being used for now.
    public bool inShelter = false; // FALSE if you're wandering around, TRUE if you're in some kind of shelter.
    public bool campfire = false; // Whether or not you have a campfire going.
    private float shelterCover = 0; // Current overhead cover from 0 to 1.
    private float shelterWindBlock = 0; // Current protection from the wind from 0 to 1.

    // Methods which allow you to change these values, but ONLY using get/set so that all attempts to increment these
    // must pass through these methods, allowing us to ensure that everything that modifies them triggers everything it
    // should and the values are clamped appropriately, etc.

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
        TimeManager.instance.incrementMinute();
        WeatherManager.instance.PerSecondWeather();

        regen(ref healthCurrent, healthRate, healthMax);
        regen(ref staminaCurrent, staminaRate, staminaMax);
        regen(ref warmthCurrent, warmthRate, warmthMax);
        IncManager.instance.AddAmount(hunger, 1);
        IncManager.instance.AddAmount(thirst, 0.1f);

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

    public bool InShelter
    {
        get { return inShelter; }
        set { inShelter = value; }
    }
    public float Wetness
    {
        get { return wetness; }
        set
        {
            if (value > 100) { wetness = 100; }
            if (value < 0) { wetness = 0; }
            else { wetness = value; }
        }
    }

    public float BodyTemp
    {
        get { return bodyTemp; }
        set
        {
            if (value > 20) { bodyTemp = 20; }
            if (value < -20) { bodyTemp = -20; }
            else { bodyTemp = value; }
        }
    }
    public float ShelterCover
    {   // Don't panic, currently unused.
        get { return shelterCover; }
        set
        {
            if (value < 0) { shelterCover = 0; }
            else if (value > 0) { shelterCover = 1; }
            else { shelterCover = value; }
        }
    }
    public float ShelterWind
    {
        // Don't panic, currently unused.
        get { return shelterWindBlock; }
        set
        {
            if (value < 0) { shelterWindBlock = 0; }
            else if (value > 0) { shelterWindBlock = 1; }
            else { shelterCover = value; }
        }
    }
}
