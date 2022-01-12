using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiderSwarm : ButtonEffectScript
{
    private void Start()
    {
        int index = Random.Range(0, 4);
        if(index==0)
        {
            setButtonText("Spider", "EEWWW");
        }
        else if(index==1)
        {
            setButtonText("Spider", "squishy?");
        }
        else
        {
            setButtonText("Spider", "*skittering*");
        }
        
    }
    public override void onStart()
    {
        StartCoroutine(SquishSpider());
    }
    IEnumerator SquishSpider()
    {
        bool untoggleEvent = true;
        if (TooltipManager.isEvent) untoggleEvent = false;
        TooltipManager.StartEvent_Static();
        TooltipManager.StartTooltip_Static("SQUISH", true);
        yield return new WaitForSeconds(0.5f);
        if (untoggleEvent) TooltipManager.StopEvent_Static();
        else TooltipManager.StopTooltip_Static(true);
        deleteSelf();
    }
}
