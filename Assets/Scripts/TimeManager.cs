using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public Times times = new Times();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Sets all of the time properties to a default for testing/so nothing explodes
        times.Minutes = 0;
        times.AMorPM = 1;
        times.TimeOfDay = "blank";
        times.Day = 1;
        times.TimeFactor = 1;
        times.Weekday = 5;
        times.WeekdayName = "Friday";
        times.DayMonth = 30;
        times.NumMonth = 7;
        times.Month = "blank";
        times.Season = "blank";
        times.Years = 0;
        incrementSeason();
        incrementMonth();
    }
    // Important to keep Day, Weekday, DayMonth, and NumMonth inits so the array doesn't start at 0.
    // Some of the time tracking functions could be unified, like month and season, but for long term sanitation reasons
    // I split them apart so that we can throw things into them without worrying about accidentally triggering the wrong
    // thing.

    void loadTimeData()
    {
        // Eventually will load the time data from the player's save into memory.
    }

    public void incrementMinute()
    {

        if (times.Minutes + times.TimeFactor <= 1439)
        {
            times.Minutes += times.TimeFactor;
        }
        else
        {
            times.Minutes -= 1440;
            times.Minutes += times.TimeFactor;
            incrementDay();
        }
        Debug.Log("time min: " + times.Minutes + " " + times.TimeOfDay + " Temp: " + WeatherManager.instance.weather.CurrTemp + " Sun: " + WeatherManager.instance.weather.Sun + " Season: " + times.Season);
        Debug.Log(times.WeekdayName + " the " + times.DayMonth + " of " + times.Month);
    }

    void incrementDay()
    {
        string[] weekDays = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        Debug.Log("Day Increment");
        times.Day += 1;
        times.DayMonth += 1;
        times.Weekday += 1;
        WeatherManager.instance.PerDayWeather(); // Updates slow weather data.

        if (times.Weekday >= 7)
            {
            times.Weekday -= -7; 
            }
        times.WeekdayName = weekDays[times.Weekday];

        if(times.DayMonth >= 30)
        {
            incrementMonth();
        }
    }

    void incrementMonth()
    {
        // Counts up and resets counts, checks to see if the season changed.
        string[] months = new string[] { "blank", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul","Aug","Sep","Oct","Nov","Dec" };
        if (times.DayMonth > 30)
        {
            Debug.Log("incrementing month");
            times.DayMonth -= 30;
            times.NumMonth += 1;
        }

        times.Month = months[times.NumMonth];

        if (times.NumMonth >= 12)
        {
            times.NumMonth -= 12;
            incrementYear();
        }

        incrementSeason();

    }

    public void incrementSeason()
    {
        
        // Updates Season by checking the number of the month
        if (times.NumMonth >= 3 && times.NumMonth <=5) { times.Season = "Spring"; }
        else if (times.NumMonth >= 6 && times.NumMonth <= 8) { times.Season = "Summer"; }
        else if (times.NumMonth >= 9 && times.NumMonth <= 11) { times.Season = "Fall"; }
        else if (times.NumMonth >= 12 || times.NumMonth <= 2) { times.Season = "Winter"; }

        // Changes base temperature depending on the season. Will eventually need to reference biome data
        // rather than just being static, but for now this is fine.
        if (times.Season == "Spring") { WeatherManager.instance.weather.SeasonTemp = 35f; }
        else if (times.Season == "Summer") { WeatherManager.instance.weather.SeasonTemp = 47f; }
        else if (times.Season == "Fall") { WeatherManager.instance.weather.SeasonTemp = 27f; }
        else if (times.Season == "Winter") { WeatherManager.instance.weather.SeasonTemp = 10f; }
    }

    void incrementYear()
    {
        times.Years += 1;
    }
}