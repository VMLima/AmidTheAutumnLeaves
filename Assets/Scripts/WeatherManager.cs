using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public Weather weather = new Weather();
    // 
    private float diurnalTemp = 15f; // 15 for european forest
    private float Evaporation; // Wetness loss per second
    private float sweatRate; // Wetness gained per second
    
    public float deltaTemp = 0; // Change in player body temperature
    public float deltaWet = 0; // change in wetness per second
    public float deltaThirst = 0; // Change in thirst from weather
    public float deltaFood = 0; // Change in food from weather

    // Used in temperature change calculations method.
    private float tempFactor =0;
    private float windFactor=1;
    private float watrFactor=1;
    private float windChill = 0;


    // TEMPORARY
    private float skillEnduranceMod = 1; // Modifier from Endurance.  Should be (1+End^1.33)/166 

    List<EffectScript> summerEffects;
    GameObject summerObject;
    List<EffectScript> springEffects;
    GameObject springObject;
    List<EffectScript> winterEffects;
    GameObject winterObject;
    EffectScript[] autumnEffects;   //is a list of all the effects.
    GameObject autumnObject;    //can ignore the objects

    public static WeatherManager instance;
    EffectManager effectManager;

    string season = "autumn";   //at some point seasons may change, for now this is good enough.

    public void pickRandomWeather()
    {
        //do stuff with weatherEffects
        if (season == "autumn")
        {
            //the line below starts the effect of the 0th index autumn effect.
            EffectManager.instance.startEffect(autumnEffects[0]);

            //the line below starts an autumnEffect named "rainy"
            //getEffect has a method that finds the effect named "rainy" in the effect list autumnEffects.
            EffectManager.instance.startEffect(EffectManager.instance.getEffect(autumnEffects, "Rainy"));
        }
    }

    public void PerSecondWeather() // Applies weather effects to the player.
    {
        PlayerAttributeSO Thirst = IncManager.instance.Get<PlayerAttributeSO>("Thirst");
//        PlayerAttributeSO Hunger = IncManager.instance.Get<PlayerAttributeSO>("Hunger");

        Sunlight(); // figures out how much sunlight affects the temperature
        TimeofDay(); // updates the time of day string
        Temperature(); // find the exterior weather temperature, must be before Wetness.

 //       Debug.Log("Wetness before delta: " + Player.instance.Wetness + " BodyTemp before delta: " + Player.instance.BodyTemp);

        Player.instance.Wetness += Wetness(); // Sweat, evaporation, and rain. Must be before ChangeInTemp.
        Player.instance.BodyTemp += ChangeInTemperature(); // Temp change based on weather.

//        Debug.Log("Wetness after delta: " + Player.instance.Wetness + " BodyTemp after delta: " + Player.instance.BodyTemp + "deltaTemp: " + deltaTemp + " deltaWetn: " + deltaWet);
        // Subtract sweat from player's current thirst.
        Thirst.addAmount(-1 * sweatRate);

//        Debug.Log("Sun:" + weather.Sun + " Temp:" + weather.CurrTemp + " deltaTemp:" + deltaTemp + " player wetness:" + Player.instance.Wetness);

    }

    public float ChangeInTemperature() // Figures out how fast the player's temperature changes.
    {
        PlayerAttributeSO Thirst = IncManager.instance.Get<PlayerAttributeSO>("Thirst");
        //        PlayerAttributeSO Hunger = IncManager.instance.Get<PlayerAttributeSO>("Hunger");
        // Set deltaTemp to a fresh 0.
        deltaTemp = 0;

        if (weather.CurrTemp <= 68)
        {
            // Calculates approximate wind chill.
            windFactor = Mathf.Abs(weather.CurrTemp - 40) / 20;
            windChill = windFactor * (weather.Wind / 1.2f);

            // Calculates the effects of player's current wetness that magnify temp loss.
            watrFactor = 1 + Player.instance.Wetness / 150 + weather.Humidity / 1.2f;

            // Calculates the base temperature loss that gets divided by insulation.
            tempFactor = 0.1f + (Mathf.Abs(weather.CurrTemp - 68 - windChill) / 70);

            // Returns a value for how much the player's temperature changes per second.            
            deltaTemp -= (tempFactor * watrFactor) / (11.5f + (GearManager.instance.gear.ColdInsulation / (1 + Player.instance.Wetness)));
        }
        else if (weather.CurrTemp >= 80)
        {
            // Simple version, if you can sweat enough you're fine, otherwise bad things, but quite not as bad with protection.
            if(Thirst.amount > 0) { deltaTemp = 0; }
            else
            {
                // Wind mildly increases heat transfer
                windFactor = 1 + weather.Wind / 20;

                // Produces gradually increasing base value that is divided later
                tempFactor = Mathf.Pow((weather.CurrTemp - 80), 0.33f);

                // 
                deltaTemp += tempFactor * windFactor * (1+weather.Humidity) * weather.Sun / GearManager.instance.gear.HeatInsulation;
            }
        }
        else
        {
            deltaTemp = 0;
        }

        return deltaTemp;
    }

    void Temperature()
    {
        // Does all of the math to figure out the current base temperature outside.


        // Calculates base temperature for the outside world. Cloud albedo ranges from 0 to 0.9
        // In general cloud values should be aimed at ~0 to 0.2 for nice weather, higher for especially bad weather.
        // Could in theory use negative cloud values to sim high clouds that reflect heat back at the earth but
        // let's not get too crazy.
        weather.BaseTemp = weather.WethTemp + (int)(weather.SeasonTemp + diurnalTemp * weather.Sun * (1 - (weather.Clouds * 0.9)));

        // The version that gets messed with.
        weather.CurrTemp = weather.BaseTemp;

        if (Player.instance.InShelter == true)
        {
            // Placeholder, if in shelter it sets current temp to 70 no matter what conditions are outside.
            weather.CurrTemp = 70;
        }
    }

    public float Wetness()
    {
        // Freshly sets per second change in wetness to 0.
        WeatherManager.instance.deltaWet = 0;

        // Effects of rainfall
        if (weather.Snow == false && Player.instance.InShelter == false) // If you're not under cover and it's raining
        {
            deltaWet += weather.Rain * GearManager.instance.gear.WaterRes;
        }
        else if (weather.Snow == false && Player.instance.InShelter == true) // if you're under cover and it's raining
        {
            deltaWet += (weather.Rain * Player.instance.ShelterCover * GearManager.instance.gear.WaterRes);
        }
        else { } // if it is snowing, you don't get rained on.


        // Calculates how much wetness loss per second based on Temp, humidty, and wind. Wind is the dominant lever.
        // If you're curious, the math works out such that drying from 'drenched' to 'dry' takes ~3 hours in 70 F, 
        // mildly breezy weather at 50% (0.5) relative humidity.
        WeatherManager.instance.Evaporation = 0.2777f * ((weather.CurrTemp + 100) / 100) * (1.25f - weather.Humidity) * (1 + weather.Wind / 7);

        // If you have a campfire going, you dry off faster. 
        if (Player.instance.campfire == true) { Evaporation += 1; }

        Sweat();
        Evaporate();

        // Apply values to wetness change this second.
        WeatherManager.instance.deltaWet += WeatherManager.instance.sweatRate;
        WeatherManager.instance.deltaWet -= WeatherManager.instance.Evaporation;
        return deltaWet;
    }
    public float Sweat()
    {
        PlayerAttributeSO Thirst = IncManager.instance.Get<PlayerAttributeSO>("Thirst");

        // The amount of water sweated out per second based on the weather assuming you have water to sweat out.
        sweatRate = 0.00034721666f * Player.instance.workDiff * (230 + weather.CurrTemp) * (1.4f - weather.Humidity) / skillEnduranceMod;

        if (Thirst.amount <= 0) { sweatRate = 0; }
        else if (Thirst.amount - sweatRate < 0 && Thirst.amount > 0) { sweatRate = Thirst.amount; }


        return sweatRate;
    }

    public float Evaporate()
    {

        if (Player.instance.Wetness <= 0) { Evaporation = 0; }
        else if(Player.instance.Wetness < Evaporation) { Evaporation = Player.instance.Wetness; }

        return Evaporation;
    }


    public void Sunlight()
    {
        // Figures out how 'up' the sun is and returns a value between ~-1.7 and 1.7 that's used to modify diurnal variation.

        // Returns -1 or 1 so seasonal variation affects sunrise and sunset
        if(TimeManager.instance.times.Minutes <=720) { TimeManager.instance.times.AMorPM = 1; }
        else if (TimeManager.instance.times.Minutes >= 721) { TimeManager.instance.times.AMorPM = -1; }

        // Uses the above variables to calculate the 'upness' of the sun, which is used to factor
        // the weather.diurnalTemp modifier and figure out what time of day it is.*
        weather.Sun = (450 - Mathf.Abs(TimeManager.instance.times.Minutes - 780) + (TimeManager.instance.times.AMorPM * weather.TimeShift)) / (300 + weather.TimeShift);

    }

    public void TimeofDay()
    {
        // The if stack to determine the time of day based on how 'up' the sun is. 
//        Debug.Log("Working" + TimeManager.instance.times.AMorPM);

        if (weather.Sun <= 0)
        {
//            Debug.Log("I think the sun isn't out!");
            // Differentiates between different times while sun is down.
            if (TimeManager.instance.times.Minutes < 286) { TimeManager.instance.times.TimeOfDay = "Wee Hours"; return; }
            else if(TimeManager.instance.times.Minutes >= 286 && TimeManager.instance.times.Minutes <= 600) { TimeManager.instance.times.TimeOfDay = "Predawn"; return; }
            else if (TimeManager.instance.times.Minutes > 1275) { TimeManager.instance.times.TimeOfDay = "Late Night"; return; }
            else if (TimeManager.instance.times.Minutes > 1200) { TimeManager.instance.times.TimeOfDay = "Night"; return; }
        }
        else if (weather.Sun > 0 && TimeManager.instance.times.AMorPM == 1)
        {
//            Debug.Log("I think the sun is out and it's morningish!");
            // Uses timeSwap to tell whether it's morning or evening
            if (weather.Sun > 0 && TimeManager.instance.times.Minutes > 719 && TimeManager.instance.times.Minutes < 780) { TimeManager.instance.times.TimeOfDay = "Midday"; return; }
            else if (weather.Sun <= 0.02) { TimeManager.instance.times.TimeOfDay = "Sunrise"; return; }
            else if (weather.Sun <= 0.17) { TimeManager.instance.times.TimeOfDay = "Dawn"; return; }
            else if (weather.Sun <= 0.34) { TimeManager.instance.times.TimeOfDay = "Early Morning"; return; }
            else if (weather.Sun <= 0.68) { TimeManager.instance.times.TimeOfDay = "Midmorning"; return; }
            else if (weather.Sun <= 1) { TimeManager.instance.times.TimeOfDay = "Late Morning"; return; }
 
        }
        else if (weather.Sun > 0 && TimeManager.instance.times.AMorPM == -1)
        {
//           Debug.Log("I think the sun is out and it's eveningish!");
            // Uses timeSwap to tell whether it's morning or evening
            if (weather.Sun <= 0.02) { TimeManager.instance.times.TimeOfDay = "Sunset"; return; }
            else if (weather.Sun <= 0.17) { TimeManager.instance.times.TimeOfDay = "Dusk"; return; }
            else if (weather.Sun <= 0.34) { TimeManager.instance.times.TimeOfDay = "Evening"; return; }
            else if (weather.Sun <= 0.68) { TimeManager.instance.times.TimeOfDay = "Late Afternoon"; return; }
            else if (weather.Sun <= 1) { TimeManager.instance.times.TimeOfDay = "Afternoon"; return; }

        }

    }
    public void PerDayWeather()
    {

        // Calculates seasonal time shift based on day of the year, the '0.5' can be adjusted 
        //later to return different seasonal variations. In its current form it should cause 
        // the day to be roughly 6 hours longer at the summer solstice and vice versa.
        weather.TimeShift.Equals(0.5 * (Mathf.Abs(TimeManager.instance.times.Day - 180) - 90));

    }

    void Awake()
    {
        instance = this;
        loadSeasonData();
    }

    private void Start()
    {
        TimeManager.instance.incrementSeason();
        PerDayWeather();
    }

    //called when game ends.
    private void OnDestroy()
    {
        deleteSeasonData();
    }

    void deleteSeasonData()
    {
        //deletes autumn object.  Guess it's good because we instantiated it.
        GameObject.Destroy(autumnObject);
    }

    void loadSeasonData()
    {
        //this loads the data for autumn.
        autumnObject = Utils.GetWeatherObject("AutumnEffects"); //gets the GameObject called AutumnEffects
        autumnObject = (GameObject)Instantiate(autumnObject, transform);    //creates an instance of it.
        autumnEffects = autumnObject.GetComponents<EffectScript>();     //gets the components of it that are EffectScripts.

        //debug log to make sure it is working.
        for (int i = 0; i < autumnEffects.Length; i++)
        {
            Debug.Log("autumnEffect loaded:" + autumnEffects[i].name);
        }
    }
}
