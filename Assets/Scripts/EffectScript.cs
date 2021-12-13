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
    [Tooltip("Seconds.  0 means continue forever (or untill something else stops it).")]
    public float duration = 0;
    [Tooltip("Seconds between triggers. 0 means only onStartOverride() and, after Duration, onStopOverride() will be called.")]
    public float frequency = 1f;
    //[Tooltip("Will not use Duration and Frequency.  Will only call onStartOverride() and onStopOverride() then be done.")]
    //public bool skipDurationEffects = false;

    [Tooltip("How this effect being applied while already applied is handled.  If checked then another individual effect will start.  If unchecked it won't, instead the duration will be reset.")]
    public bool stackEffects = false;

    private bool onTimer = false;
    [HideInInspector]
    public float timeLeft;
    private bool isActive = false;

    //[Tooltip("GENERALLY KEEP THIS CHECKED.  Will only be false if the object it is tied to UI element with a short term effect that needs to persist even when the effect of it ends.")]
    [HideInInspector]
    public bool destroyObjectOnEnd = true;

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
        if(stackEffects == false) resetDuration();
    }

    public virtual void onStartOverride()
    {
        //GUARANTEED TO BE CALLED ON EFFECT START
    }

    public void endingCall()
    {
        onStopOverride();
        if (destroyObjectOnEnd) deleteSelf();
        else endCondition();
    }

    public virtual void onStopOverride()
    {
        //GUARANTTED TO BE CALLED NO MATTER HOW THE EFFECT ENDS.
    }

    public void deleteSelf()
    {
        Debug.Log("deleting self");
        GameObject.Destroy(this.gameObject);
    }

    public void effect()
    {
        
        if (onTimer)
        {
            //it is not 0 because of float based imprecision.  it can get off by a 10^-8 very quickly.
            if(timeLeft <= 0.125)
            {
                if (destroyObjectOnEnd) deleteSelf(); //triggers onDestroy(); which triggers endCondition();
                else endCondition();
            }
            timeLeft = timeLeft - frequency;
        }
        effectOverride();
    }

    public virtual void startCondition()
    {
        if (duration == 0)
        {
            onTimer = false;
        }
        if (frequency < 0)
        {
            Debug.LogError("StatusScript:startCondition: invalid FREQUENCY value in " + name + ".  Please fix inspector value.");
            return;
        }
        if (duration < 0)
        {
            Debug.LogError("StatusScript:startCondition: invalid DURATION value in " + name + ".  Please fix inspector value.");
            return;
        }

        if (isActive)
        {
            //if the effect is already active, instead of adding a new game object, do this.
            //may change this later to be optional in inspector. Always create new instances on application.
            effectStackOverride();
        }
        else
        {
            if (frequency == 0)
            {
                //just make 1 call to the onStop... and then clean this all up.
                Invoke("endingCall", duration);
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
                InvokeRepeating("effect", 0f, frequency);
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
