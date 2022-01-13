using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveIntoLake : ButtonEffectScript
{
    int stage = 0;
    public ButtonSO fish;
    public ButtonSO diveDeeper;
    private void Start()
    {
        toggleButton = false;   //just making sure.  It is set in the inspector, but I've forgoten before.
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

    IEnumerator FishSpawn()
    {
        //random amount of time, then fish shows up for a half second.
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(1, 7));
            int numFish = 1;
            for(int i = 0;i < numFish; i++)
            {
                fish.createSpawn(getPanelIndex() + 5);
            }
            yield return new WaitForSeconds(0.65f);
            ButtonManager.instance.deleteButtons("Fish");
        }
        
    }

    void stageStuff()
    {
        //index is there so I can easily insert stages into spots without having to renumber everything. again.
        int index = 0;
        if (stage == index)
        {
            setButtonText("Dive into the lake", ":D");
            return;
        }
        index++;
        if (stage == index)
        {
            //fish.createSpawn(15);
            GameHandler.instance.modUIColor("Swimming");
            ButtonManager.instance.addButtonArrayToUI("Lake", false);
            ButtonManager.instance.addButtonArrayToUI("Swimming");
            StartCoroutine(FishSpawn());
            setButtonText("Surface", "");

            //setting delay till can be pressed again.
            return;
        }
        index++;
        if (stage == index)
        {
            GameHandler.instance.modUIColor("Lake");
            StopCoroutine(FishSpawn());
            ButtonManager.instance.deleteButtons("Fish");
            diveDeeper.UIInstance.GetComponent<DiveDeeper>().setStage(0);
            ButtonManager.instance.addButtonArrayToUI("Swimming", false);
            ButtonManager.instance.addButtonArrayToUI("Lake");
            setButtonText("Dive into the lake", ":D");
            stage -= 2;
            return;
        }
        
    }
    void everyTime()
    {
        refreshTooltip(); // TOOLTIP WON"T REFRESH WITHOUT THIS HAPPENEING EACH TIME TOOLTIP CHAGNES.
        stage++;
    }


}