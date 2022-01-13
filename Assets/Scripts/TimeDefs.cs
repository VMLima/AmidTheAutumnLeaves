using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Times
{
    private int minutes; // Minute of the day from 1 to 1440
    private float aMorPM; // -1 AM for 1 PM
    private string timeOfDay; // Var to hold the current time of day based on weather.Sun
    private int day; // day of the year from 1 to 360
    private int timeFactor; // the number of minutes that pass every second
    private int weekday; // the day name, mon/tues/weds etc.
    private string weekdayName; 
    private int dayMonth; // 1 to 30
    private int numMonth; // 1 to 12
    private string month; // the name of the current month
    private string season; // spring, summer, fall, winter
    private int years; // number of years that have passed

    public int Minutes
    {
        get { return minutes; }
        set { minutes = value; }
    }
    public float AMorPM
    {
        get { return aMorPM; }
        set { aMorPM = value; }
    }

    public string TimeOfDay
    {
        get { return timeOfDay; }
        set { timeOfDay = value; }
    }

    public int Day // Day of the year from 1 to 360
    {
        get { return day; }
        set { day = value; }
    }

    public int TimeFactor // How many minutes pass per second while sleeping
    {
        get { return timeFactor; }
        set { timeFactor = value; }
    }

    public int Weekday
    {
        get { return weekday; }
        set { weekday = value; }
    }

    public string WeekdayName
    {
        get { return weekdayName; }
        set { weekdayName = value; }
    }

    public int DayMonth
    {
        get { return dayMonth; }
        set { dayMonth = value; }
    }

    public int NumMonth
    {
        get { return numMonth; }
        set { numMonth = value; }
    }

    public string Month
    {
        get { return month; }
        set { month = value; }
    }

    public string Season
    {
        get { return season; }
        set { season = value; }
    }

    public int Years
    {
        get { return years; }
        set { years = value; }
    }
}