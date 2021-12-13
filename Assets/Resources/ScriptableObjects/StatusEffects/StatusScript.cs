using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ALL STATUS EFFECT SCRIPTS SHOULD INHERIT THIS.
///     Override...
///         effectOverride() to set up what happens each second.
///         effectStackOverride() to set up what happens if the effect is applied again while already active.
/// </summary>

public class StatusScript : MonoBehaviour
{
    public float duration;
    private bool onTimer = false;
    [HideInInspector]
    public float timeLeft;
    private bool isActive = false;

    private void OnDestroy()
    {
        endCondition();
    }

    //the effect to happen every second.
    public virtual void effectOverride()
    {
        //OVERRIDEN TO ADD STATUS EFFECT.
        //example.
        //SkillManager.instance.Addxp("Foraging", 5);
        // this status effect will give 5 foraging xp every second.
    }

    public void resetDuration()
    {
        timeLeft = duration;
    }    

    public virtual void effectStackOverride()
    {
        //what happens if another startCondition gets called? does it get more severe? restart the timer?
        //by default just restarts the timer.
        resetDuration();
    }

    public virtual void onStartOverride()
    {
        //GUARANTEED TO BE CALLED ON EFFECT START
    }

    public virtual void onStopOverride()
    {
        //GUARANTTED TO BE CALLED NO MATTER HOW THE EFFECT ENDS.
    }

    public void effect()
    {
        
        if (onTimer)
        {
            timeLeft = timeLeft - 1;
            if(timeLeft <= 0)
            {
                endCondition();
            }
        }
        effectOverride();
    }

    public void startCondition()
    {
        if(isActive)
        {
            effectStackOverride();
        }
        else
        {
            onStartOverride();
            timeLeft = duration;
            isActive = true;
            //start the status condition in 1s, keep repeating it every 1s.
            InvokeRepeating("effect", 1f, 1f);
        }
    }

    public void endCondition()
    {
        onStopOverride();
        CancelInvoke();
        isActive = false;
    }
}
