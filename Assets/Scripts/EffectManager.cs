using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    //hold health, stamina, temperature.
    //add status effects that end based on conditions.

    // Start is called before the first frame update


    //get a list of status effect scriptableObjects
    //SO_StatusEffect[] statusArray;
    GameObject[] effectArray;
    public GameObject statusPanel;

    List<EffectScript> activeEffects;

    public static EffectManager instance;

    private float timer;

    public int health;
    //add them to a gameObject as they become active.
    //remove them from a gameObject as they become inactive.

    public GameObject getEffect(string effectName)
    {
        foreach(GameObject effect in effectArray)
        {
            if (effect.name == effectName)
            {
                return effect;
            }
        }
        return null;
    }

    //loose gameObjects... I gotta handle this better.  Stop creating and deleting in here.
    //create all at start, then just call stuff on their effect scripts?
    //or just have 1 central object with effects?  I like that.
    //prefab 'playerEffects' and just load it full of effectScripts.
    //  can have UI objects attached to the effectScripts if we want UI.
    //scriptable object items will have to have their game object effects instantiated and stored at start.
    //  
    public void removeObject(EffectScript effectScript)
    {
        foreach (Transform child in statusPanel.transform)
        {
            //child is your child transform
            if (child.name == effectScript.name)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    //start an effect by passing a script.
    public void startEffect(EffectScript effectScript)
    {
        effectScript.startEffect();
        activeEffects.Add(effectScript);
    }

    public void startEffect(GameObject effectObject)
    {
        Debug.Log("Start effect" + effectObject.name);
        //first check if status effect is already on.
        //if so simply call effectStackOverride();
        foreach (Transform child in statusPanel.transform)
        {
            //child is your child transform
            if (child.name == effectObject.name)
            {
                child.GetComponent<EffectScript>().effectStackOverride();
                if(child.GetComponent<EffectScript>().stackEffects == false) return;
            }
        }

        //otherwise... get the status scriptable object... create its UI object and put it in the UI... start up the script.
        GameObject effectInstance = (GameObject)Instantiate(effectObject, transform);

        EffectScript effectScript = effectInstance.GetComponent<EffectScript>();

        if (effectScript != null)
        {
            effectInstance.transform.SetParent(statusPanel.transform);
            effectScript.name = effectInstance.name;
            effectObject.name = effectInstance.name;
            startEffect(effectScript);
        }
    }

    //search for the effect in a compiled list of loose effects.
    //gonna be 'attached to player object'
    public void startEffect(string statusName)
    {
        GameObject effect = getEffect(statusName);
        startEffect(effect);
    }


    public void endEffect(GameObject effect, int toRemove = 0)
    {
        endEffect(effect.name, toRemove);
    }

    public void endEffect(string effectName, int toRemove = 0)
    {
        //get children of statusPanel, get 
        //foreach child in pane...
        //if child.name == statusName
        //remove it and delete.
        if (activeEffects.Count > 0)
        {
            foreach (EffectScript script in activeEffects)
            {
                if(script.name == effectName)
                {
                    script.endEffect(toRemove);
                }
            }
        }
    }

    void Awake()
    {
        instance = this;
        //statusArray = Utils.GetSriptableStatusEffects<SO_StatusEffect>();
        effectArray = Utils.GetAllGameObjects(Utils.effectLocation);
        activeEffects = new List<EffectScript>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //every half second call .tick(float deltaTime); to all effects in list.
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            timer = 0;
            //Debug.Log("tick"); 
            for(int i = (activeEffects.Count - 1); i>= 0; i--)
            {
                if (!activeEffects[i].tick(0.5f))
                {
                    removeObject(activeEffects[i]);
                    activeEffects.RemoveAt(i);
                    
                }
            }
        }
    }
}