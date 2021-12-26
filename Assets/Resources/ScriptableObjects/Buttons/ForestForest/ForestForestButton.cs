using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestForestButton : ButtonEffectScript
{
    IncManager inc;
    SkillSO Foraging;
    ResourceSO Berry;
    ItemSO Twig;

    protected override void Awake()
    {
        base.Awake();   //all the garbage in the method call is to ensure the ButtonEffect still calls it's Awake()
        inc = IncManager.instance;
        Foraging = inc.Get<SkillSO>("Foraging");
        Berry = inc.Get<ResourceSO>("Berry");
        Twig = inc.Get<ItemSO>("Twig");
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
        inc.AddAmount(Foraging, 5);
        inc.AddAmount(Berry, 1);
        inc.AddAmount(Twig, 1);
    }
    
}