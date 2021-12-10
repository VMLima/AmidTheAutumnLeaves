using UnityEngine;

public static class Utils
{
    //get all scriptable objects within folder "location"
    private static string itemLocation = "ScriptableObjects/Items";
    private static string skillLocation = "ScriptableObjects/Skills";

    public static T[] GetScriptableSkills<T>() where T : SO_Skill
    {
        return Resources.LoadAll<T>(skillLocation);
    }

    public static T[] GetSriptableItems<T>() where T : SO_Item
    {
        return Resources.LoadAll<T>(itemLocation);
    }

}
