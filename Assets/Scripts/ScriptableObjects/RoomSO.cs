using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// a node within the grid of explorable nodes.
///     UNLIKE OTHER SO_ CANNOT STORE DATA IN THESE.  These are many forest nodes to be made, but only 1 Foraging skill.
///         except generalities that change but are shared between all forest nodes.
/// it is made up of a list of features that each effect the text put out, the buttons that show up.
///     STILL NEED TO ADD IN EXISTS.
///     STILL NEED TO WORK IN SAV
/// </summary>

[CreateAssetMenu(fileName = "NewNode", menuName = "Scriptable Object/Node")]
public class RoomSO : CommonBaseSO
{
    //public string description;
    //public Button[] buttons;

    public int numRandomFeatures;
    public ButtonSO[] presetFeatures;

    //UI to be added to
    //placement order in UI

    public override void reset()
    {
        base.reset();
        //ANYTHING YOU WANT RESET TO DEFAULT VALUES ON NEW GAME.
    }
}