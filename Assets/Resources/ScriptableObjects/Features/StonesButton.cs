using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StonesButton : ButtonEffect
{
    public override void onClickExtra()
    {
        Debug.Log("GOT A STONES BUTTON PRESS");
    }
}