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
    public RockController rockController;

    private void Start()
    {

        rockController = GetComponentInParent<RockController>();
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
            setButtonText("Try and crawl out", "I can see the light!!!!");
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("I can't fit", "maybe if I shift some of these boulders");

            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Shove large rocks asside", "I am feeling the effects of masculinity");
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Shove more rocks asside", "MUST GET OUT!!");
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            setButtonText("Shove even more rocks", "I can feel it!! I'm nearly there!!!");
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
        FlashlightManager.SetDarknessAlpha_Static(0,0); //instantly change to 0.
        TooltipManager.StartTooltip_Static("RUMBLE", true);
        FlashlightManager.SetDarknessAlpha_Static(0.05f, 1.5f); //over 1.5 seconds, change alpha to 0.05 alpha
        yield return new WaitForSeconds(1.5f);
        TooltipManager.StartTooltip_Static("BAM", true);
        
        yield return new WaitForSeconds(0.75f);
        TooltipManager.StartTooltip_Static("BOOM", true);
        yield return new WaitForSeconds(0.5f);
        FlashlightManager.SetDarknessAlpha_Static(0.15f, 0.5f);
        TooltipManager.StartTooltip_Static("CRASH", true);
        yield return new WaitForSeconds(0.5f);
       
        TooltipManager.StartTooltip_Static("POP!", true);
        FlashlightManager.SetDarknessAlpha_Static(0.65f, 0.15f);
        rockController.SpawnRockSlide();
        yield return new WaitForSeconds(0.35f);
        FlashlightManager.SetDarknessAlpha_Static(1, 0.05f);
        TooltipManager.StopEvent_Static();
        ButtonManager.instance.addButtonToUI("Rest", false);
        ButtonManager.instance.addButtonToUI("Panic", false);
        ButtonManager.instance.deactivateAllButtons();
        //GameHandler.instance.startDarkness(true);
        //GameHandler.instance.setDarkness(235);
        GameHandler.instance.modUIColor("CaveIn");
        //IncManager.instance.startDarkness();
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
