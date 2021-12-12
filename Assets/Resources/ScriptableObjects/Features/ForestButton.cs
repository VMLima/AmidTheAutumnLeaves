using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestButton : ButtonEffect
{
    public override void onClickExtra()
    {
        Debug.Log("GOT A FOREST BUTTON PRESS");
    }
}