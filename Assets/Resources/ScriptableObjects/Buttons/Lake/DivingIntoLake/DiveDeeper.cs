using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveDeeper : ButtonEffectScript
{

    int stage = 0;
    float deeperCount = 0;

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
        ButtonManager.instance.addButtonToUI("LakeTreasure", false);
        FlashlightManager.SetDarknessAlpha_Static(0, 1);
        FlashlightManager.HideLight_Static();
        FlashlightManager.HideDarkness_Static();
        AllowPresses();
        stage = toSet;
        deeperCount = 0;
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
            FlashlightManager.showSmallLight();
            FlashlightManager.SetDarknessAlpha_Static(0, 0);
            FlashlightManager.SetDarknessAlpha_Static(0.1f, 1);
            setButtonText("Even Deeper", "..");

            //setting delay till can be pressed again.
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Even Even Deeper", "");
            FlashlightManager.SetDarknessAlpha_Static(0.25f, 1);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Even Even Deeper Deeper", ".glub.glubglub glub");
            FlashlightManager.SetDarknessAlpha_Static(0.35f, 1);
            return;
        }
        index++;
        if (stage == index)
        {
            
            //FlashlightManager.ShowLight_Static();
            FlashlightManager.SetDarknessAlpha_Static(0.6f, 1);
            setButtonText("Deeper...", "!glub!glub!");
            return;
        }
        index++;
        if (stage == index)
        {
            FlashlightManager.SetDarknessAlpha_Static(0.7f, 1);
            setButtonText("Keep Going Deeper!!", "!glub !glub!glub! glub!");
            return;
        }
        index++;
        if (stage == index)
        {
            if (IncManager.instance.GetAmount<ItemSO>("Chopper") <= 0) ButtonManager.instance.addButtonToUI("LakeTreasure");
            FlashlightManager.SetDarknessAlpha_Static(0.75f, 1);
            setButtonText("Keep Going Deeper than Deeper!!", "!!!");
            return;
        }
        index++;
        
        deeperCount +=0.05f;
        FlashlightManager.SetDarknessAlpha_Static(0.7f + deeperCount, 1);
        setButtonText("Keep Going Deeper than Deeper!!", "!!!!!!!");
        PreventPresses();
        return;
        
    }
    void everyTime()
    {
        PressDelay(1.25f);
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }


}
