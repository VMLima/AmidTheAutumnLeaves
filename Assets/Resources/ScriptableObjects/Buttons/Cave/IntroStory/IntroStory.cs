using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStory : ButtonEffectScript
{

    bool toggle = false;
    int stage = 0;
    PlayerAttributeSO stamina;
    PlayerAttributeSO health;
    PlayerAttributeSO water;

    private void Start()
    {
        toggleButton = false;   //just making sure.  It is set in the inspector, but I've forgoten before.
        stamina = IncManager.instance.Get<PlayerAttributeSO>("Stamina");
        health = IncManager.instance.Get<PlayerAttributeSO>("Health");
        water = IncManager.instance.Get<PlayerAttributeSO>("Water");
        defaultValues();
    }

    public void defaultValues()
    {
        stage = 0;
        onStart();
        setTooltipHoverDelay(0.5f);
    }

    void stageStuff()
    {
        int index = 0;
        if (stage == index)
        {
            //complex effects from the last button name

            //changing simple click effects of this one
                //setStartEffect(stamina, 5);    //if there is a stamina cost, or if there isn't, the new stamina cost is 5.
                //removeStartEffect(thirst);    //remove any thirst costs.
            //setting name/tooltip of this one
            setButtonText("introStory0", "introStory0 tooltip");

            //setting delay till can be pressed again.
            return;
        }
        index++;
        if (stage == index)
        {
            //complex effects from the last button name
            ButtonManager.instance.addButtonToUI("SearchArea");

            //changing simple click effects of this one
            
            //setting name/tooltip of this one
            setButtonText("introStory1", "introStory1 tooltip");

            //setting delay till can be pressed again.
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("introStory2", "introStory2 tooltip");
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("introStory3", "introStory3 tooltip");
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("introStory4", "introStory4 tooltip");
            PreventPresses();
            return;
        }
    }
    void everyTime()
    {
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }

    public override void onStart()
    {
        //intro story stuff.
        stageStuff();
        everyTime();
    }
}
