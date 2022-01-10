using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWont : ButtonEffectScript
{
    bool toggle = false;
    int stage = 0;

    private void Start()
    {
        setTooltipHoverDelay(0.5f); //for the sake of it, I am having this buttons tooltip show after 0.5s of hovering instead of the default 1.
        toggleButton = false;   //just making sure.  It is set in the inspector, but I've forgoten before.
    }

    public override void onStart()
    {
        //Each time the button is pressed.

        if(stage == 0)
        {
            //individually setting each value in a button.
            setTitle("You are dead.");
            setTooltip("Wat u going to do about it?");
            setColor(Color.red);

            setTooltipHoverDelay(1.0f); //when mousing over this button, how long it takes for the tooltip window to show up.
            refreshTooltip();  //the current tooltip will blink out.  After the time (no value means use hover delay) it will appear again and be updated.
            PressDelay(5);  //this button will ignore being pressed again for 5 seconds.
            
            //Red, you are dead
        }
        else if(stage == 1)
        {
            //setting them all in 1 line.
            //        (button title,    hover description,     button color)
            setButtonInfo("JK", "Just a joke bro, why so mad.", Color.green);
            refreshTooltip(1.5f);
            setTooltipHoverDelay(1.5f);
            PressDelay(5);
        }
        else if(stage == 2)
        {
            setButtonInfo("Chill","You keep pushing my buttons like this and you may just be dead.",Color.yellow);
            refreshTooltip(0.5f);
            setTooltipHoverDelay(2.0f);
            PreventPresses();
        }
        stage++;
    }
}
