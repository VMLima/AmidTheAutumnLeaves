using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// EFFECTS:
///     effects are scripts attached to effect prefab objects that are placed in a scriptable object's field.
///     most scriptable objects have an Effect Object field to drag a prefab into.
///     if the prefab has a prefab there and that prefab has scripts attached to it that inherit EffectScript...
///         doing .startEffect() on the item will end up sending adding all the effects there to this manager.
///         alternatively .startEffect("Sick", 2) will start only the effects named sick in the object, and give it a stack count of 2.
///         
///     this class is made to take EffectScripts, throw them in a list, and do .tick() on them every set amount of time.
///         it can add scripts to that.
///         it can remove scripts from that.
///         
///         the effects each have their own scripted functionality to do when .tick()'ed
///         
///     This is built more for performance than ease of use.
///         Invoke and Listneres have more overhead.  So it is a bit messier in having to hold a list of all active effects.
///         has to search it for effects when removing them.
///         has to deal with adding them to lists.
///         has to deal with instantiating/destroying objects.
///         


public class EffectManager : MonoBehaviour
{

    GameObject[] effectArray;
    public GameObject statusPanel;

    List<EffectScript> activeEffects;

    //ASH WEATHER VARIABLES.
    List<EffectScript> summerEffects;
    List<EffectScript> springEffects;
    List<EffectScript> winterEffects;
    List<EffectScript> autumnEffects;

    string season = "autumn";

    public static EffectManager instance;

    private float timer;

    public float health;

    public void pickRandomWeather()
    {
        //do stuff with weatherEffects
        //different seasons have different weathers.

    }

    void Awake()
    {
        instance = this;
        activeEffects = new List<EffectScript>();
        timer = 0;
        loadLooseEffects(); //effect scripts attached to a misc effect object attached to the EffectManager in the inspector.
    }

    void Update()
    {
        //every second call .tick(float deltaTime); to all effects in list.
        //keeps nice uniform UI updates.
        timer += Time.deltaTime;
        if (timer >= 1.0f)
        {
            timer = 0;
            for (int i = (activeEffects.Count - 1); i >= 0; i--)
            {
                if (!activeEffects[i].tick(1.0f))
                {
                    //Debug.Log("EffectManager:Update: removing effect: " + activeEffects[i].nameTag);
                    removeObject(activeEffects[i]);
                    activeEffects.RemoveAt(i);
                }
                else
                {
                    //Debug.Log("EffectManager:Update: continueing effect: " + activeEffects[i].nameTag);
                }
            }
            //should be last, cleans up float rounding funkiness in values.
            Player.instance.everySecond();
        }
    }

    //FUNCTIONS BELOW SHOULD NOT BE PERSONALLY ACCESSED.
    //Items/skills/etc call these functions.  Not normal game runtime stuff.
    //if you want an effect of an item to start.  use things like item.startEffect();
    //that will link into the functions below and add it to the effect tick loop.

    //      MISC NOTES.
    //start an effect....
    //  given a list of effectScripts
    //  given an effectScript
    //  given an effect string (so search loose effects)

    public void startEffect(EffectScript[] effectScripts, int stacks = 1)
    {
        foreach (EffectScript script in effectScripts)
        {
            startEffect(script, stacks);
        }
    }
    public void startEffect(EffectScript effectScript, int stacks = 1)
    {
        //Debug.Log("Start effect:" + effectScript.nameTag);
        effectScript.startEffect(stacks);
        foreach (EffectScript script in activeEffects)
        {
            if (script == effectScript)
            {
                //Debug.Log("startEffect: already have effect");
                return;
            }
        }
        activeEffects.Add(effectScript);
    }

    public void startEffect(string effectName, int stacks = 1)
    {
        EffectScript[] effects = getEffect(effectName);
        if (effects != null)
        {
            startEffect(effects, stacks);
        }
        else
        {
            Debug.LogError("EffectManageR:startEffect:Could not find effect by name: " + effectName);
        }
    }

    //get effect(s)
    //  from string... searches the loose effects.
    //  from an object...
    public EffectScript[] getEffect(string effectName = "")
    {
        List<EffectScript> effects = new List<EffectScript>();
        
        //change from searching for game object matching name to script tag matching name.
        foreach (GameObject effect in effectArray)
        {
            effects.AddRange(getEffect(effect, effectName));
        }
        if(effects.Count == 0)
        {
            Debug.LogError("EffectManager: getEffect: could not find effect named:" + effectName);
        }
        return effects.ToArray();
    }

    public EffectScript[] getEffect(GameObject effectObject, string effectName = "")
    {
        List<EffectScript> effects = new List<EffectScript>();

        //change from searching for game object matching name to script tag matching name.

        Component[] components = effectObject.GetComponents(typeof(EffectScript));
        foreach (Component comp in components)
        {
            if (effectName == "") effects.Add(((EffectScript)comp));
            else if ((effectName != "") && (((EffectScript)comp).name == effectName)) effects.Add(((EffectScript)comp));
        }
        
        if (effects.Count == 0)
        {
            Debug.LogError("EffectManager: getEffect: could not find effects in:" + effectObject.name + ": with criteria:" + effectName);
        }
        return effects.ToArray();
    }

    //ending an effect.
    // can send a...
    //  gameObject.  Will end all effects active on it, or matching a _name, or only remove toRemove severity.
    //  string.  Will search active effects for any matches.  Can only remove toRemove severity.
    //  EffectScript. Will end that one effect.  Or can only remove toRemove severity.
    public void endEffect(GameObject effectObject, string _name = "", int toRemove = 0)
    {
        foreach (EffectScript effectScript in getEffect(effectObject))
        {
            if (_name != "")
            {
                if (effectScript.name == _name) endEffect(effectScript, toRemove);
            }
            else endEffect(effectScript, toRemove);
        }
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
                if (script.name == effectName)
                {
                    endEffect(script, toRemove);
                }
            }
        }
    }

    public void endEffect(EffectScript effectScript, int toRemove = 0)
    {
        effectScript.endEffect(toRemove);
    }

    //when an effect ends...
    //  might have to change this.  I no longer destroy and create?
    //  or do i? since these are straight prefabs.
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

    //PAUSING - doesn't change the state of anything, simply ignores all .tick() commands.
    //UNPAUSING - causes the .tick() commands to function normally again.
    //      useful for equiping/unequiping. useful for pausing the game, or certain features for a bit.

    //search in the given effectObject for components that are effectScripts.
    //pause those effects.
    public void pauseEffect(GameObject effectObject, string effectName = "", string effectType = "")
    {
        EffectScript[] effects = getEffect(effectObject, effectName);
        if (effects != null)
        {
            foreach (EffectScript e in effects)
            {
                e.pauseEffect(effectType);
            }
        }
    }

    public void unPauseEffect(GameObject effectObject, string effectName = "", string effectType = "")
    {
        EffectScript[] effects = getEffect(effectObject, effectName);
        if (effects != null)
        {
            foreach (EffectScript e in effects)
            {
                e.unPauseEffect(effectType);
            }
        }
    }


    //as above but only if the effect is active at the moment.
    public void pauseActiveEffect(string effectName = "", string effectType = "")
    {
        foreach(EffectScript e in activeEffects)
        {
            if (effectName == "")           e.pauseEffect(effectType);
            else if (effectName == e.name) e.pauseEffect(effectType);
        }
    }

    public void unPauseActiveEffect(string effectName = "", string effectType = "")
    {
        foreach (EffectScript e in activeEffects)
        {
            if (effectName == "") e.unPauseEffect(effectType);
            else if (effectName == e.name) e.unPauseEffect(effectType);
        }
    }

    private void OnDestroy()
    {
        //Any instantiated objects need to be destroyed or else weird lingering data stuff can happen.
        destroyLooseEffects();
    }

    void destroyLooseEffects()
    {
        for (int i = (effectArray.Length - 1); i >= 0; i--)
        {
            GameObject.Destroy(effectArray[i]);
        }
    }

    //atm have misc effects held on a game object attached to EffectManager.
    //  for when an effect isn't tied to an item/resource/skill/button, it can be found here.
    void loadLooseEffects()
    {
        effectArray = Utils.GetAllGameObjects(Utils.effectLocation);
        for (int i = 0; i < effectArray.Length; i++)
        {
            string nameTag = effectArray[i].name;
            //I am creating a clone of the base prefab.  Setting the name to the actual though (since that matters for effects)
            //I must create a clone or else it edits the prefab which persists even out of game.
            effectArray[i] = (GameObject)Instantiate(effectArray[i], transform);
            effectArray[i].name = nameTag;

            //gets all components of the prefab that are EffectScripts.
            //I want to carry the name above in them so I can find and remove them from the activeEffects array.
            Component[] components = effectArray[i].GetComponents(typeof(EffectScript));
            foreach (Component effect in components)
            {
                //((EffectScript)effect).nameTag = effectArray[i].name;
                //Debug.Log("awake: for loop name:" + effectArray[i].name);
            }
            //Debug.Log("awake: effect name:" + effectArray[i].name);
        }
    }
}