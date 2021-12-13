using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ALL STATUS EFFECT SCRIPTS SHOULD INHERIT THIS.
///     Override...
///         effectOverride() to set up what happens each second.
///         effectStackOverride() to set up what happens if the effect is applied again while already active.
/// </summary>

public class EffectScript : MonoBehaviour
{
    [Tooltip("0=forever, x>0 = x seconds")]
    public float duration = 0;
    [Tooltip("how often to trigger.  Default is every 1s")]
    public float frequency = 1f;
    [Tooltip("only call onStartOverride() and onStopOverride().  Disables above duration based effects")]
    public bool skipDurationEffects = false;

    [Tooltip("If the effect is applied again while already active... TRUE = create a new individual ticking effect, effectStackOverride() will be called on all existing effects... FALSE = effectStackOverride() will  be called on all existing effects (by default refreshes the duration)")]
    public bool stackEffect = false;

    private bool onTimer = false;
    [HideInInspector]
    public float timeLeft;
    private bool isActive = false;


    void Start()
    {
        
    }

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

    //THIS IS CALLED WHENEVER...
    // a new effect is added of the same name as this one.  Can handle effect behavior then here.
    public virtual void effectStackOverride()
    {
        //if effects aren't stacked when a new application happens... then, as the default, refresh the duration.
        if(stackEffect == false) resetDuration();
    }

    public virtual void onStartOverride()
    {
        //GUARANTEED TO BE CALLED ON EFFECT START
    }

    public virtual void onStopOverride()
    {
        //GUARANTTED TO BE CALLED NO MATTER HOW THE EFFECT ENDS.
    }

    public void deleteCondition()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void effect()
    {
        
        if (onTimer)
        {
            timeLeft = timeLeft - frequency;

            //it is not 0 because of float based imprecision.  it can get off by a 10^-8 very quickly.
            if(timeLeft <= 0.125)
            {
                deleteCondition(); //triggers onDestroy();
            }
        }
        effectOverride();
    }

    public void startCondition()
    {
        if (duration == 0)
        {
            onTimer = false;
        }
        if (frequency <= 0.125)
        {
            skipDurationEffects = true;
            Debug.LogError("StatusScript:startCondition: invalid FREQUENCY value in " + name + ".  Please fix inspector value.");
        }
        if (duration < 0)
        {
            skipDurationEffects = true;
            Debug.LogError("StatusScript:startCondition: invalid DURATION value in " + name + ".  Please fix inspector value.");
        }

        
        if (isActive)
        {
            //if the effect is already active, instead of adding a new game object, do this.
            //may change this later to be optional in inspector. Always create new instances on application.
            effectStackOverride();
        }
        else
        {
            
            if (skipDurationEffects)
            {
                //if the effect doesn't want duration effects called.  Call start effects and end.
                onStartOverride();
                deleteCondition();
                return;
            }
            else
            {
                onStartOverride();
                isActive = true;
                if (duration == 0)
                {
                    //do not deal with stopping based on time.
                    onTimer = false;
                }
                else
                {
                    //stop when timeLeft <= 0 (unless something else stops this first.
                    onTimer = true;
                    timeLeft = duration;
                }
                //start the status condition in Xs, keep repeating it every Xs.
                InvokeRepeating("effect", frequency, frequency);
            }
        }
    }

    public void endCondition()
    {
        onStopOverride();
        CancelInvoke();
        isActive = false;
    }
}
