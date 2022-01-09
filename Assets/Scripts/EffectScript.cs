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
    [Tooltip("VERY IMPORTANT. Make sure it is a unique name.")]
    public new string name;
    [Tooltip("Seconds.  0 means continue forever (or untill something else stops it).")]
    public float duration = 0;
    [Tooltip("Seconds between triggers. 0 = only onStartOverride() and, after Duration, onStopOverride() will be called.")]
    public float frequency = 1f;
    //[Tooltip("Will not use Duration and Frequency.  Will only call onStartOverride() and onStopOverride() then be done.")]
    //public bool skipDurationEffects = false;
    [Tooltip("The maximum number of concurrent stacks.  0 = unlimited.")]
    public int maxStacks = 0;

    [Tooltip("Each time a stack is applied, reset the duration of all existing stacks.")]
    public bool resetDurationOnStack = false;

    private bool onTimer = false;
    
    private bool isActive = false;
    private bool isPaused = false;
    private float floatRoundFactor = 0.03125f;

    //[Tooltip("GENERALLY KEEP THIS CHECKED.  Will only be false if the object it is tied to UI element with a short term effect that needs to persist even when the effect of it ends.")]
    [HideInInspector]
    public bool destroyObjectOnEnd = true;


    private List<float> effectDurations = new List<float>();
    private float timeToTick;

    private int numStacks = 0;

    private string dataType;

    public void setType(string _dataType)
    {
        dataType = _dataType;
    }

    public string getType()
    {
        return dataType;
    }

    protected virtual void Awake()
    {
        
    }

    //the effect to happen every second.
    public virtual void onTick(int numEffects)
    {

        Debug.Log("EffectScript: DEFUALT onTICK:" + name);
        //OVERRIDEN TO ADD STATUS EFFECT.
        //example.
        //SkillManager.instance.Addxp("Foraging", 5);
        // this status effect will give 5 foraging xp every second.
    }

    public void resetAllDurations()
    {
        if(effectDurations != null)
        {
            for(int i = 0; i < effectDurations.Count; i++)
            {
                effectDurations[i] = duration - floatRoundFactor;
            }
        }
    }

    public virtual void onStart(int oldNumStacks, int addingStacks)
    {
        //GUARANTEED TO BE CALLED ON EFFECT START
    }

    public virtual void onStop(int oldNumStacks, int removingStacks)
    {
        //GUARANTTED TO BE CALLED NO MATTER HOW THE EFFECT ENDS.
    }

    public void setActive(bool toSet)
    {
        isActive = toSet;
    }

    //returns false if effect has reached end.
    public bool tick(float timePassed)
    {
        if (isPaused) return true;
        if (!isActive)
        {
            //Debug.Log("EffectScript:effect: " + nameTag + " is not active, TERMINATING.");
            return false;
        }
        if (timePassed >= timeToTick)
        {
            resetTimeToTick();
            return onTickHidden();
        }
        else
        {
            timeToTick -= timePassed;
            //Debug.Log("EffectScript:effect: timeToTick:" + timeToTick);
        }
        return true;
    }

    //the effect of a tick.
    //  a frequency of 1 and duration of 1 should tick once then remove instantly.
    //  passes the boolean returned to tick() which passes it to effectManager.
    //      if false, then remove the effect from the list.  No more .tick() will be called.
    
    bool onTickHidden()
    {
        //tick every effect in list.
        //a duration of 1 and frequency of 1 should tick once at 1s.
        onTick(numStacks);
        if (onTimer)
        {
            //if some at or past time, remove them.
            int toStop = 0;
            for (int i = (effectDurations.Count - 1); i >= 0; i--)
            {
                effectDurations[i] -= frequency;
                if (effectDurations[i] <= 0)
                {
                    effectDurations.RemoveAt(i);
                    toStop++;
                }
            }

            if (toStop > 0) onStop(numStacks, toStop);
            numStacks -= toStop;

            if (effectDurations.Count <= 0)
            {
                if (numStacks > 0) Debug.LogError("EffectScript:effect:" + name + ": out of effectDuration yet still effect count.");
                //else Debug.Log("EffectScript:effect: " + nameTag + " no count left, TERMINATING.");
                return false;
            }
        }
        //if there are stacks left OR no time based restriction... return 
        return true;
    }

    void resetTimeToTick()
    {
        if(frequency <= 0) timeToTick = duration - floatRoundFactor;
        else timeToTick = frequency - floatRoundFactor;
    }

    public void startEffect(int _numEffects = 1)
    {
        if (frequency < 0)
        {
            Debug.LogError("StatusScript:startCondition: invalid FREQUENCY value in " + base.name + ".  Please fix inspector value.");
            return;
        }
        if (duration < 0)
        {
            Debug.LogError("StatusScript:startCondition: invalid DURATION value in " + base.name + ".  Please fix inspector value.");
            return;
        }

        int oldNumEffects = numStacks;
        numStacks += _numEffects;
        if ((maxStacks != 0) && (numStacks > maxStacks))
        {
            //if adding more stacks than the maximum, only add enough to reach the maximum.
            numStacks = maxStacks;
            _numEffects = maxStacks - oldNumEffects;
        }

        if(resetDurationOnStack)
        {
            resetAllDurations();
        }

        //if in the end you are still adding a positive number of stacks.
        if (_numEffects > 0)
        {
            onStart(oldNumEffects, _numEffects);
            //if there were no stacks, so just starting up again...
            if (oldNumEffects <= 0)
            {
                resetTimeToTick();
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
                for (int i = 0; i < _numEffects; i++)
                {
                    effectDurations.Add(duration - floatRoundFactor);
                }
            }
        }
        isActive = true;
    }

    //can pause all item effects.
    //can pause all region effects... etc.
    public void pauseEffect(string _dataType = "")
    {
        if((_dataType == "") || (dataType == _dataType)) isPaused = true;
    }

    public void unPauseEffect(string _dataType = "")
    {
        if ((_dataType == "") || (dataType == _dataType)) isPaused = false;
    }

    public int getNumStacks()
    {
        return numStacks;
    }

    public void endEffect(int _numEffects)
    {
        int toRemove = _numEffects;
        if((_numEffects == 0) || (_numEffects >= numStacks))
        {
            //remove all
            toRemove = numStacks;
            isActive = false;   //will trigger the effect being popped from the effectList soon~
        }

        onStop(numStacks, toRemove);
        numStacks -= toRemove;
        
        for(int i = 0; i < toRemove; i++)
        {
            if(effectDurations.Count>0)
            {
                //removing oldest first atm.
                effectDurations.RemoveAt(0);
            }
        }
    }
}
