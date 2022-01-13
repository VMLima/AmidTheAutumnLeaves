using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearFabric : ButtonEffectScript 
{
    int stage = 0;

    private void Start()
    {
        toggleButton = false;   //just making sure.  It is set in the inspector, but I've forgoten before.

        //if the button needs to be reset (like new game) the stuff that needs to be set
        defaultValues();
    }

    public void defaultValues()
    {
        stage = 0;
        onStart();

        //the tooltip will show up after 0.5s of hovering over the button.  Default is 1s. Low values for more story/actiony things.
        setTooltipHoverDelay(0.5f);
    }
    public override void onStart()
    {
        //stuff that happens specific to the current stage
        stageStuff();
        //stuff that happens every press
        everyTime();
    }

    void stageStuff()
    {
        //index is there so I can easily insert stages into spots without having to renumber everything. again.
        int index = 0;
        if (stage == index)
        {
            setButtonText("Tear shirt for fabric", "I could use some cloth");
            
            return;
        }
        index++;
        if (stage == index)
        {
            IncManager.instance.Add<ItemSO>("Cloth", 3);
            IncManager.instance.Add<ItemSO>("Shirt", -1);
            setButtonText("Tear pants for fabric", "I could use some cloth");
            return;
        }
        index++;
        if (stage == index)
        {
            IncManager.instance.Add<ItemSO>("Cloth", 3);
            IncManager.instance.Add<ItemSO>("Pants", -1);
            setButtonText("I am naked", "Strangely exilerating.");
            PreventPresses();
            return;
        }
    }
    void everyTime()
    {
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }


}