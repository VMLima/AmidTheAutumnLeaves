using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : ButtonEffectScript
{
    bool wasPressed = false;
    private void Start()
    {
        int index = Random.Range(0, 4);
        if (index == 0)
        {
            setButtonText("Big Fish", "");
        }
        else if (index == 1)
        {
            setButtonText("Just Right Fish", "");
            
        }
        else
        {
            setButtonText("Small Fish", "");
        }

        StartCoroutine(swimAway());
    }
    public override void onStart()
    {
        if (wasPressed) return;
        wasPressed = true;
        if(IncManager.instance.GetAmount<ItemSO>("Basket")>0)
        {
            IncManager.instance.Add<ResourceSO>("Fish");
            setButtonText("CAUGHT");
            StartCoroutine(SquishSpider());
        }
        else
        {
            StartCoroutine(letFishGo());
        }
    }

    IEnumerator swimAway()
    {
        yield return new WaitForSeconds(Random.Range(0.5f,1.25f));
        if (!wasPressed) deleteSelf();
    }

    IEnumerator letFishGo()
    {
        TooltipManager.StartEvent_Static();
        TooltipManager.StartTooltip_Static("You have no where to put the fish, so you just give it a nice lil pat.", true);
        setButtonText("Happy Fish");
        yield return new WaitForSeconds(2);
        TooltipManager.StopEvent_Static();
        deleteSelf();
    }

    IEnumerator SquishSpider()
    {
        wasPressed = true;
        yield return new WaitForSeconds(0.5f);
        deleteSelf();
    }
}
