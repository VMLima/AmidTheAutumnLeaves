using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearChase2 : ButtonEffectScript
{
    public ButtonSO bearChase1;
    public ButtonSO bearChase2;
    BearChase1 _bearChase1;
    public ButtonSO bearChase3;
    BearChase3 _bearChase3;
    //ienumerator running that checks if clicked flag changed.
    //if it has, no damage, if it hasn't, damage.
    bool chasing = true;
    bool wasPress = true;
    bool thisWasPressed = false;
    bool doNotProgress = false;

    bool toggle = false;
    int stage = 0;
    PlayerAttributeSO stamina;
    PlayerAttributeSO health;
    PlayerAttributeSO water;

    void sendPresses()
    {
        _bearChase1.goToNext();
        _bearChase3.goToNext();
    }

    public void goToNext()
    {
        thisWasPressed = false;
        onStart();
    }



    private void Start()
    {
        toggleButton = false;   //just making sure.  It is set in the inspector, but I've forgoten before.

        //me setting the values I may use later for easy quick access.
        stamina = IncManager.instance.Get<PlayerAttributeSO>("Stamina");
        health = IncManager.instance.Get<PlayerAttributeSO>("Health");
        water = IncManager.instance.Get<PlayerAttributeSO>("Water");

        //yield return new WaitForEndOfFrame();
        _bearChase1 = bearChase1.UIInstance.GetComponent<BearChase1>();
        _bearChase3 = bearChase3.UIInstance.GetComponent<BearChase3>();
        //if the button needs to be reset (like new game) the stuff that needs to be set
        defaultValues();
    }

    public void defaultValues()
    {
        stage = 0;
        thisWasPressed = false;
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
        if(chasing) everyTime();
    }

    void stageStuff()
    {
        //index is there so I can easily insert stages into spots without having to renumber everything. again.
        
        int index = 0;
        if (stage == index)
        {
            chasing = true;
            setButtonText("RUN AWAY!!!");
            return;
        }
        index++;
        if (stage == index)
        {
            chasing = true;
            setButtonText("RUN FASTER");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("RUN INTO TREE");
            return;
        }
        index++;
        if (stage == index)
        {
            if(thisWasPressed) doNotProgress = true;
            else setButtonText("SCREAM AND RUN.");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("TRIP OVER TURTLE");
            return;
        }
        index++;
        if (stage == index)
        {
            if (thisWasPressed) doNotProgress = true;
            else setButtonText("Get Lost!!");
            return;
        }
        else
        {
            if (thisWasPressed) doNotProgress = true;
            else chasing = false;
            return;
        }
    }
    void everyTime()
    {
        if (doNotProgress)
        {
            doNotProgress = false;
            return;
        }

        if (thisWasPressed)
        {
            sendPresses();
            PressDelay(0.5f);
        }
        else
        {
            PressDelay(0.2f);
        }
        wasPress = true;
        thisWasPressed = true;
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }
}
