using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

    class SOComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return (new CaseInsensitiveComparer()).Compare(((CommonBaseSO)x).GetType(), ((CommonBaseSO)y).GetType());
        }
    }

    public void compileReferences()
    {
        //go through compiled references, remove all nulls
        for(int i = references.Count-1;i>=0;i--)
        {
            if (references[i] == null) references.RemoveAt(i);
        }

        //build up dictionary for quick game searching.
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
            if (toAdd)
            {
                references.Add(unl);
                //sorts the list by type.  Looks all nice and pretty.
                references.Sort((x, y) => x.GetType().ToString().CompareTo(y.GetType().ToString()));
                EditorUtility.SetDirty(this);
            }
            
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