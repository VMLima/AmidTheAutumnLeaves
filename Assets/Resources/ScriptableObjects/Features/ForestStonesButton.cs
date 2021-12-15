using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestStonesButton : ButtonEffect
{
    public override void onClickExtra()
    {
        Debug.Log("GOT A STONES BUTTON PRESS");
    }
}