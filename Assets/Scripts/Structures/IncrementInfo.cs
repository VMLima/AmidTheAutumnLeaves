using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IncrementInfo : MonoBehaviour
{
    public Incremental inc;
    public float incTime;
    public int incAmount;
    private float time;
    [TextArea]
    public string descTooltip;
    [TextArea]
    public string descEffect;
    public IncrementInfo(Incremental _inc = null, int _incAmount = 1, float _incTime = 1f, string _descTooltip = "", string _descEffect = "")
    {
        //something to be incremented
        inc = _inc;
        //how often to be incremented
        incTime = _incTime;
        time = incTime;
        //how much to be incremented
        incAmount = _incAmount;
        //tooltip description
        descTooltip = _descTooltip;
        //extra effect description
        descEffect = _descEffect;
    }

    //however often this is called... running through a list of structs doing .tick(deltaTime) will handle all the passive gains.
    public void tick(float _time)
    {
        time -= _time;

        if (time <= 0)
        {
            if (inc != null)    inc.addAmount(incAmount);
            time = incTime;
        }
    }
}
