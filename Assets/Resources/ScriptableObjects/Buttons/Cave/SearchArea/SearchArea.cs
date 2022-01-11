using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchArea : ButtonEffectScript
{
    bool toggle = false;
    int stage = 0;
    PlayerAttributeSO stamina;
    PlayerAttributeSO health;
    PlayerAttributeSO water;

    private void Start()
    {
        setTooltipHoverDelay(0.5f); //for the sake of it, I am having this buttons tooltip show after 0.5s of hovering instead of the default 1.
        toggleButton = false;   //just making sure.  It is set in the inspector, but I've forgoten before.
        stamina = IncManager.instance.Get<PlayerAttributeSO>("Stamina");
        health = IncManager.instance.Get<PlayerAttributeSO>("Health");
        water = IncManager.instance.Get<PlayerAttributeSO>("Water");
        setButtonText("Where am I ? ", "It is so dark");
        setStartEffect(stamina, -10);
    }

    public override void onStart()
    {
        //1 - a few stages just story text.  Hunger Tutorial.
        //2 - finding medkit and broken down wall.
        //3 - finding soft place to rest
        //4 - finding misc other stuff

        //digging at wall will toggle off rest? 'rest' would be a toggle button.
        if (stage == 0)
        {
            setButtonText("Fumble around. ", "My body hurts so bad.");
            removeStartEffect(stamina);
            PressDelay(1.5f);
        }
        else if (stage == 1)
        {
            //setting them all in 1 line.
            //        (button title,    hover description,     button color)
            setButtonText("Oooh. Soft.", "??????");
            
            PressDelay(1.5f);
        }
        else if (stage == 2)
        {
            setButtonText("Pick up medkit", "Thank god!");
            PressDelay(1.5f);
        }
        else if (stage == 3)
        {
            setButtonText("Drag yourself around", "Seems my legs aren't working too good.");
            IncManager.instance.Add<ItemSO>("MedKit");
            ButtonManager.instance.addButtonToUI("Rest");
            PressDelay(1.5f);
        }
        else if (stage == 4)
        {
            PressDelay(1.5f);
        }
        else if (stage == 5)
        {
            setButtonText("I think I see a light", "Is this...\n is this the end???");
            //ButtonManager.instance.addButtonToUI("Dig");
            PressDelay(1.5f);
        }
        else
        {
            setButtonText("Chill", "You keep pushing my buttons like this and you may just be dead.");
            setTooltipHoverDelay(2.0f);
            PreventPresses();
        }
        refreshTooltip();
        stage++;
    }
}
