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
    //public float timeLeft;
    
    private bool isActive = false;
    private float floatRoundFactor = 0.03125f;

    //[Tooltip("GENERALLY KEEP THIS CHECKED.  Will only be false if the object it is tied to UI element with a short term effect that needs to persist even when the effect of it ends.")]
    [HideInInspector]
    public bool destroyObjectOnEnd = true;


    private List<float> effectDurations = new List<float>();
    private float timeToTick;

    private int numEffects = 0;

    void Start()
    {
        
    }

    //the effect to happen every second.
    public virtual void effectOverride(int numEffects)
    {
        //OVERRIDEN TO ADD STATUS EFFECT.
        //example.
        //SkillManager.instance.Addxp("Foraging", 5);
        // this status effect will give 5 foraging xp every second.
    }

    public void resetDuration()
    {
        if(effectDurations != null)
        {
            for(int i = 0; i < effectDurations.Count; i++)
            {
                effectDurations[i] = duration - floatRoundFactor;
                Debug.Log("resetDuration: reseting duration");
            }
        }
    }

    //THIS IS CALLED WHENEVER...
    // a new effect is added of the same name as this one.  Can handle effect behavior then here.
    public virtual void effectStackOverride()
    {
        //if effects aren't stacked when a new application happens... then, as the default, refresh the duration.
        resetDuration();
    }

    public virtual void onStartOverride(int numSimultaneousEffects = 1)
    {
        //GUARANTEED TO BE CALLED ON EFFECT START
    }

    public virtual void onStopOverride(int numSimultaneousEffects = 1)
    {
        //GUARANTTED TO BE CALLED NO MATTER HOW THE EFFECT ENDS.
    }

    //returns false if effect has reached end.
    public bool tick(float timePassed)
    {
        if (!isActive)
        {
            Debug.Log("EffectScript:effect: is not active, TERMINATING.");
            return false;
        }
        if (timePassed >= timeToTick)
        {
            resetTimeToTick();
            return effect();
        }
        else
        {
            timeToTick -= timePassed;
            //Debug.Log("EffectScript:effect: timeToTick:" + timeToTick);
        }
        return true;
    }

    public bool effect()
    {
        //check if past duration.
        if (onTimer)
        {
            int toStop = 0;
            for(int i = (effectDurations.Count - 1); i >= 0; i--)
            {
                if (effectDurations[i] <= 0)
                {
                    Debug.Log("EffectScript:effect: ending an effect.");
                    effectDurations.RemoveAt(i);
                    toStop++;
                    
                }
            }
            if(toStop > 0)
            {
                onStopOverride(toStop);
                numEffects -= toStop;
            }
            
            if (effectDurations.Count <= 0)
            {
                Debug.Log("EffectScript:effect: no count left, TERMINATING.");
                return false;
            }
        }
        //if not past duration, do effect.
        effectOverride(numEffects);
        return true;
    }

    void resetTimeToTick()
    {
        timeToTick = frequency - floatRoundFactor;
    }

    public virtual void startEffect(int _numEffects = 1)
    {
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

        int oldNumEffects = numEffects;
        
        if (!stackEffects)
        {
            //NON STACKING EFFECTS WILL ONLY EVER HAVE 0 OR 1 STACK.
            if (numEffects>=1)
            {
                //got another effect while the current is already active.
                effectStackOverride();
                _numEffects = 0;
            }
            else
            {
                //just starting
                onStartOverride(1);
                numEffects = 1;
                _numEffects = 1;

                if ((frequency == 0))
                {
                    //if there is a no frequency, just make 1 call at the end of the duration.
                    timeToTick = duration - floatRoundFactor;
                }
            }

        }
        else
        {
            if(numEffects == 0)
            {
                resetTimeToTick();
            }

            onStartOverride(_numEffects);
            numEffects += _numEffects;

            
            
            if ((frequency == 0))
            {
                //if there is no frequency, just make 1 call at the end of the duration.
                timeToTick = duration - floatRoundFactor;
            }
        }

        if (duration == 0)
        {
            //no duration means go untill told from an outside source to stop/pause or amount goes to 0.
            onTimer = false;
        }
        else
        {
            onTimer = true;
            
            //SLOPPY, BUT FUNCTIONAL UNLESS WE HAVE HUGE STACKS ADDED THIS WAY.
            for (int i = 0; i<_numEffects;i++)
            {
                effectDurations.Add(duration - floatRoundFactor);
            }
        }
        isActive = true;
    }

    public void pauseEffect()
    {
        isActive = false;
    }

    public int getNumEffects()
    {
        return numEffects;
    }

    public void endEffect(int _numEffects)
    {
        int toRemove = _numEffects;
        if((_numEffects == 0) || (_numEffects >= numEffects))
        {
            //remove all
            toRemove = numEffects;
            isActive = false;   //will trigger the effect being popped from the effectList soon~
        }

        onStopOverride(toRemove);
        numEffects -= toRemove;
        

        for(int i = 0; i < toRemove; i++)
        {
            if(effectDurations.Count>0)
            {
                effectDurations.RemoveAt(0);
            }
        }

    }
}
