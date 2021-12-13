using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect
{
    //let's do some basic ones.
    // in cold enviornment.  Added when entering enviornment.  Removed upon leaving.
    //      function 

    // active StatusEffects will be in a list and called every (time) seconds.
    // they will change a list of things by an amount each time called.
    // they will have conditions for breaking. pass a variable by reference into init.  If it is false... end and remove.


    // a scriptable object class 'statusEffect'.
    // attach a gameObject holding a script to it.
    // attach a UI prefab to it.
    // attach a name?
    // attach a duration?

    // addStatusCondition("sick")
    // prefab "sick"? attached to a scriptable object.
    //  can attach UI images to it.  Have a status bar.
    //     when you instantiate the prefab, attach it to something, it will start the script.
    //      have a status game object.  Will attach child status's to it as the game goes on.

    //situations, addStatusCondition("sick") again.  Do you want to create a second? have a function in the "sick" script that handles it?
    //      a function for calledAgain() that can be overriden is good.  If a second attempt to add more is made... what to do?
    //removeStatus("cold")
    //  search for child "cold" and if exists end it.


    // other option.  Class StatusEffect.  specific effects inherit from it.  Have a list of those.
    // addEffect("cold") would...
        //if (status == "cold")
        // create sick class and add to list... damn.

    public StatusEffect(ref bool endTrigger)
    {

    }
    public bool isFinished()
    {
        //overriden by specific status effects.
        return true;
    }
    public bool tick(float time)
    {
        //continually apply some effect till condition(s) met.
        return true;
    }
}
