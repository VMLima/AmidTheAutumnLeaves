using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : ButtonEffectScript
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
            //complex effects from the last button name

            //changing simple click effects of this one
            //setStartEffect(stamina, 5);    //if there is a stamina cost, or if there isn't, the new stamina cost is 5.
            //removeStartEffect(thirst);    //remove any thirst costs.

            //setting name/tooltip/color of this one
            setButtonText("Toes are about to freeze off", "I need fire!!");

            //setting delay till can be pressed again.

            return;
        }
        index++;
        if (stage == index)
        {
            setStartEffect(stamina, -5);
            setButtonText("Rub two sticks together", "I have no idea what I'm doing...");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Try and rub in the same spot??", "Still no idea what I'm doing...");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("ooo, what about a twisting motion", "I think I get it now");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Don't give up!", "Almost got it!");
            
            return;
        }
        index++;
        if (stage == index)
        {
            removeStartEffect(stamina);
            setButtonText("check the smoke", "Tinder! I need Tinder!");

            return;
        }
        index++;
        if (stage == index)
        {
            ButtonManager.instance.addButtonToUI("AddMoss");
            setButtonText("Pick up dry leaves", "Is this right??");
            return;
        }
        index++;
        if (stage == index)
        {
            setStartEffect(stamina, -5);
            setButtonText("More stick rubbing!", "Hawt.");
            return;
        }
        index++;
        if (stage == index)
        {
            removeStartEffect(stamina);
            setButtonText("Gather kindling", "Looking good!");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonColor(Color.yellow);
            ButtonManager.instance.addButtonToUI("AddMoss", false);
            setButtonText("Campfire", "It is so warm");
            return;
        }
        index++;
        if (stage == index)
        {
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
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }
}
