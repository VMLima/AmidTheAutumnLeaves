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

    public static EffectManager instance;

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

    public void startEffect(GameObject effect)
    {
        Debug.Log("Start effect" + effect.name);
        //first check if status effect is already on.
        //if so simply call effectStackOverride();
        foreach (Transform child in statusPanel.transform)
        {
            //child is your child transform
            if (child.name == effect.name)
            {
                child.GetComponent<EffectScript>().effectStackOverride();
                if(child.GetComponent<EffectScript>().stackEffects == false) return;
            }
        }

        //otherwise... get the status scriptable object... create its UI object and put it in the UI... start up the script.
        GameObject effectObject = (GameObject)Instantiate(effect, transform);

        effectObject.name = effect.name;    //just to confirm I can refind it.
        EffectScript statusScript = effectObject.GetComponent<EffectScript>();
        effectObject.transform.SetParent(statusPanel.transform);

        if (statusScript != null)
        {
            statusScript.startCondition();
        }
    }

    public void startEffect(string statusName)
    {
        GameObject effect = getEffect(statusName);
        startEffect(effect);
    }

    public void endEffect(GameObject effect, bool removeAll = true)
    {
        endEffect(effect.name, removeAll);
    }

    public void endEffect(string statusName, bool removeAll = true)
    {
        //get children of statusPanel, get 
        //foreach child in pane...
        //if child.name == statusName
        //remove it and delete.
        foreach (Transform child in statusPanel.transform)
        {
            //child is your child transform
            if(child.name == statusName)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    void Awake()
    {
        instance = this;
        //statusArray = Utils.GetSriptableStatusEffects<SO_StatusEffect>();
        effectArray = Utils.GetAllGameObjects(Utils.effectLocation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
