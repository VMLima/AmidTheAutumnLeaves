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
    public List<CommonBaseSO> references;
    [SerializeField]
    public Dictionary<string, List<CommonBaseSO>> dataDictionary;

    public void clear()
    {
        //dataStore.Clear();
        //references = new List<CommonBaseSO>();
        
    }

    public void compileReferences()
    {
        //Debug.Log("add:" + toAdd.GetType().ToString());
        dataDictionary = new Dictionary<string, List<CommonBaseSO>>();
        foreach (CommonBaseSO unl in Utils.GetAllScriptableObjects<CommonBaseSO>())
        {
            //Debug.Log("name:" + unl.name);
            string dataType = unl.GetType().ToString();
            if (!dataDictionary.ContainsKey(dataType)) dataDictionary.Add(dataType, new List<CommonBaseSO>());
            if (!dataDictionary[dataType].Contains(unl)) dataDictionary[dataType].Add(unl);

            //if it already exists, skip.
            bool toAdd = true;
            for (int i = 0; i < references.Count; i++)
            {
                if (references[i] == unl)
                {
                    toAdd = false;
                    break;
                }
            }
            if (toAdd) references.Add(unl);
            
        }
    }

    public void resetAllScriptableObjects()
    {
        foreach (KeyValuePair<string, List<CommonBaseSO>> ele in dataDictionary)
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
        if(dataDictionary.ContainsKey(stringType))
        {
            foreach (CommonBaseSO t in dataDictionary[stringType])
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