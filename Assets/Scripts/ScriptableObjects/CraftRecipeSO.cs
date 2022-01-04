using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//inherits unlocking and button activating.
[CreateAssetMenu(fileName = "CraftRecipe", menuName = "Scriptable Object/CraftRecipe")]
public class CraftRecipeSO : UIMenuSO
{
    public IncrementalValuePair[] craftArray;
    public IncrementalValuePair[] costArray;

    public override void declareUI()
    {
        UIPrefab = IncManager.instance.CraftPrefab;
        UIPanel = IncManager.instance.CraftPanel;
        
        //get the CraftPrefab's Component of CraftEffectScript
        //set its craftArray to yours, and CostArray;
    }

    public override void whenUnlocked()
    {
        isActive = true;
        base.whenUnlocked();
    }

    public override string compileTooltip()
    {
        if (craftArray == null || craftArray.Length <= 0) return "";
        string output = "";
        bool doAnd;
        if (costArray == null || costArray.Length <= 0)
        {

        }
        else
        {
            output += "Recipe = ";
            doAnd = false;
            foreach (IncrementalValuePair pair in costArray)
            {
                if (doAnd) output += " and ";
                doAnd = true;
                output += pair.amount + " " + pair.incrementable.name;
                
            }
            output += "\n";
        }
        output += "Result = ";
        doAnd = false;
        foreach (IncrementalValuePair pair in craftArray)
        {
            if (doAnd) output += " and ";
            doAnd = true;
            output += pair.amount + " " + pair.incrementable.name;
            
        }
        Debug.Log("UIMenuSO:compileTooltip:" + output);
        return output;
    }

    public override void setUIData()
    {
        foreach (Transform eachChild in UIInstance.transform)
        {
            if (eachChild.name == "HookName")
            {
                eachChild.GetComponent<TextMeshProUGUI>().text = name;
            }
        }
        CraftEffectScript c = UIInstance.GetComponent<CraftEffectScript>();
        c.setArrays(craftArray, costArray, this);
    }

    public bool canCraft(float numCrafts = 1)
    {
        //check if everything is unlocked.
        if (!unlocked) return false;
        foreach(IncrementalValuePair pair in craftArray)
        {
            if (!pair.incrementable.unlocked)
            {
                Debug.LogError("CraftRecipeSO:canCraft: cannot craft, results not unlocked");
                return false;
            }
        }

        //if you have the required resources.
        bool _canCraft = costFunction(false, numCrafts);
        //Debug.Log("CraftSO:canCraft: " + _canCraft);
        return _canCraft;

    }

    private bool costFunction(bool modifyValues, float numCrafts)
    {
        if ((costArray != null) && (costArray.Length > 0))
        {
            foreach (IncrementalValuePair cost in costArray)
            {
                //get its type
                //find the actual created object
                if (cost.incrementable != null)
                {
                    if (modifyValues == false)
                    {
                        if (cost.incrementable.getAmount() < (cost.amount * numCrafts))
                        {
                            //if you do not have the resources... and checking to see if all resource requirements exist...
                            return false;
                        }
                    }
                    else
                    {
                        //if you are modifying values...
                        //Debug.Log("CraftSO:costFunction: modifying values..." + cost.unlocker.getAmount() + " " + cost.amount * -1 * numCrafts);
                        IncManager.instance.AddAmount(cost.incrementable, cost.amount * -1 * numCrafts);
                    }
                }
            }
            if(modifyValues)
            {
                //Debug.Log("CraftSO:costFunction: modifying values..." + toCraft.getAmount() + " +" + amountCrafted * numCrafts);
                foreach(IncrementalValuePair craft in craftArray)
                {
                    IncManager.instance.AddAmount(craft.incrementable, craft.amount * numCrafts);
                }
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
