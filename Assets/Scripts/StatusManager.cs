using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    //hold health, stamina, temperature.
    //add status effects that end based on conditions.

    // Start is called before the first frame update


    //get a list of status effect scriptableObjects
    SO_StatusEffect[] statusArray;
    public GameObject statusPanel;

    public static StatusManager instance;

    public int health;
    //add them to a gameObject as they become active.
    //remove them from a gameObject as they become inactive.

    public SO_StatusEffect getStatus(string statusName)
    {
        foreach(SO_StatusEffect status in statusArray)
        {
            if (status.nameTag == statusName)
            {
                return status;
            }
        }
        return null;
    }

    public void addStatus(string statusName)
    {
        //first check if status effect is already on.
        //if so simply call effectStackOverride();
        foreach (Transform child in statusPanel.transform)
        {
            //child is your child transform
            if (child.name == statusName)
            {
                child.GetComponent<StatusScript>().effectStackOverride();
                return;
            }
        }

        //otherwise... get the status scriptable object... create its UI object and put it in the UI... start up the script.
        SO_StatusEffect status = getStatus(statusName);
        GameObject statusObject = (GameObject)Instantiate(status.UIObject, transform);

        statusObject.name = status.name;    //just to confirm I can refind it.
        StatusScript statusScript = statusObject.GetComponent<StatusScript>();
        statusObject.transform.SetParent(statusPanel.transform);

        if (statusScript != null)
        {
            statusScript.startCondition();
        }
    }

    public void removeStatus(string statusName, bool removeAll = true)
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
    void Start()
    {
        instance = this;
        statusArray = Utils.GetSriptableStatusEffects<SO_StatusEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
