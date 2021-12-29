using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//CREATING A READ ONLY INSPECTOR ELEMENT
// I copied and pasted this from elsewhere
public class ReadOnlyAttribute : PropertyAttribute
{

}

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                            GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position,
                               SerializedProperty property,
                               GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}

enum direction { Item = 1, Skill = 2, Resource = 3, Craftable = 4, Other = 5 };


//static utility functions.
public static class Utils
{
    //effects are in GameObjects which every object is... so to just get the ones I want it needs to be only ones in a certain folder.
    public static string effectLocation = "ScriptableObjects/Effects";
    public static string prefabLocation = "Prefabs";

    //wanna get the object and add to it....

    public static T[] GetAllScriptableObjects<T>() where T : ScriptableObject
    {
        return (T[])Resources.FindObjectsOfTypeAll(typeof(T));
    }

    public static GameObject[] GetAllGameObjects(string folder)
    {
        return Resources.LoadAll<GameObject>(folder);
    }

    public static GameObject GetPrefab(string _name)
    {
        GameObject[] objects = Resources.LoadAll<GameObject>(prefabLocation);
        foreach(GameObject obj in objects)
        {
            if (obj.name == _name) return obj;
        }
        Debug.LogError("Utils.GetPrefab:could not find prefab:" + _name);
        return null;
    }

    public static T GetScriptableObjects<T>(string objName) where T : ScriptableObject
    {
        T[] items = (T[])Resources.FindObjectsOfTypeAll(typeof(T));
        List<T> matchingItems = new List<T>();
        foreach(T i in items)
        {
            if (i.name == objName) matchingItems.Add(i);
        }

        if (matchingItems.Count == 1) return matchingItems[0];
        else if (matchingItems.Count > 1)
        {
            Debug.LogError("Utils.GetScriptableObjects: found multiple objects of name:" + objName);
            return matchingItems[0];
        }
        else
        {
            Debug.LogError("Utils.GetScriptableObjects: could not find object of name:" + objName);
        }
        return null;
    }

    public static bool checkUnlocked(LockInfoSO[] lockList, float times = 1)
    {
        if ((lockList != null) && (lockList.Length > 0))
        {
            foreach (LockInfoSO info in lockList)
            {
                //get its type
                //find the actual created object
                if ((info.unlocker != null) && (info.unlocker.getUnlockValue() < (info.amount * times))) return false;
            }
            //all requirements are a success!!
        }
       return true;
    }

}
