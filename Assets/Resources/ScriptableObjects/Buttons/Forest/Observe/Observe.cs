using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observe : ButtonEffectScript
{
    bool toggle = false;
    int stage = 0;
    PlayerAttributeSO stamina;
    PlayerAttributeSO health;
    PlayerAttributeSO water;

    private void Start()
    {
        toggleButton = false;   //just making sure.  It is set in the inspector, but I've forgoten before.

        //me setting the values I may use later for easy quick access.
        stamina = IncManager.instance.Get<PlayerAttributeSO>("Stamina");
        health = IncManager.instance.Get<PlayerAttributeSO>("Health");
        water = IncManager.instance.Get<PlayerAttributeSO>("Water");

        //if the button needs to be reset (like new game) the stuff that needs to be set
        defaultValues();
    }

    public void defaultValues()
    {
        stage = 0;
        onStart();

        //the tooltip will show up after 0.5s of hovering over the button.  Default is 1s. Low values for more story/actiony things.
        setTooltipHoverDelay(1.25f);
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
            setButtonText("Take it all in", "it is really nice out.");

            //setting delay till can be pressed again.

            return;
        }
        index++;
        if (stage == index)
        {
            //complex effects from the last button name
            

            //changing simple click effects of this one

            //setting name/tooltip of this one
            setButtonText("Look for anything usefull", "");

            //setting delay till can be pressed again.
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            ButtonManager.instance.addButtonToUI("Bear");
            setButtonText("Look around some more", "What do your elf eyes see?");
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Just lookin around.", "huh.");
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Nothing more to see", "100% achievement.  Have seen nature.  Where is home again?");
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
