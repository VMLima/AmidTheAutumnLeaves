using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : ButtonEffectScript
{
    private PlayerAttributeSO Health;
    bool eventActive = false;

    private void Start()
    {
        toggleButton = false;
        setButtonText("Take a nap", "I do need my strength\nI'm sure they will find me.  I'm sure of it.");
    }

    IEnumerator sleepTooltip()
    {
        if (!eventActive)
        {
            eventActive = true;
            Cursor.lockState = CursorLockMode.Locked;
            TooltipManager.StartEvent_Static();
            TooltipManager.StartTooltip_Static("zzzzzzzz", true);
            yield return new WaitForSeconds(3);
            IncManager.instance.Add<PlayerAttributeSO>("Health", -10);
            TooltipManager.StartTooltip_Static("  zzzzzzzz\nzzzzzzzz", true);
            yield return new WaitForSeconds(3);
            IncManager.instance.Add<PlayerAttributeSO>("Health", -20);
            TooltipManager.StartTooltip_Static("    zzzzzzzz\n\n  zzzzzzzz\nzzzzzzzz", true);
            yield return new WaitForSeconds(3);
            IncManager.instance.Add<PlayerAttributeSO>("Health", -30);
            TooltipManager.StartTooltip_Static("      zzzzzzzz\n\n    zzzzzzzz\n\n  zzzzzzzz\nzzzzzzzz", true);
            yield return new WaitForSeconds(3);
            IncManager.instance.Add<PlayerAttributeSO>("Health", -50);
            TooltipManager.StartTooltip_Static("YOU HAVE DIED.", true);
            yield return new WaitForSeconds(6);
            TooltipManager.StopEvent_Static();
            Cursor.lockState = CursorLockMode.None;
            eventActive = false;
            
        }
    }

    public override void onStart(int oldNumStacks, int addingStacks)
    {
        //Debug.Log("Medkit:onStart");
        StartCoroutine(sleepTooltip());
    }
}
