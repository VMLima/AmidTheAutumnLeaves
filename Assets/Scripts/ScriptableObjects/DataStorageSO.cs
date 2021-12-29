using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDataStore", menuName = "Scriptable Object/DataStorage")]
public class DataStorageSO : CommonBaseSO
{
    //THIS IS A SCRIPTABLE OBJECT THAT HOLDS A REFERENCE TO ALL OTHER SCRIPTABLE OBJECTS.
    //  unity kinda needs this or else it has trouble finding unreferenced scriptable objects.
    //  it also holds a dictionary of <string type, List of objects of type> for easy scriptable object searching.
    public List<CommonBaseSO> dataStore;
    [SerializeField]
    public Dictionary<string, List<CommonBaseSO>> typeStorage;

    public void clear()
    {
        //dataStore.Clear();
        dataStore = new List<CommonBaseSO>();
        typeStorage = new Dictionary<string, List<CommonBaseSO>>();
    }

    public void add(CommonBaseSO toAdd)
    {
        //Debug.Log("add:" + toAdd.GetType().ToString());
        string dataType = toAdd.GetType().ToString();
        if (!typeStorage.ContainsKey(dataType)) typeStorage.Add(dataType, new List<CommonBaseSO>());
        if (!typeStorage[dataType].Contains(toAdd)) typeStorage[dataType].Add(toAdd);

        //if it already exists, skip.
        for (int i = 0; i < dataStore.Count; i++)
        {
            if (dataStore[i] == toAdd) return;
        }
        dataStore.Add(toAdd);
        
    }

    public void resetAll()
    {
        foreach (KeyValuePair<string, List<CommonBaseSO>> ele in typeStorage)
        {
            if(ele.Value != null && ele.Value.Count > 0)
            {
                foreach(CommonBaseSO SO in ele.Value)
                {
                    SO.reset();
                }
            }
        }
    }

    public T get<T>(string _name) where T : CommonBaseSO
    {
        string stringType = typeof(T).ToString();
        //Debug.Log("DataStorage:Get:trying for:" + stringType + ":" + _name);
        if(typeStorage.ContainsKey(stringType))
        {
            foreach (CommonBaseSO t in typeStorage[stringType])
            {
                if (t.name == _name) return (T)t;
            }
            Debug.LogError("DataStorage:Get: Could not get name-value in dictionary:" + stringType + ":" + _name);
        }
        else
        {
            Debug.LogError("DataStorage:Get: Could not get type-key in dictionary:" + stringType + ":" + _name);
        }
        
        return null;
    }
}