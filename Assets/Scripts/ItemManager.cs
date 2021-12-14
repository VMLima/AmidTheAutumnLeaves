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
    private ItemSO[] itemArray;

    void Awake()
    {
        instance = this;
        itemArray = Utils.GetAllScriptableObjects<ItemSO>();
        setupItems();
    }

    void setupItems()
    {
        foreach(ItemSO item in itemArray)
        {
            item.reset();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private ItemSO getScriptableObject(string prefabName)
    {
        foreach (ItemSO soItem in itemArray)
        {
            if (soItem.nameTag == prefabName)
            {
                //found the prefab.
                return soItem;
            }
        }
        return null;
    }

    public ItemSO getItem(string itemName)
    {
        foreach(ItemSO item in itemArray)
        {
            if(item.nameTag == itemName)
            {
                return item;
            }
        }
        return null;
    }

    public ItemSO getItem(ItemSO item)
    {
        return getItem(item.nameTag);
    }

    private void addItemToUI(ItemSO soItem)
    {
        //UI adding removing stuff
    }

    private void removeItemToUI(ItemSO soItem)
    {
        //UI adding removing stuff
    }

    public void allItemsDebugLog()
    {
        int i = 1;
        Debug.Log("==currentInventory===");
        foreach (ItemSO item in itemArray)
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
        ItemSO scriptableItem = getScriptableObject(itemName);
        
        if (scriptableItem)
        {
            //Debug.Log("itemManager:additem: getScriptableObject=found");
            // find out if item already exists in inventory

            ItemSO item = getItem(itemName);
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
        ItemSO item = getItem(itemName);
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
