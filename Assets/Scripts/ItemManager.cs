using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    private List<Item> itemList;
    private SO_Item[] prefabArray;

    void Awake()
    {
        instance = this;
        itemList = new List<Item>();
        prefabArray = Utils.GetSriptableItems<SO_Item>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private SO_Item getScriptableObject(string prefabName)
    {
        foreach (SO_Item soItem in prefabArray)
        {
            if (soItem.name == prefabName)
            {
                //found the prefab.
                return soItem;
            }
        }
        return null;
    }

    private Item getItem(string itemName)
    {
        foreach(Item item in itemList)
        {
            if(item.name == itemName)
            {
                return item;
            }
        }
        return null;
    }

    private void addNewItem(SO_Item soItem, int quantity = 1)
    {
        //given a prefab instanciate and add a new item.
        Item newItem = new Item(soItem);
        newItem.setQuantity(quantity);
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
            if(item.canStack) Debug.Log("slot:" + i + "=" + item.name + ":" + item.getQuantity());
            else Debug.Log("slot:" + i + "=" + item.name);
            i++;
        }
        Debug.Log("===================");
    }

    public void addItem(string itemName, int quantity = 1)
    {
        //get prefab from list
        //Debug.Log("itemManager:additem: " + itemName);
        SO_Item prefab = getScriptableObject(itemName);
        
        if (prefab)
        {
            //Debug.Log("itemManager:additem: getScriptableObject=found");
            // find out if item already exists in inventory

            Item item = getItem(itemName);
            if (item != null)
            {
                //ITEM IS IN INVENTORY ALREADY
                if (item.singleInstance)
                {
                    //Debug.Log("itemManager:additem: adding to quantity");
                    item.addQuantity(quantity);
                }
                else
                {
                    //Debug.Log("itemManager:additem: adding another to inventory");
                    addNewItem(prefab, quantity);
                }
            }
            else
            {
                //ITEM IS NOT IN INVENTORY
                //Debug.Log("itemManager:additem: adding to inventory");
                addNewItem(prefab, quantity);
            }
        }
        else
        {
            Debug.LogError("itemManager:additem: Could not find prefab: " + itemName);
        }
        //Debug.Log("itemManager:additem: FINISHED WITH " + itemName);
    }

    public void removeItem(string itemName, int quantity = 1)
    {
        //Debug.Log("itemManager:removeItem: " + itemName);
        Item item = getItem(itemName);
        if (item != null)
        {
            //Debug.Log("itemManager:removeItem: found item");
            if (!item.canStack)
            {
                //Debug.Log("itemManager:removeItem: can't stack, removing item");
                removeItemFromList(item);
            }
            else
            {
                if (item.addQuantity(-1 * quantity))
                {
                    //cam stack and there is quantity left
                    //Debug.Log("itemManager:removeItem: still quantity left");
                }
                else
                {
                    //can stack and there is no quantity left
                    //Debug.Log("itemManager:removeItem: out of quantity, removing");
                    removeItemFromList(item);
                }
            }
        }
        else
        {
            Debug.LogError("itemManager:removeItem: Could not find item: " + itemName);
        }
        //Debug.Log("itemManager:removeItem: FINISHED WITH " + itemName);
    }
}
