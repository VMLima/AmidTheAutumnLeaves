using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    
    [HideInInspector] public static TextManager instance;
    public TextMeshProUGUI textPanel;
    private string text;
    public void addText(string toAdd)
    {
        text = text + toAdd + " \n ";
        textPanel.text = text;
    }

    public void setText(string _text)
    {
        text = _text;
        textPanel.text = text;
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        text = "";
        addText("Hello!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
