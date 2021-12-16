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

    public float health;
    //add them to a gameObject as they become active.
    //remove them from a gameObject as they become inactive.

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
            else if ((effectName != "") && (((EffectScript)comp).nameTag == effectName)) effects.Add(((EffectScript)comp));
        }
        
        if (effects.Count == 0)
        {
            Debug.LogError("EffectManager: getEffect: could not find effects in:" + effectObject.name + ": with criteria:" + effectName);
        }
        return effects.ToArray();
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
    public void startEffect(EffectScript[] effectScripts, int stacks = 1)
    {
        foreach(EffectScript script in effectScripts)
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
                Debug.Log("startEffect: already have effect");
                return;
            }
        }
        activeEffects.Add(effectScript);
    }

    //search for the effect in a list of loose effects.
    //gonna be attached to player object at some point.
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



    //search through all active effects, pause all by name or by type.
    public void pauseActiveEffect(string effectName = "", string effectType = "")
    {
        foreach(EffectScript e in activeEffects)
        {
            if (effectName == "")           e.pauseEffect(effectType);
            else if (effectName == e.nameTag) e.pauseEffect(effectType);
        }
    }
    //search through all active effects, unpause all by name or by type.
    public void unPauseActiveEffect(string effectName = "", string effectType = "")
    {
        foreach (EffectScript e in activeEffects)
        {
            if (effectName == "") e.unPauseEffect(effectType);
            else if (effectName == e.nameTag) e.unPauseEffect(effectType);
        }
    }

    

    //sending a game object.
    //get all scripts in it, possibly matching a name
    //remove toRemove stacks from them (if toRemove is 0, just end the effect)
    public void endEffect(GameObject effectObject, string _name = "", int toRemove = 0)
    {
        foreach (EffectScript effectScript in getEffect(effectObject))
        {
            if (_name != "")
            {
                if (effectScript.nameTag == _name) endEffect(effectScript, toRemove);
            }
            else endEffect(effectScript, toRemove);
        }
    }

    //sending a string, get the script.
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
                if (script.nameTag == effectName)
                {
                    endEffect(script, toRemove);
                }
            }
        }
    }
    //sending a script, end it.
    public void endEffect(EffectScript effectScript, int toRemove = 0)
    {
        effectScript.endEffect(toRemove);
    }

    private void OnDestroy()
    {
        //Any instantiated objects need to be destroyed or else weird lingering data stuff can happen.
        for(int i = (effectArray.Length - 1); i >= 0; i--)
        {
            GameObject.Destroy(effectArray[i]);
        }
    }

    void Awake()
    {
        instance = this;
        //statusArray = Utils.GetSriptableStatusEffects<SO_StatusEffect>();
        effectArray = Utils.GetAllGameObjects(Utils.effectLocation);
        for(int i = 0; i<effectArray.Length;i++)
        {
            string nameTag = effectArray[i].name;
            //I am creating a clone of the base prefab.  Setting the name to the actual though (since that matters for effects)
            //I must create a clone or else it edits the prefab which persists even out of game.
            effectArray[i] = (GameObject)Instantiate(effectArray[i], transform);
            effectArray[i].name = nameTag;

            //gets all components of the prefab that are EffectScripts.
            //I want to carry the name above in them so I can find and remove them from the activeEffects array.
            Component[] components = effectArray[i].GetComponents(typeof(EffectScript));
            foreach(Component effect in components)
            {
                //((EffectScript)effect).nameTag = effectArray[i].name;
                //Debug.Log("awake: for loop name:" + effectArray[i].name);
            }
            //Debug.Log("awake: effect name:" + effectArray[i].name);
        }
        activeEffects = new List<EffectScript>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //every half second call .tick(float deltaTime); to all effects in list.
        timer += Time.deltaTime;
        if (timer >= 1.0f)
        {
            timer = 0;
            //Debug.Log("tick"); 
            
            for (int i = (activeEffects.Count - 1); i >= 0; i--)
            {
                if (!activeEffects[i].tick(1.0f))
                {
                    Debug.Log("EffectManager:Update: removing effect: " + activeEffects[i].nameTag);
                    removeObject(activeEffects[i]);
                    activeEffects.RemoveAt(i);
                }
                else
                {
                    Debug.Log("EffectManager:Update: continueing effect: " + activeEffects[i].nameTag);
                }
            }
            //should be last, since also cleans up float rounding funkiness in its values.
            Player.instance.everySecond();
        }
    }
}