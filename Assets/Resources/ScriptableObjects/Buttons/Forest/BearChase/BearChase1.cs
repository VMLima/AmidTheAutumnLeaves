using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearChase1 : ButtonEffectScript
{
    public ButtonSO bearchase1;
    public ButtonSO bearChase2;
    BearChase2 _bearChase2;
    public ButtonSO bearChase3;
    BearChase3 _bearChase3;
    //ienumerator running that checks if clicked flag changed.
    //if it has, no damage, if it hasn't, damage.
    bool chasing = true;
    bool wasPress = false;
    bool thisWasPressed = false;
    bool doNotProgress = false;

    bool toggle = false;
    int stage = 0;
    PlayerAttributeSO stamina;
    PlayerAttributeSO health;
    PlayerAttributeSO water;

    void sendPresses()
    {
        _bearChase2.goToNext();
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
        _bearChase2 = bearChase2.UIInstance.GetComponent<BearChase2>();
        _bearChase3 = bearChase3.UIInstance.GetComponent<BearChase3>();
        //if the button needs to be reset (like new game) the stuff that needs to be set
        defaultValues();
    }

    public void defaultValues()
    {
        stage = 0;
        onStart();

        //the tooltip will show up after 0.5s of hovering over the button.  Default is 1s. Low values for more story/actiony things.
        setTooltipHoverDelay(0.5f);
        StartCoroutine(chaseLoop());
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
            setButtonText("RUN LEFT");
            return;
        }
        index++;
        if (stage == index)
        {
            chasing = true;
            setButtonText("RUN");
            return;
        }
        
        index++;
        if (stage == index)
        {
            setButtonText("RUN UP HILL");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Beg the Bear to stop.");
            return;
        }
        index++;
        if (stage == index)
        {
            if (thisWasPressed) doNotProgress = true;
            else setButtonText("SCOOP UP TURTLE");
            return;
        }
        index++;
        if (stage == index)
        {
            IncManager.instance.Add<ResourceSO>("Turtle");
            setButtonText("ESCAPE!!");
            return;
        }
        else
        {
            chasing = false;
            StopCoroutine(chaseLoop());
            StopCoroutine(chaseDamage());
            TooltipManager.StopTooltip_Static(true);
            TooltipManager.StopEvent_Static();

            ButtonManager.instance.addButtonArrayToUI("BearChase", false);
            ButtonManager.instance.addButtonArrayToUI("TheForest");
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
    IEnumerator chaseLoop()
    {
        //Debug.Log("Bearchase1:chaseLoop:starting");
        TooltipManager.StartEvent_Static();
        while (chasing)
        {
            //Debug.Log("Bearchase1:chaseLoop:loop");
            yield return new WaitForSeconds(1f);
            //Debug.Log("Bearchase1:chaseLoop:loopAfterTimer");
            if (wasPress) wasPress = false;
            else StartCoroutine(chaseDamage());
        }
        TooltipManager.StopEvent_Static();
        //Debug.Log("Bearchase1:chaseLoop:stopping");
        yield return new WaitForEndOfFrame();
    }

    IEnumerator chaseDamage()
    {
        IncManager.instance.Add(health, -5);
        int i = Random.Range(0, 4);
        if (i == 0) TooltipManager.StartTooltip_Static("IT BIT ME!!", true);
        else if (i == 1) TooltipManager.StartTooltip_Static("MY LEG!!", true);
        else if (i == 2) TooltipManager.StartTooltip_Static("IT CLAWED ME!!", true);
        else TooltipManager.StartTooltip_Static("I AM DYING!!", true);
        yield return new WaitForSeconds(0.75f);
        TooltipManager.StopTooltip_Static(true);
    }
}
