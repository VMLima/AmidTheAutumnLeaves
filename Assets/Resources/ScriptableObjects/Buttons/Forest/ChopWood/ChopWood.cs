using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopWood : ButtonEffectScript
{
    int stage = 0;
    private void Start()
    {
        toggleButton = false;   //just making sure.  It is set in the inspector, but I've forgoten before.
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
            setButtonText("Chop Wood?", "Fee Fi Fo Fum!");

            //setting delay till can be pressed again.

            return;
        }
        index++;
        if (stage == index)
        {
            if(IncManager.instance.GetAmount<ItemSO>("Chopper")>0)
            {
                setButtonText("Chop Wood", "Fee Fi Fo Fum!");
                IncManager.instance.Add<ItemSO>("Wood", 1);
                ButtonManager.instance.addButtonToUI("Bear");
            }
            else
            {
                setButtonText("With what?", "I can't chop wood with intention alone.");
            }
            stage--;
            return;
        }
    }
    void everyTime()
    {
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }


}
