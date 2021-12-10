using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvents : MonoBehaviour
{
    // adds and removes items based on a string that corresponds
    // to the name of scriptable objects in a specific directory.
    // exact functionality is determined by variables in the scriptable objects
    void Start()
    {
        Debug.Log("Adding a sword");
        ItemManager.instance.addItem("Sword");
        ItemManager.instance.allItemsDebugLog();

        Debug.Log("Adding a sword");
        ItemManager.instance.addItem("Sword");
        ItemManager.instance.allItemsDebugLog();

        Debug.Log("Removing a sword");
        ItemManager.instance.removeItem("Sword");
        ItemManager.instance.allItemsDebugLog();

        Debug.Log("Adding a gold");
        ItemManager.instance.addItem("Gold");
        ItemManager.instance.allItemsDebugLog();

        Debug.Log("Adding 20 gold. Max gold is 10.");
        ItemManager.instance.addItem("Gold", 20);
        ItemManager.instance.allItemsDebugLog();

        Debug.Log("Removing a gold");
        ItemManager.instance.removeItem("Gold");
        ItemManager.instance.allItemsDebugLog();

        Debug.Log("Removing 15 gold");
        ItemManager.instance.removeItem("Gold", 15);
        ItemManager.instance.allItemsDebugLog();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
