using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigOut : ButtonEffectScript
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
            setButtonText("digOut0", "digOut0 tooltip");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("digOut1", "digOut1 tooltip");

            PressDelay(1.5f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("digOut2", "digOut2 tooltip");
            PressDelay(1.5f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("digOut3", "digOut3 tooltip");
            PressDelay(1.5f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("digOut4", "digOut4 tooltip");
            return;
        }
        else
        {
            PreventPresses();
            StartCoroutine(CaveIn());
        }
    }
    IEnumerator CaveIn()
    {
        TooltipManager.StartEvent_Static();
        TooltipManager.StartTooltip_Static("rumble", true);
        yield return new WaitForSeconds(2);
        TooltipManager.StartTooltip_Static("RUMBLE", true);
        yield return new WaitForSeconds(1.5f);
        TooltipManager.StartTooltip_Static("BAM", true);
        yield return new WaitForSeconds(0.75f);
        TooltipManager.StartTooltip_Static("BOOM", true);
        yield return new WaitForSeconds(0.5f);
        TooltipManager.StartTooltip_Static("CRASH", true);
        yield return new WaitForSeconds(0.5f);
        TooltipManager.StartTooltip_Static("POP!", true);
        yield return new WaitForSeconds(0.35f);
        TooltipManager.StopEvent_Static();
        ButtonManager.instance.deactivateAllButtons();
        ButtonManager.instance.addButtonArrayToUI("CaveIn");
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
