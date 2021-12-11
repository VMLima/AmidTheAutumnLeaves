using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //get this's button component and add listener to onClick()
        this.GetComponent<Button>().onClick.AddListener(onClick);
    }

    public void onClick()
    {
        Debug.Log("GOT A FOREST BUTTON PRESS");
        SkillManager.instance.addXP("Foraging", 5);
    }
}
