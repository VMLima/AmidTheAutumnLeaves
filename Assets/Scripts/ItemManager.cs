using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Exists as singleton manager for items.
///     holds a list of items, 
///     
///     PERSONAL NOTES...
///         items... added, incremented, etc.  It will be up to... either inspector tags or function calls to actually activate the effects.
///         inspector tags: onGainQuantityActivate.  Would be like a resource in a way.
///         function calls: onEquip, onUse.
/// </summary>

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    //private List<Item> itemList;
    private SO_Item[] itemArray;

    void Awake()
    {
        instance = this;
        itemArray = Utils.GetAllScriptableObjects<SO_Item>();
        setupItems();
    }

    void setupItems()
    {
        foreach(SO_Item item in itemArray)
        {
            item.reset();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private SO_Item getScriptableObject(string prefabName)
    {
        foreach (SO_Item soItem in itemArray)
        {
            if (soItem.nameTag == prefabName)
            {
                //found the prefab.
                return soItem;
            }
        }
        return null;
    }

    public SO_Item getItem(string itemName)
    {
        foreach(SO_Item item in itemArray)
        {
            if(item.nameTag == itemName)
            {
                return item;
            }
        }
        return null;
    }

    public SO_Item getItem(SO_Item item)
    {
        return getItem(item.nameTag);
    }

    private void addItemToUI(SO_Item soItem)
    {
        //UI adding removing stuff
    }

    private void removeItemToUI(SO_Item soItem)
    {
        //UI adding removing stuff
    }

    public void allItemsDebugLog()
    {
        int i = 1;
        Debug.Log("==currentInventory===");
        foreach (SO_Item item in itemArray)
        {
            Debug.Log("slot:" + i + "=" + item.nameTag + " amount:" + item.getAmount());
            i++;
        }
        Debug.Log("===================");
    }

    public void addItem(string itemName, int amount = 1)
    {
        //get prefab from list
        //Debug.Log("itemManager:additem: " + itemName);
        SO_Item scriptableItem = getScriptableObject(itemName);
        
        if (scriptableItem)
        {
            //Debug.Log("itemManager:additem: getScriptableObject=found");
            // find out if item already exists in inventory

            SO_Item item = getItem(itemName);
            if (item != null)
            {
                //ITEM IS IN INVENTORY ALREADY
                item.addAmount(amount);
            }
            else
            {
                //ITEM IS NOT IN INVENTORY
                addItemToUI(item);
                item.setAmount(amount);
                Debug.Log("itemManager:additem: adding " + amount + " to inventory");
            }
        }
        else
        {
            Debug.LogError("itemManager:additem: Could not find prefab: " + itemName);
        }
        //Debug.Log("itemManager:additem: FINISHED WITH " + itemName);
    }

    public void removeItem(string itemName, int amount = 1)
    {
        //Debug.Log("itemManager:removeItem: " + itemName);
        SO_Item item = getItem(itemName);
        if (item != null)
        {
            //Debug.Log("itemManager:removeItem: found item");
            if((item.addAmount((-1*amount)) <= 0) && (item.DeleteWhenEmpty))
            {
                //all out of item and delete when empty.
                removeItemToUI(item);
            }
        }
        else
        {
            Debug.LogError("itemManager:removeItem: Could not find item: " + itemName);
        }
        //Debug.Log("itemManager:removeItem: FINISHED WITH " + itemName);
    }
}
