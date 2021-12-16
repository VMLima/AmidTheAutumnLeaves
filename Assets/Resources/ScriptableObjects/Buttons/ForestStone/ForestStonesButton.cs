using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestStonesButton : ButtonEffect
{
    IncManager inc;
    SkillSO Foraging;

    protected override void Awake()
    {
        base.Awake();   //all the garbage in the method call is to ensure the ButtonEffect still calls it's Awake()
        inc = IncManager.instance;
        Foraging = inc.Get<SkillSO>("Foraging");
    }


    public override void onStart()
    {
        Debug.Log("GOT " + name + " BUTTON start");
    }
    public override void onStop()
    {
        Debug.Log("GOT " + name + " stop");
    }
    public override void onTick()
    {
        Debug.Log("GOT " + name + " Tick");
        inc.AddAmount(Foraging, 1);
    }

}