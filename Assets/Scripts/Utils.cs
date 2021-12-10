using UnityEngine;

public static class Utils
{
    //get all scriptable objects within folder "location"
    private static string itemLocation = "ScriptableObjects/Items";
    private static string skillLocation = "ScriptableObjects/Skills";
    private static string nodeLocation = "ScriptableObjects/Nodes";
    private static string featureLocation = "ScriptableObjects/Features";

    public static T[] GetScriptableSkills<T>() where T : SO_Skill
    {
        return Resources.LoadAll<T>(skillLocation);
    }

    public static T[] GetSriptableItems<T>() where T : SO_Item
    {
        return Resources.LoadAll<T>(itemLocation);
    }

    public static T[] GetSriptableNodes<T>() where T : SO_Node
    {
        return Resources.LoadAll<T>(nodeLocation);
    }

    public static T[] GetSriptableFeatures<T>() where T : SO_Feature
    {
        return Resources.LoadAll<T>(featureLocation);
    }

}
