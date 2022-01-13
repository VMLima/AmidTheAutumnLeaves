using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : ButtonEffectScript
{
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
    }
    public override void onStart()
    {
        if(IncManager.instance.GetAmount<ItemSO>("Basket")>0)
        {
            IncManager.instance.Add<ResourceSO>("Fish");
            StartCoroutine(SquishSpider());
        }
        else
        {
            StartCoroutine(noCatch());
        }
    }

    IEnumerator noCatch()
    {
        bool untoggleEvent = true;
        if (TooltipManager.isEvent) untoggleEvent = false;
        //TooltipManager.StartEvent_Static();
        //TooltipManager.StartTooltip_Static("I caught one!!  But I have nothing to put it in.....", true);
        yield return new WaitForSeconds(0.5f);
        //if (untoggleEvent) TooltipManager.StopEvent_Static();
        //else TooltipManager.StopTooltip_Static(true);
        deleteSelf();
    }

    IEnumerator SquishSpider()
    {
        bool untoggleEvent = true;
        if (TooltipManager.isEvent) untoggleEvent = false;
       // TooltipManager.StartEvent_Static();
       // TooltipManager.StartTooltip_Static("I caught one!!", true);
        yield return new WaitForSeconds(0.5f);
       // if (untoggleEvent) TooltipManager.StopEvent_Static();
       // else TooltipManager.StopTooltip_Static(true);
        deleteSelf();
    }
}
