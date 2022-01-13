using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveDeeper : ButtonEffectScript
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
        
        setTooltipHoverDelay(0.75f);
    }

    public void setStage(int toSet)
    {
        AllowPresses();
        stage = toSet;
        stageStuff();
    }

    public void incrementStage(int toAdd)
    {
        stage += toAdd;
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
            //complex effects from the last button name

            //changing simple click effects of this one
            //setStartEffect(stamina, 5);    //if there is a stamina cost, or if there isn't, the new stamina cost is 5.
            //removeStartEffect(thirst);    //remove any thirst costs.

            //setting name/tooltip/color of this one
            setButtonText("Dive Deeper", ".gurgle.");

            //setting delay till can be pressed again.

            return;
        }
        index++;
        if (stage == index)
        {
            //complex effects from the last button name
            //ButtonManager.instance.addButtonToUI("SearchArea");

            //changing simple click effects of this one

            //setting name/tooltip of this one
            setButtonText("Even Deeper", ".flub.");

            //setting delay till can be pressed again.
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Even Even Deeper", ".gurgle.gurgle.");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Even Even Deeper Deeper", ".glub.\nglub\nglub\nglub");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("A shiney!!!!", ".glub.\nglub\nglub\nglub");
            return;
        }
        index++;
        if (stage == index)
        {
            IncManager.instance.Add<ItemSO>("Chopper");
            setButtonText("Keep Going Deeper!!", ".glub.\nglub\nglub\nglub");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Keep Going Deeper than Deeper!!", ".glub.\nglub\nglub\nglub");
            PreventPresses();
            return;
        }
    }
    void everyTime()
    {
        PressDelay(1.25f);
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }


}
