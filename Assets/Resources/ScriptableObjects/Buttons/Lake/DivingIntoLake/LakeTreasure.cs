using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeTreasure : ButtonEffectScript
{
    int stage = 0;

    private void Start()
    {
        toggleButton = false;   //just making sure.  It is set in the inspector, but I've forgoten before.

        //me setting the values I may use later for easy quick access.

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

    //called on button press.
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
            setTextColor(Color.blue);
            setButtonText("Shiny?", "...");
            return;
        }
        index++;
        if (stage == index)
        {
            setTextColor(Color.black);
            setButtonText("!!!", "It's sharp!!");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Carefully pick up.", "");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Carefully pick up.", "");
            IncManager.instance.Add<ItemSO>("Chopper");
            ButtonManager.instance.addButtonToUI("LakeTreasure", false);
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
