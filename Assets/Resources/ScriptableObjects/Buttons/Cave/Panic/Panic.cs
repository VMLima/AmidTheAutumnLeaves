using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panic : ButtonEffectScript
{
    private PlayerAttributeSO Health;

    bool eventActive = false;

    private void Start()
    {
        toggleButton = false;
        setButtonText("Panic", "THIS CAN'T BE HAPPENING");
    }

    IEnumerator panicTooltip()
    {
        if (!eventActive)
        {
            eventActive = true;
            Cursor.lockState = CursorLockMode.Locked;
            TooltipManager.StartEvent_Static();
            TooltipManager.StartTooltip_Static("AHHHHHHHHHHHHHHHHHHHHHHHHHHH!!!!!!!!!!", true);
            yield return new WaitForSeconds(3);
            IncManager.instance.Add<PlayerAttributeSO>("Stamina", -2);
            TooltipManager.StartTooltip_Static("SOMEBODY HELP ME!!!!!!", true);
            yield return new WaitForSeconds(2);
            IncManager.instance.Add<PlayerAttributeSO>("Stamina", -5);
            TooltipManager.StartTooltip_Static("  SOMEBODY!!!!!!!!!\nSOMEBODY HELP ME!!!!!!", true);
            yield return new WaitForSeconds(1);
            IncManager.instance.Add<PlayerAttributeSO>("Stamina", -2);
            TooltipManager.StartTooltip_Static("    ANYBODY!?!?!!\n  SOMEBODY!!!!!!!!!\nSOMEBODY HELP ME!!!!!!", true);
            yield return new WaitForSeconds(4);
            IncManager.instance.Add<PlayerAttributeSO>("Stamina", -5);
            TooltipManager.StartTooltip_Static("---Silence----", true);
            yield return new WaitForSeconds(2);
            TooltipManager.StopEvent_Static();
            Cursor.lockState = CursorLockMode.None;
            eventActive = false;
        }
    }

    public override void onStart(int oldNumStacks, int addingStacks)
    {
        //Debug.Log("Medkit:onStart");
        StartCoroutine(panicTooltip());
    }
}
