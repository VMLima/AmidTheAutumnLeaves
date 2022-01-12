using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather
{
    // All core basic weather variables used to determine weather effects.
    private float sun; // factors the diurnal temperature modifier
    private float timeShift; // The amount of seasonal variation

    // Biome-Specific Values
    private float diurnalTemp; // Diurnal temperature variation from sunlight
    private float seasonTemp; // Seasonal base temperature
    private float baseTemp; // Calculated base temp based on core weather, not modified by weather systems
    private float currTemp; // The current temperature. Recalculated every second.

    // Modified by weather systems, then used to determine effects
    private string currWeather; // String holding currently active weather pattern.
    private float wethTemp; // The temp modifier of the current weather, if any.
    private float wind; // Wind level from 0 (dead calm) to 12 (hurricane)
    private float clouds; // cloud cover factors diurnal sunlight
    private float rain; // Wetness/s. Wetness 0 is bone dry, 100 is drenched.
    private string windType; // Description of the level of wind. Purely aesthetic.
    private string rainType; // The description of the type of rain. Purely aesthetic.
    private float humidity; // 0 to 1, relative humidity
    private bool snow; // Flipped to 'true' when it's snowing, 'false' when not. 

    // Core
    public float Sun
    {
        get { return sun; }
        set
        {
            // Sanitizes sun values. 
            if (value < 0)  { sun = 0; }
            else if( value > 1.5f) { sun = 1.5f; }
            else { sun = Mathf.Round(1000 * value) / 1000; }
        }
    }
    public float TimeShift
    {
        get { return timeShift; }
        set { timeShift = value; }
    }

    // Biome
    public float DiurnalTemp
    {
        get { return diurnalTemp; }
        set { diurnalTemp = value; }
    }

    public float SeasonTemp
    {
        get { return seasonTemp; }
        set { seasonTemp = value; }
    }

    public float BaseTemp
    {
        get { return baseTemp; }
        set { baseTemp = value; }
    }
    public float CurrTemp
    {
        get { return currTemp; }
        set { currTemp = value; }
    }

    // Weather Effects
    public float WethTemp
    {
        get { return wethTemp; }
        set { wethTemp = value; }
    }
    public float Wind
    {
        get { return wind; }
        set { wind = value; }
    }

    public float Clouds
    {
        get { return clouds; }
        set
        {
            if (value < 0) { clouds = 0; }
            else { clouds = Mathf.Round(1000 * value) / 1000; }
        }
    }
    public float Rain
    {

        get { return rain; }
        set
        {
            // Sanitize the rain value
            if(value < 0) { rain = 0; }
            if (value > 3) { rain = 3; }
            else { rain = value; }
        }
    }
    public float Humidity
    {
        get { return humidity; }
        set { humidity = value; }
    }

    public string RainType
    {
        get { return rainType; }
        set { rainType = value; }
    }

    public string WindType
    {
        get { return windType; }
        set { windType = value; }
    }
    public bool Snow
    {
        get { return snow; }
        set { snow = value; }
    }
}
