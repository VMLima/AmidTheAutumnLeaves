using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : ButtonEffectScript
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

    IEnumerator startChase()
    {

        TooltipManager.StartEvent_Static();
        ButtonManager.instance.addButtonArrayToUI("TheForest", false);
        setButtonColor(Color.gray);
        setButtonText("Bear", "");
        yield return new WaitForSeconds(1);
        setButtonText("?", "");
        yield return new WaitForSeconds(1.5f);
        setButtonColor(Color.red);
        yield return new WaitForSeconds(0.25f);
        setButtonColor(Color.gray);
        yield return new WaitForSeconds(0.7f);
        setButtonColor(Color.red);
        yield return new WaitForSeconds(0.25f);
        setButtonColor(Color.gray);
        setButtonText("...");
        yield return new WaitForSeconds(2.5f);
        setButtonColor(Color.red);
        setButtonText("RUN");
        setTooltip("");
        yield return new WaitForSeconds(1.0f);
        ButtonManager.instance.addButtonToUI("Bear", false);
        ButtonManager.instance.addButtonArrayToUI("BearChase");
        TooltipManager.StopEvent_Static();
    }

    void stageStuff()
    {
        //index is there so I can easily insert stages into spots without having to renumber everything. again.
        int index = 0;
        if (stage == index)
        {
            setButtonText("Pick up Stick", "huh.  Very pointy.");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Look around", "is that a bear?");
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Poke it with stick.", "100% success rate in identifying if bear.");
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            //hide forest buttons
            ButtonManager.instance.addButtonArrayToUI("TheForest", false);
            //start chase button
            ButtonManager.instance.addButtonToUI("Bear");
            PreventPresses();

            StartCoroutine(startChase());
            return;
        }
    }
    void everyTime()
    {
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }
}
