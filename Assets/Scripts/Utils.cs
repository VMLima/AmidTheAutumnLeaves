using UnityEngine;

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

    public static bool checkUnlocked(LockInfo[] lockList)
    {
        if ((lockList != null) && (lockList.Length > 0))
        {
            foreach (LockInfo info in lockList)
            {
                //get its type
                //find the actual created object
                if (info.soBasic.GetType() == typeof(SkillSO))
                {
                    SkillSO temp = SkillManager.instance.getSkill((SkillSO)info.soBasic);
                    if ((temp != null) && (temp.getLevel() >= info.amount))
                    {
                        //REQUIREMENT SUCCESS!!
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (info.soBasic.GetType() == typeof(ResourceSO))
                {
                    //incTemp = ResourceManager.instance.getResource((SO_Resource)info.soBasic);
                    Debug.LogError("Incremental:checkIfLocked: Resources not set up yet.");
                }
                else if (info.soBasic.GetType() == typeof(ItemSO))
                {
                    ItemSO temp = ItemManager.instance.getItem((ItemSO)info.soBasic);
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
