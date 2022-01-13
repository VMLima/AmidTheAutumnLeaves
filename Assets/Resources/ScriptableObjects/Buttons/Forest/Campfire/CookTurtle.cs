using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookTurtle : ButtonEffectScript
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

    IEnumerator consumedTurtle()
    {
        Cursor.lockState = CursorLockMode.Locked;
        TooltipManager.StartEvent_Static();
        float delay = 3f;
        
        yield return new WaitForSeconds(delay);
        TooltipManager.StartTooltip_Static("I can't believe I am doing this", true);
        yield return new WaitForSeconds(delay);
        TooltipManager.StartTooltip_Static("*Nom*", true);
        yield return new WaitForSeconds(delay * 0.75f);
        TooltipManager.StartTooltip_Static("UGHH.. it tastes terrible...", true);
        yield return new WaitForSeconds(delay);
        TooltipManager.StartTooltip_Static("*Nom*", true);
        yield return new WaitForSeconds(delay * 0.75f);
        TooltipManager.StartTooltip_Static("This better be worth it.", true);
        yield return new WaitForSeconds(delay);
        TooltipManager.StartTooltip_Static("*Nom* *Nom*", true);
        yield return new WaitForSeconds(delay * 0.75f);
        TooltipManager.StartTooltip_Static("Ugh my stomach.", true);
        yield return new WaitForSeconds(delay * 1.25f);
        TooltipManager.StartTooltip_Static("*hickup*", true);
        yield return new WaitForSeconds(1);
        TooltipManager.StartTooltip_Static("the last election was a SCAM", true);
        yield return new WaitForSeconds(0.75f);
        TooltipManager.StartTooltip_Static("*hickup*", true);
        yield return new WaitForSeconds(1);
        TooltipManager.StartTooltip_Static("*Nom...*", true);
        yield return new WaitForSeconds(delay * 0.75f);
        TooltipManager.StartTooltip_Static("  *No...*\n*Nom...*", true);
        yield return new WaitForSeconds(delay * 0.75f);
        TooltipManager.StartTooltip_Static("    *N...*\n  *No...*\n*Nom...*", true);
        yield return new WaitForSeconds(delay * 0.75f);
        TooltipManager.StartTooltip_Static("      *...*\n    *N...*\n  *No...*\n*Nom...*", true);
        //ButtonManager.instance.addButtonToUI("CookTurtle", false);
        FlashlightManager.SetDarknessAlpha_Static(0, 0);
        FlashlightManager.SetDarknessAlpha_Static(1, delay);
        yield return new WaitForSeconds(delay);
        TooltipManager.StopEvent_Static();
        yield return new WaitForSeconds(delay);

        //YOU HAVE DIED OF DYSENTARY. (but worth?)
        //THANK YOU FOR PLAYING
        
        Cursor.lockState = CursorLockMode.None;
        GameHandler.instance.Credits();
    }    

    void stageStuff()
    {
        //index is there so I can easily insert stages into spots without having to renumber everything. again.
        int index = 0;
        if (stage == index)
        {
            return;
        }
        index++;
        if (stage == index)
        {
            //complex effects from the last button name
            //ButtonManager.instance.addButtonToUI("SearchArea");

            //changing simple click effects of this one

            //setting name/tooltip of this one
            IncManager.instance.Add<ItemSO>("mcTurtle", -1);
            setTextColor(Color.red);
            setButtonText("Eat?", "...This may be a bad idea...");

            //setting delay till can be pressed again.
            return;
        }
        index++;
        if (stage == index)
        {
            StartCoroutine(consumedTurtle());
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
