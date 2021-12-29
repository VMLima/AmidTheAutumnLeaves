using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDataStore", menuName = "Scriptable Object/DataStorage")]
public class DataStorageSO : CommonBaseSO
{
    public List<CommonBaseSO> dataStore;

    public void clear()
    {
        dataStore.Clear();
    }

    public void add(CommonBaseSO toAdd)
    {
        //if it already exists, skip.
        for (int i = 0; i < dataStore.Count; i++)
        {
            if (dataStore[i] == toAdd) return;
        }
        dataStore.Add(toAdd);
    }

    public CommonBaseSO[] get()
    {
        return dataStore.ToArray();
    }
}