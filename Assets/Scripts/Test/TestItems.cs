using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItems : MonoBehaviour
{
    // adds and removes items based on a string that corresponds
    // to the name of scriptable objects in a specific directory.
    // exact functionality is determined by variables in the scriptable objects
    void Start()
    {
        Debug.Log("Adding a sword");
        IncManager.instance.Add<ItemSO>("Sword");

        Debug.Log("Adding a sword");
        IncManager.instance.Add<ItemSO>("Sword");

        Debug.Log("Removing a sword");
        IncManager.instance.Add<ItemSO>("Sword", -1);

        Debug.Log("Adding a gold");
        IncManager.instance.Add<ItemSO>("Gold");

        Debug.Log("Adding 20 gold. Max gold is 10.");
        IncManager.instance.Add<ItemSO>("Gold", 20);

        Debug.Log("Removing a gold");
        IncManager.instance.Add<ItemSO>("Gold", -1);

        Debug.Log("Removing 15 gold");
        IncManager.instance.Add<ItemSO>("Gold", -15);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
