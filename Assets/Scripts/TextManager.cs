using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    
    [HideInInspector] public static TextManager instance;
    public TextMeshProUGUI textBox;
    private string text;
    //private List<string> texts;

    public void addText(string toAdd)
    {
        if(toAdd != "")
        {
            text = text + toAdd + " \n";
            textBox.text = text;
        }
        
    }

    public void setText(string _text)
    {
        text = _text;
        textBox.text = text;
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
