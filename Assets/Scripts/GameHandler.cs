using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    IncManager incM;
    EffectManager effectM;
    ButtonManager buttonM;
    Player player;
    public static GameHandler instance;

    public GameObject background;
    public GameObject darkness;
    GameObject darknessInstance;

    IEnumerator changeAlpha()
    {
        Color color = darkness.GetComponent<Image>().color;
        while (color.a>0)
        {
            if(color.a>0.02f)
            {
                color.a -= 0.02f;
            }
            else
            {
                color.a = 0;
            }
            darkness.GetComponent<Image>().color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void modUIColor(string location)
    {
        if (location == "Forest")
        {
            //setDarkness(0);
            //StartCoroutine(changeAlpha());
            FlashlightManager.HideDarkness_Static(2);
            background.GetComponent<Image>().color = new Color(0.83f, 0.91f, 0.77f);
            StartCoroutine(McConnelManager.instance.startQuoteDelay("If I were to suggest a course of action, I think we should start a Fillibuster.",3));
        }
        else if (location == "Cave")
        {
            //setDarkness(60);
            //FlashlightManager.HidDarkness_Static();
            //FlashlightManager.HideFlashlight_Static();
            background.GetComponent<Image>().color = new Color(0.08f, 0.08f, 0.08f);
        }
        else if (location == "CaveIn")
        {
            //FlashlightManager.ShowDarkness_Static();
            //setDarkness(255);
        }
        else Debug.LogError("GameHandler:modUIColor: could not find match for string:" + location + ":");
    }
    public void setDarkness(int opacity)
    {
        Color color = darkness.GetComponent<Image>().color;
        color.a = ((float) opacity)/255f;
        darkness.GetComponent<Image>().color = color;
    }    
    public void startDarkness(bool turnOn)
    {
        if (turnOn) darkness.GetComponent<Image>().color = Color.black;
        else darkness.GetComponent<Image>().color = Color.clear;
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        incM = IncManager.instance;
        effectM = EffectManager.instance;
        buttonM = ButtonManager.instance;
        player = Player.instance;

        //COMPILING MPORTANT DATA
        incM.compileDataStorage();  //gets all the known scriptable objects.
        effectM.loadLooseEffects(); //loads loose effects held on a central object.  UNUSED ATM.

        //SETTING UP START DATA
        incM.resetAllScriptableObjects(); //resets all the values of all scriptable objects
        player.defaultValues();
        setInitialInventory();

        //begining game.
        startGame();
    }

    void setInitialInventory()
    {
        IncrementalValuePair[] startingInventory = incM.Get<IncArraySO>("StartingInventory").IncrementableArray;
        if(startingInventory != null && startingInventory.Length>0)
        {
            foreach(IncrementalValuePair pair in startingInventory)
            {
                if(pair.incrementable != null) incM.Add(pair.incrementable, pair.amount);
            }
        }
    }

    void startGame()
    {
        modUIColor("Cave");
        buttonM.addButtonArrayToUI("StartingButtons");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        //Any instantiated objects need to be destroyed or else weird lingering data stuff can happen.
        effectM.destroyLooseEffects();
    }
}
