using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : ButtonEffectScript
{

    bool toggle = false;
    int stage = 0;
    float delayPresses = 1;
    float tooltipDelay = 1.25f;
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
        setTooltipHoverDelay(tooltipDelay);
    }

    //called on button press.
    public override void onStart()
    {
        //stuff that happens specific to the current stage
        stageStuff();
        //stuff that happens every press
        everyTime();
    }

    public void incrementStage(int amount)
    {
        stage += amount;
        onStart();
    }

    void stageStuff()
    {
        //index is there so I can easily insert stages into spots without having to renumber everything. again.
        int index = 0;
        if (stage == index)
        {
            setButtonText("Toes are about to freeze off", "Shiver me timbers!");
            return;
        }
        index++;
        if (stage == index)
        {
            setStartEffect(stamina, -5);
            setButtonText("I could really use a fire", "What do I need again...");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("I need stones", "12 of em for the fire pit");
            return;
        }
        index++;
        if (stage == index)
        {
            if (IncManager.instance.GetAmount<ItemSO>("Stone") >= 12)
            {
                IncManager.instance.Add<ItemSO>("Stone", -12);
                setButtonText("Ahh, there we go", "All nice and perty");
            }
            else
            {
                setButtonText("Still need stones", "At least 12 of em");
                stage--;
            }
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Arrange Twigs", "need at least 5 of em for a teepee.");
            return;
        }
        index++;
        if (stage == index)
        {
            if (IncManager.instance.GetAmount<ItemSO>("Twig") >= 5)
            {
                IncManager.instance.Add<ItemSO>("Twig", -5);
                setButtonText("Larger wood chunks", "Need at least 2 large hunks of wood.");
            }
            else
            {
                //setButtonText("I need twigs", "At least 3 of em");
                stage--;
            }
            return;
        }
        index++;
        if (stage == index)
        {
            if (IncManager.instance.GetAmount<ItemSO>("Wood") >= 2)
            {
                IncManager.instance.Add<ItemSO>("Wood", -2);
                setButtonText("Make a Bow Drill!!", "Need 2 twigs and some cloth");
            }
            else
            {
                //setButtonText("Larger wood chunks", "Need at least 2 large hunks of wood.");
                stage--;
            }
            return;
        }
        index++;
        if (stage == index)
        {
            if (IncManager.instance.GetAmount<ItemSO>("Twig") >= 2 && IncManager.instance.GetAmount<ItemSO>("Cloth") >= 2)
            {
                IncManager.instance.Add<ItemSO>("Twig", -2);
                IncManager.instance.Add<ItemSO>("Cloth", -2);
                IncManager.instance.Add<ItemSO>("BowDrill", 1);
                setButtonText("Start this fire", "done it a million times.");
            }
            else
            {
                setButtonText("I need stuff", "At least 2 twigs and 2 cloth");
                stage--;
            }
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Drill Fast", "");
        }
        index++;
        if (stage == index)
        {
            setButtonText("Drill Faster", "");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Drill Fastest", "");
            return;
        }
        index++;
        if (stage == index)
        {
            
            setButtonColor(Color.yellow);
            setButtonText("Campfire", "It is so warm");
            return;
        }
        index++;
        if (stage == index)
        {
            delayPresses = 0.5f;
            setButtonColor(Color.gray);
            ButtonManager.instance.addButtonArrayToUI("TheForest", false);
            ButtonManager.instance.addButtonArrayToUI("CampfireArray");
            setButtonText("Leave Campfire", "But it is so warm");
            return;
        }
        index++;
        if (stage == index)
        {
            stage -= 2;
            setButtonColor(Color.yellow);
            ButtonManager.instance.addButtonArrayToUI("CampfireArray", false);
            ButtonManager.instance.addButtonArrayToUI("TheForest");
            setButtonText("Campfire", "It is so warm");
            return;
        }
    }
    void everyTime()
    {
        PressDelay(delayPresses);
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }
}
