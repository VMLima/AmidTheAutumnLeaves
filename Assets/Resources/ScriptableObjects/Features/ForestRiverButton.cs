using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestRiverButton : ButtonEffect
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
        Debug.Log("GOT " + nameTag + " BUTTON start");
    }
    public override void onStop()
    {
        Debug.Log("GOT " + nameTag + " stop");
    }
    public override void onTick()
    {
        Debug.Log("GOT " + nameTag + " Tick");
        inc.AddAmount(Foraging, 10);
    }

}