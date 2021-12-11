using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    private List<Item> itemList;
    private SO_Item[] soItemArray;

    void Awake()
    {
        instance = this;
        itemList = new List<Item>();
        soItemArray = Utils.GetSriptableItems<SO_Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private SO_Item getScriptableObject(string prefabName)
    {
        foreach (SO_Item soItem in soItemArray)
        {
            if (soItem.nameTag == prefabName)
            {
                //found the prefab.
                return soItem;
            }
        }
        return null;
    }

    public Item getItem(string itemName)
    {
        foreach(Item item in itemList)
        {
            if(item.soItem.nameTag == itemName)
            {
                return item;
            }
        }
        return null;
    }

    public Item getItem(SO_Item item)
    {
        return getItem(item.nameTag);
    }

    private void addNewItem(SO_Item soItem, int amount = 1)
    {
        //given a prefab instanciate and add a new item.
        Item newItem = new Item(soItem);
        itemList.Add(newItem);

        //any other stuff important to instantiating, like adding it to its UI, etc.
    }

    private void removeItemFromList(Item item)
    {
        itemList.Remove(item);
        //any other stuff important to removing, like removing from UI, etc.
    }

    public void allItemsDebugLog()
    {
        int i = 1;
        Debug.Log("==currentInventory===");
        foreach (Item item in itemList)
        {
            Debug.Log("slot:" + i + "=" + item.soItem.nameTag + " amount:" + item.getAmount());
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

            Item item = getItem(itemName);
            if (item != null)
            {
                //ITEM IS IN INVENTORY ALREADY
                item.addAmount(amount);
            }
            else
            {
                //ITEM IS NOT IN INVENTORY
                Debug.Log("itemManager:additem: adding " + amount + " to inventory");
                addNewItem(scriptableItem, amount);
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
        Item item = getItem(itemName);
        if (item != null)
        {
            //Debug.Log("itemManager:removeItem: found item");
            if((item.addAmount((-1*amount)) <= 0) && (item.DeleteWhenEmpty))
            {
                //all out of item and delete when empty.
                removeItemFromList(item);
            }
        }
        else
        {
            Debug.LogError("itemManager:removeItem: Could not find item: " + itemName);
        }
        //Debug.Log("itemManager:removeItem: FINISHED WITH " + itemName);
    }
}
