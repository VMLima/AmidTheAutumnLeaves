using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestRiverButton : ButtonEffect
{
    public override void onClickExtra()
    {
        Debug.Log("GOT A RIVER BUTTON PRESS");
    }
}