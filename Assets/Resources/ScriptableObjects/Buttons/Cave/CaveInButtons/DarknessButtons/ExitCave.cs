using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCave : ButtonEffectScript
{
    bool toggle = false;
    int stage = 0;
    PlayerAttributeSO stamina;
    PlayerAttributeSO health;
    PlayerAttributeSO water;

    public ButtonSO spiderSwarm;

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

    

    void stageStuff()
    {
        //index is there so I can easily insert stages into spots without having to renumber everything. again.
        int index = 0;
        if (stage == index)
        {
            spiderSwarm.createSpawn(getPanelIndex() + Random.Range(-2, 3));
            spiderSwarm.createSpawn(getPanelIndex() + Random.Range(-2, 3));
            spiderSwarm.createSpawn(getPanelIndex() + Random.Range(-2, 3));
            spiderSwarm.createSpawn(getPanelIndex() + Random.Range(-2, 3));
            spiderSwarm.createSpawn(getPanelIndex() + Random.Range(-2, 3));
            //setup in editor.
            //start spider shit.
            //spider buttons will... just exist? sure for now.  Maybe start jumping in annoying spots in the button order.
            //do the create button thing again in ButtonEffectScript, with addding to parent, yada yada.  OnDestroy, it is going to destroy it.
            return;
        }
        index++;
        if (stage == index)
        {
            spiderSwarm.createSpawn(1);
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            spiderSwarm.createSpawn(1);
            for (int i = 0; i < stage * 2; i++)
            {
                spiderSwarm.createSpawn(getPanelIndex() + Random.Range(-2, 3));
            }
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            spiderSwarm.createSpawn(1);
            for (int i = 0; i < stage * 2; i++)
            {
                spiderSwarm.createSpawn(getPanelIndex() + Random.Range(-2, 3));
            }
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            spiderSwarm.createSpawn(1);
            for (int i = 0; i < stage * 2; i++)
            {
                spiderSwarm.createSpawn(getPanelIndex() + Random.Range(-2, 3));
            }
            PressDelay(1.0f);
            return;
        }
        index++;
        if (stage == index)
        {
            //delete leftover spiders
            //Debug.Log("ExitCave:Deleting spiders");
            IncManager.instance.Add<ItemSO>("mcTurtle");
            ButtonManager.instance.deleteButtons("Spider");
            //Debug.Log("ExitCave:deactivating buttons");
            ButtonManager.instance.deactivateAllButtons();
            ButtonManager.instance.addButtonArrayToUI("TheForest");
            //IncManager.instance.endDarkness();
            //GameHandler.instance.setDarkness(25);
            GameHandler.instance.modUIColor("Forest");
           // GameHandler.instance.startDarkness(false);
            return;
        }
    }
    void everyTime()
    {
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }

}
