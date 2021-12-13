using UnityEngine;

public static class Utils
{
    //get all scriptable objects within folder "location"
    private static string itemLocation = "ScriptableObjects/Items";
    private static string skillLocation = "ScriptableObjects/Skills";
    private static string nodeLocation = "ScriptableObjects/Nodes";
    private static string featureLocation = "ScriptableObjects/Features";
    private static string statusEffectLocation = "ScriptableObjects/Effects";

    public static T[] GetAllSkills<T>() where T : SO_Skill
    {
        return Resources.LoadAll<T>(skillLocation);
    }

    public static T[] GetAllItems<T>() where T : SO_Item
    {
        return Resources.LoadAll<T>(itemLocation);
    }

    public static T[] GetAllNodes<T>() where T : SO_Node
    {
        return Resources.LoadAll<T>(nodeLocation);
    }

    public static T[] GetAllFeatures<T>() where T : SO_Feature
    {
        return Resources.LoadAll<T>(featureLocation);
    }

    public static GameObject[] GetAllEffects()
    {
        return Resources.LoadAll<GameObject>(statusEffectLocation);
    }

    public static bool checkUnlocked(LockInfo[] lockList)
    {
        if ((lockList != null) && (lockList.Length > 0))
        {
            foreach (LockInfo info in lockList)
            {
                //get its type
                //find the actual created object
                if (info.soBasic.GetType() == typeof(SO_Skill))
                {
                    SO_Skill temp = SkillManager.instance.getSkill((SO_Skill)info.soBasic);
                    if ((temp != null) && (temp.getLevel() >= info.amount))
                    {
                        //REQUIREMENT SUCCESS!!
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (info.soBasic.GetType() == typeof(SO_Resource))
                {
                    //incTemp = ResourceManager.instance.getResource((SO_Resource)info.soBasic);
                    Debug.LogError("Incremental:checkIfLocked: Resources not set up yet.");
                }
                else if (info.soBasic.GetType() == typeof(SO_Item))
                {
                    SO_Item temp = ItemManager.instance.getItem((SO_Item)info.soBasic);
                    //incTemp = ItemManager.instance.getItem((SO_Item)info.soBasic);
                    if((temp != null) && (temp.getAmount() >= info.amount))
                    {
                        //REQUIREMENT SUCCESS!!!
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    Debug.LogError("Incremental:checkIfLocked: unknown SO_basic type of " + info.soBasic.nameTag);
                    return false;
                }
            }
            //all requirements are a success!!
        }
       return true;
    }

}
