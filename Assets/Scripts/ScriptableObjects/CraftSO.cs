using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//inherits unlocking and button activating.
[CreateAssetMenu(fileName = "CraftRecipe", menuName = "Scriptable Object/CraftRecipe")]
public class CraftSO : UnlockableObjectSO
{
    public IncrementableSO toCraft;
    public int amountCrafted;
    public LockInfoSO[] costArray = new LockInfoSO[0];

    /*
    public override void setButtonInfo()
    {
        //HAVE TO SET THE VALUES STILL.
        UIPanel = null;
        buttonPrefab = null;
    }
    */

    public override void declareUI()
    {
        UIPrefab = null;    //still gotta make and hook up to the prefab.
        //UIImage
        //name
        //will be fed into the prefab.
        //textDisplay = (numerical text output panel.  Will get updated on addAmount())
        UIPanel = IncManager.instance.SkillPanel;
    }
    public bool canCraft(float numCrafts = 1)
    {
        //if you have more than enough of each to craft X times...
        //can be a decimal too.
        if(!toCraft.unlocked || !unlocked)
        {
            return false;
        }

        bool _canCraft = costFunction(false, numCrafts);
        //Debug.Log("CraftSO:canCraft: " + _canCraft);
        return _canCraft;

    }

    private bool costFunction(bool modifyValues, float numCrafts)
    {
        if ((costArray != null) && (costArray.Length > 0))
        {
            foreach (LockInfoSO cost in costArray)
            {
                //get its type
                //find the actual created object
                if (cost.unlocker != null)
                {
                    if (modifyValues == false)
                    {
                        if (cost.unlocker.getAmount() < (cost.amount * numCrafts))
                        {
                            //if you do not have the resources... and checking to see if all resource requirements exist...
                            return false;
                        }
                    }
                    else
                    {
                        //if you are modifying values...
                        //Debug.Log("CraftSO:costFunction: modifying values..." + cost.unlocker.getAmount() + " " + cost.amount * -1 * numCrafts);
                        IncManager.instance.AddAmount(cost.unlocker, cost.amount * -1 * numCrafts);
                    }
                }
            }
            if(modifyValues)
            {
                //Debug.Log("CraftSO:costFunction: modifying values..." + toCraft.getAmount() + " +" + amountCrafted * numCrafts);
                IncManager.instance.AddAmount(toCraft, amountCrafted * numCrafts);
            }
            //all requirements are a success!!
        }
        return true;
    }
    public void craft(float numCrafts = 1)
    {
        if(canCraft(numCrafts))
        {
            costFunction(true, numCrafts);
        }
    }
}
