using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    //ASH WEATHER VARIABLES.
    //
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

    void Awake()
    {
        instance = this;
        
        loadSeasonData();
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
