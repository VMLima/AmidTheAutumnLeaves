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


//static utility functions.
public static class Utils
{
    //effects are in GameObjects which every object is... so to just get the ones I want it needs to be only ones in a certain folder.
    public static string effectLocation = "ScriptableObjects/Effects";

    public static T[] GetAllScriptableObjects<T>() where T : UnlockableSO
    {
        return (T[])Resources.FindObjectsOfTypeAll(typeof(T));
    }

    public static GameObject[] GetAllGameObjects(string folder)
    {
        return Resources.LoadAll<GameObject>(folder);
    }

    public static bool checkUnlocked(LockInfoSO[] lockList)
    {
        if ((lockList != null) && (lockList.Length > 0))
        {
            foreach (LockInfoSO info in lockList)
            {
                //get its type
                //find the actual created object
                if ((info.unlocker != null) && (info.unlocker.getUnlockValue() < info.amount)) return false;
            }
            //all requirements are a success!!
        }
       return true;
    }

}
