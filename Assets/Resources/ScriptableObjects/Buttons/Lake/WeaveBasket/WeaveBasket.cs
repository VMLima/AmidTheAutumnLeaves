using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaveBasket : ButtonEffectScript
{
    bool toggle = false;
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
            setButtonText("Hmmm", "...");

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
            setButtonText("I think..", "I could weave a basket!");

            //setting delay till can be pressed again.
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Gather Fronds", "LA La LA.");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Sit in the lake and weave", "Do Da do.");
            return;
        }
        index++;
        if (stage == index)
        {
            IncManager.instance.Add<ItemSO>("Basket", 1);
            setButtonText("I Have a Basket!", "woopie!");
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
